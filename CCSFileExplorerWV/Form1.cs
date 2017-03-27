using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Be.Windows.Forms;

namespace CCSFileExplorerWV
{
    public partial class Form1 : Form
    {
        public CCSFile ccsfile;
        public string lastfolder;
        public List<Block> currPalettes;
        public Block currTexture;
        public Block0800 currModel;
        public float viewRotation = 0;

        public Form1()
        {
            InitializeComponent();
            if (tabControl1.TabPages.Contains(tabPage2))
                tabControl1.TabPages.Remove(tabPage2);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.Opaque, true);
        }

        private void unpackBINToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog d = new OpenFileDialog();
            d.Filter = "*.bin|*.bin";
            if (d.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                fbd.SelectedPath = lastfolder;
                if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    lastfolder = fbd.SelectedPath + "\\";
                    string file = d.FileName;
                    this.Enabled = false;
                    BINHelper.UnpackToFolder(d.FileName, lastfolder, pb1, status);
                    this.Enabled = true;
                    MessageBox.Show("Done.");
                }
            }
        }

        private void repackBINToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fbd.SelectedPath = lastfolder;
            if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                lastfolder = fbd.SelectedPath + "\\";
                List<string> files = new List<string>(Directory.GetFiles(lastfolder, "*.ccs", SearchOption.TopDirectoryOnly));
                files.Sort();
                SaveFileDialog d = new SaveFileDialog();
                d.Filter = "*.bin|*.bin";
                if (d.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    bool include = MessageBox.Show("Include filenames in GZipStreams (yes for areaserver, no for gamefile)?", "How to save", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes;
                    this.Enabled = false;
                    BINHelper.RepackFromFolder(d.FileName, lastfolder, include, pb1, status);
                    this.Enabled = true;
                    MessageBox.Show("Done.");
                }
            }
        }

        private void openCCSFileToolStripMenuItem_Click(object sender, EventArgs e)
        {            
            OpenFileDialog d = new OpenFileDialog();
            d.Filter = "*.ccs|*.ccs";
            if (d.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                ccsfile = new CCSFile(File.ReadAllBytes(d.FileName));
                AddRecent(d.FileName);
                RefreshStuff();
            }
        }

        private void saveCCSFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ccsfile == null)
                return;
            if (!ccsfile.isvalid)
            {
                MessageBox.Show("Can not save a invalid file!");
                return;
            }
            SaveFileDialog d = new SaveFileDialog();
            d.Filter = "*.ccs|*.ccs";
            d.FileName = ccsfile.header.name + ".ccs";
            if (d.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                ccsfile.Save(d.FileName);
                RefreshStuff();
                MessageBox.Show("Done.");
            }
        }

        private void exportAsBitmapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TreeNode sel = treeView1.SelectedNode;
            if (ccsfile == null || !ccsfile.isvalid || sel == null || pic1 == null)
                return;
            SaveFileDialog d = new SaveFileDialog();
            d.Filter = "*.bmp|*.bmp|*.jpg|*.jpg";
            d.FileName = Path.GetFileNameWithoutExtension(sel.Text);
            if (d.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                switch (Path.GetExtension(d.FileName).ToLower())
                {
                    case ".jpg":
                        pic1.Image.Save(d.FileName, System.Drawing.Imaging.ImageFormat.Jpeg);
                        break;
                    case ".bmp":
                        pic1.Image.Save(d.FileName, System.Drawing.Imaging.ImageFormat.Bmp);
                        break;
                    default:
                        MessageBox.Show("Unknown Format Extension!");
                        return;
                }
                MessageBox.Show("Done.");
            }
        }

        private void exportRawToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TreeNode sel = treeView1.SelectedNode;
            if (ccsfile == null || !ccsfile.isvalid || sel == null)
                return;
            if (sel.Level == 3)
            {
                TreeNode obj = sel.Parent;
                TreeNode file = obj.Parent;
                FileEntry entryf = ccsfile.files[file.Index];
                ObjectEntry entryo = entryf.objects[obj.Index];
                SaveFileDialog d = new SaveFileDialog();
                d.Filter = "*.bin|*.bin";
                d.FileName = entryo.name + ".bin";
                if (d.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    File.WriteAllBytes(d.FileName, entryo.blocks[sel.Index].data);
                    MessageBox.Show("Done.");
                }
            }
        }

        private void RefreshStuff()
        {
            rtb1.Text = ccsfile.Info();
            treeView1.Nodes.Clear();
            if (!ccsfile.isvalid)
                return;
            TreeNode t = new TreeNode(ccsfile.header.name);
            foreach (FileEntry entry in ccsfile.files)
                t.Nodes.Add(entry.ToNode());
            t.Expand();
            treeView1.Nodes.Add(t);
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (tabControl1.TabPages.Contains(tabPage2))
                tabControl1.TabPages.Remove(tabPage2);
            if (tabControl1.TabPages.Contains(tabPage3))
                tabControl1.TabPages.Remove(tabPage3);
            hb1.ByteProvider = new DynamicByteProvider(new byte[0]);
            timer1.Enabled = false;
            pic1.Image = null;
            pic2.Image = null;
            if (ccsfile == null || !ccsfile.isvalid)
                return;
            TreeNode sel = e.Node;
            if (sel.Level == 3)
            {
                TreeNode obj = sel.Parent;
                TreeNode file = obj.Parent;
                FileEntry entryf = ccsfile.files[file.Index];
                ObjectEntry entryo = entryf.objects[obj.Index];
                hb1.ByteProvider = new DynamicByteProvider(entryo.blocks[sel.Index].data);
                if (entryo.blocks[sel.Index].type == 0xCCCC0800)
                {
                    currModel = (Block0800)entryo.blocks[sel.Index];
                    currModel.ProcessData();
                    if (!SceneHelper.init)
                        SceneHelper.InitializeDevice(pic2);
                    comboBox2.Items.Clear();
                    for (int i = 0; i < currModel.models.Count; i++)
                        comboBox2.Items.Add("Model " + (i + 1));
                    if (comboBox2.Items.Count > 0)
                        comboBox2.SelectedIndex = 0;
                    timer1.Enabled = true;
                    tabControl1.TabPages.Add(tabPage3);
                    tabControl1.SelectedTab = tabPage3;
                }
            }
            if (sel.Level == 1)
            {
                string ext = Path.GetExtension(sel.Text).ToLower();
                switch (ext)
                {
                    case ".bmp":
                        comboBox1.Items.Clear();
                        currPalettes = new List<Block>();
                        currTexture = null;
                        foreach (ObjectEntry obj in ccsfile.files[sel.Index].objects)
                            foreach (Block b in obj.blocks)
                            {
                                if (b.type == 0xCCCC0400)
                                {
                                    comboBox1.Items.Add(obj.name);
                                    currPalettes.Add(b);
                                }
                                if (b.type == 0xCCCC0300)
                                    currTexture = b;
                            }
                        if (comboBox1.Items.Count > 0)
                        {
                            tabControl1.TabPages.Add(tabPage2);
                            comboBox1.SelectedIndex = 0;
                            tabControl1.SelectedTab = tabPage2;
                        }
                        break;
                }
            }
            treeView1.Focus();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int n = comboBox1.SelectedIndex;
            if (n == -1 || currTexture == null || currPalettes == null || currPalettes.Count == 0)
                return;
            pic1.Image = CCSFile.CreateImage(currPalettes[n].data, currTexture.data);
        }

        private void importRawToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TreeNode sel = treeView1.SelectedNode;
            if (ccsfile == null || !ccsfile.isvalid || sel == null)
                return;
            if (sel.Level == 3)
            {
                TreeNode obj = sel.Parent;
                TreeNode file = obj.Parent;
                FileEntry entryf = ccsfile.files[file.Index];
                ObjectEntry entryo = entryf.objects[obj.Index];
                OpenFileDialog d = new OpenFileDialog();
                d.Filter = "*.bin|*.bin";
                if (d.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    entryo.blocks[sel.Index].data = File.ReadAllBytes(d.FileName);
                    ccsfile.Rebuild();
                    ccsfile.Reload();
                    RefreshStuff();
                }
                MessageBox.Show("Done.");
            }
        }

        private void openInImageImporterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ccsfile == null || !ccsfile.isvalid || treeView1.SelectedNode == null)
                return;
            TreeNode sel = treeView1.SelectedNode;
            if (sel.Level == 1)
            {
                string ext = Path.GetExtension(sel.Text).ToLower();
                if (ext != ".bmp")
                {
                    MessageBox.Show("Not a bmp file!");
                    return;
                }
                ImageImporter f = new ImageImporter();
                f.index = sel.Index;
                f.ccsfile = ccsfile;
                f.ShowDialog();
                if (f.exitok)
                {
                    ccsfile = f.ccsfile;
                    RefreshStuff();
                }
            }
        }

        private void exportToObjToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ccsfile == null || !ccsfile.isvalid || treeView1.SelectedNode == null)
                return;
            TreeNode sel = treeView1.SelectedNode;
            if (sel.Level == 3)
            {
                TreeNode obj = sel.Parent;
                TreeNode file = obj.Parent;
                FileEntry entryf = ccsfile.files[file.Index];
                ObjectEntry entryo = entryf.objects[obj.Index];
                hb1.ByteProvider = new DynamicByteProvider(entryo.blocks[sel.Index].data);
                if (entryo.blocks[sel.Index].type == 0xCCCC0800)
                {
                    Block0800 mdl = (Block0800)entryo.blocks[sel.Index];
                    mdl.ProcessData();
                    SaveFileDialog d = new SaveFileDialog();
                    d.Filter = "*.obj|*.obj";
                    d.FileName = obj.Text + ".obj";
                    if (d.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        string input = Microsoft.VisualBasic.Interaction.InputBox("Which Model to export? (1 - " + (mdl.models.Count) + ")", "Export Model", (comboBox2.SelectedIndex + 1).ToString());
                        if (input != "")
                        {
                            mdl.SaveModel(Convert.ToInt32(input) - 1, d.FileName);
                            MessageBox.Show("Done.");
                        }
                    }
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            SceneHelper.Render();
            if (SceneHelper.doRotate)
            {
                viewRotation += 1f;
                SceneHelper.SetRotation360(viewRotation);
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            int n = comboBox2.SelectedIndex;
            if (n == -1 || currModel == null || !SceneHelper.init)
                return;
            currModel.CopyToScene(n);
        }

        private void pic2_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                float p = e.Y / (float)pic2.Height - 0.5f;
                SceneHelper.SetHeight(p);
                SceneHelper.doRotate = false;
                autoRotationOnToolStripMenuItem.Checked = false;
                p = e.X / (float)pic2.Width;
                SceneHelper.SetRotation360(p * 360f);
            }
            if (e.Button == MouseButtons.Right)
            {
                float p = e.Y / (float)pic2.Height + 0.001f;
                p *= 3;
                SceneHelper.SetZoomFactor(p);
            }
        }

        private void pic2_Resize(object sender, EventArgs e)
        {
            SceneHelper.Resize();
        }

        private void autoRotationOnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SceneHelper.doRotate = autoRotationOnToolStripMenuItem.Checked;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CheckRecent();
        }

        public void CheckRecent()
        {
            recentToolStripMenuItem.Enabled = false;
            if (recentToolStripMenuItem.HasDropDownItems)
                recentToolStripMenuItem.DropDownItems.Clear();
            if (File.Exists("recent.txt"))
            {
                string[] lines = File.ReadAllLines("recent.txt");
                foreach (string line in lines)
                    if (line.Trim() != "")
                    {
                        ToolStripMenuItem item = new ToolStripMenuItem(line.Trim());
                        item.Click += recentClick;
                        recentToolStripMenuItem.DropDownItems.Add(item);
                        recentToolStripMenuItem.Enabled = true;
                    }
            }
        }

        public void recentClick(object sender, EventArgs e)
        {
            ccsfile = new CCSFile(File.ReadAllBytes(((ToolStripMenuItem)sender).Text));
            RefreshStuff();
        }

        public void AddRecent(string path)
        {
            if (!File.Exists("recent.txt"))
                File.WriteAllLines("recent.txt", new string[0]);
            string[] lines = File.ReadAllLines("recent.txt");
            List<string> result = new List<string>();
            result.Add(path);
            int index = 0;
            while (result.Count < 10 && index < lines.Length && lines[index] != path)
                result.Add(lines[index++]);
            File.WriteAllLines("recent.txt", result.ToArray());
            CheckRecent();
        }

        private void wireframeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SceneHelper.wireframe = wireframeToolStripMenuItem.Checked;
        }
    }
}
