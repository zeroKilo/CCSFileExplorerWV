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
        public Form1()
        {
            InitializeComponent();
            if (tabControl1.TabPages.Contains(tabPage2))
                tabControl1.TabPages.Remove(tabPage2);
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
                    BINHelper.UnpackToFolder(d.FileName, lastfolder, pb1);
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
                    BINHelper.RepackFromFolder(d.FileName, lastfolder, include, pb1);
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
            hb1.ByteProvider = new DynamicByteProvider(new byte[0]);
            pic1.Image = null;
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
                            treeView1.Focus();
                        }
                        break;
                }
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int n = comboBox1.SelectedIndex;
            if (n == -1 || currTexture == null || currPalettes == null || currPalettes.Count == 0)
                return;
            pic1.Image = CCSFile.CreateImage(currPalettes[n].data, currTexture.data);
        }
    }
}
