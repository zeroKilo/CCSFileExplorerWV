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
        public Form1()
        {
            InitializeComponent();
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
            if (ccsfile.error != "")
            {
                MessageBox.Show("Can not save a file that had errors on loading!");
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

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int n = listBox1.SelectedIndex;
            if (n == -1 || ccsfile == null)
                return;
            hb1.ByteProvider = new DynamicByteProvider(ccsfile.blobs[n]);
            uint type = BitConverter.ToUInt32(ccsfile.blobs[n], 0);
            switch (type)
            {
                case 0xCCCC0400:
                    if (n < ccsfile.blobs.Count - 1 && BitConverter.ToUInt32(ccsfile.blobs[n + 1], 0) == 0xCCCC0300)
                        pic1.Image = CCSFile.ReadImage(ccsfile.blobs[n], ccsfile.blobs[n + 1]);
                    break;
                default:
                    break;
            }
        }

        private void exportAsBitmapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (pic1.Image != null)
            {
                SaveFileDialog d = new SaveFileDialog();
                d.Filter = "*.bmp|*.bmp|*.jpg|*.jpg";
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
        }

        private void exportRawToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int n = listBox1.SelectedIndex;
            if (n == -1 || ccsfile == null)
                return;
            SaveFileDialog d = new SaveFileDialog();
            d.Filter = "*.bin|*.bin";
            if (d.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                File.WriteAllBytes(d.FileName, ccsfile.blobs[n]);
                MessageBox.Show("Done.");
            }
        }

        private void RefreshStuff()
        {
            rtb1.Text = ccsfile.Info();
            listBox1.Items.Clear();
            foreach (byte[] blob in ccsfile.blobs)
                listBox1.Items.Add("Blob Type 0x" + BitConverter.ToUInt32(blob, 0).ToString("X8"));
        }
    }
}
