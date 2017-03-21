using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SimplePaletteQuantizer.ColorCaches;
using SimplePaletteQuantizer.ColorCaches.Common;
using SimplePaletteQuantizer.ColorCaches.EuclideanDistance;
using SimplePaletteQuantizer.ColorCaches.LocalitySensitiveHash;
using SimplePaletteQuantizer.ColorCaches.Octree;
using SimplePaletteQuantizer.Ditherers;
using SimplePaletteQuantizer.Ditherers.ErrorDiffusion;
using SimplePaletteQuantizer.Ditherers.Ordered;
using SimplePaletteQuantizer.Helpers;
using SimplePaletteQuantizer.Quantizers;
using SimplePaletteQuantizer.Quantizers.DistinctSelection;
using SimplePaletteQuantizer.Quantizers.MedianCut;
using SimplePaletteQuantizer.Quantizers.NeuQuant;
using SimplePaletteQuantizer.Quantizers.Octree;
using SimplePaletteQuantizer.Quantizers.OptimalPalette;
using SimplePaletteQuantizer.Quantizers.Popularity;
using SimplePaletteQuantizer.Quantizers.Uniform;
using SimplePaletteQuantizer.Quantizers.XiaolinWu;

namespace CCSFileExplorerWV
{
    public partial class ImageImporter : Form
    {
        public int index;
        public CCSFile ccsfile;
        public bool exitok = false;
        public Block currPalette;
        public Block currTexture;
        public Image currInput;
        public Image currOutput;
        public HashSet<Color> colors;
        public int lastColorCount = -1;
        public int expectedCount = 256;
        public int expectedSizeX;
        public int expectedSizeY;

        public ImageImporter()
        {
            InitializeComponent();
        }

        private void ImageImporter_Load(object sender, EventArgs e)
        {
            FileEntry file = ccsfile.files[index];
            foreach(ObjectEntry obj in file.objects)
                foreach (Block b in obj.blocks)
                {
                    if (b.type == 0xCCCC0400 && currPalette == null) currPalette = b;
                    if (b.type == 0xCCCC0300 && currTexture == null) currTexture = b;
                }
            if (currPalette == null || currTexture == null)
            {
                this.Close();
                return;
            }
            expectedCount = BitConverter.ToInt32(currPalette.data, 0xC);
            textBox1.Text = expectedCount.ToString();
            pic1.Image = currOutput = currInput = CCSFile.CreateImage(currPalette.data, currTexture.data);
            expectedSizeX = currInput.Width;
            expectedSizeY = currInput.Height;
            comboBox1.Items.Clear();
            comboBox1.Items.Add("Distinct Selection");
            comboBox1.Items.Add("Neural Color");
            comboBox1.Items.Add("Optimal Palette");
            comboBox1.Items.Add("Popularity");
            comboBox1.Items.Add("Uniform");
            comboBox1.SelectedIndex = 0;
            comboBox2.Items.Clear();
            comboBox2.Items.Add("None");
            comboBox2.Items.Add("Atkinson Dithering");
            comboBox2.Items.Add("Burkes Ditherer");
            comboBox2.Items.Add("Fan Ditherer");
            comboBox2.Items.Add("Filter Lite Sierra");
            comboBox2.Items.Add("Floyd Steinberg Ditherer");
            comboBox2.Items.Add("Jarvis Judice Ninke Ditherer");
            comboBox2.Items.Add("Shiau Ditherer");
            comboBox2.Items.Add("Sierra Ditherer");
            comboBox2.Items.Add("Stucki Ditherer");
            comboBox2.Items.Add("Two Row Sierra Ditherer");
            comboBox2.Items.Add("Bayer Ditherer 4");
            comboBox2.Items.Add("Bayer Ditherer 8");
            comboBox2.Items.Add("Clustered Dot Ditherer");
            comboBox2.Items.Add("Dot Half Tone Ditherer");
            comboBox2.SelectedIndex = 0;
            CountColors();
        }

        private void CountColors()
        {
            Bitmap bmp = new Bitmap(currOutput);
            colors = new HashSet<Color>();
            for (int y = 0; y < bmp.Size.Height; y++)
                for (int x = 0; x < bmp.Size.Width; x++)
                    colors.Add(bmp.GetPixel(x, y));
            lastColorCount = colors.Count();
            status.Text = "Colors : " + lastColorCount;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            IColorQuantizer q = null;
            switch (comboBox1.SelectedIndex)
            {
                case 0:
                    q = new DistinctSelectionQuantizer();
                    break;
                case 1:
                    q = new NeuralColorQuantizer();
                    break;
                case 2:
                    q = new OptimalPaletteQuantizer();
                    break;
                case 3:
                    q = new PopularityQuantizer();
                    break;
                case 4:
                    q = new UniformQuantizer();
                    break;
            }
            IColorDitherer d = null;
            switch (comboBox2.SelectedIndex)
            {
                case 1:
                    d = new AtkinsonDithering();
                    break;
                case 2:
                    d = new BurkesDitherer();
                    break;
                case 3:
                    d = new FanDitherer();
                    break;
                case 4:
                    d = new FilterLiteSierra();
                    break;
                case 5:
                    d = new FloydSteinbergDitherer();
                    break;
                case 6:
                    d = new JarvisJudiceNinkeDitherer();
                    break;
                case 7:
                    d = new ShiauDitherer();
                    break;
                case 8:
                    d = new SierraDitherer();
                    break;
                case 9:
                    d = new StuckiDitherer();
                    break;
                case 10:
                    d = new TwoRowSierraDitherer();
                    break;
                case 11:
                    d = new BayerDitherer4();
                    break;
                case 12:
                    d = new BayerDitherer8();
                    break;
                case 13:
                    d = new ClusteredDotDitherer();
                    break;
                case 14:
                    d = new DotHalfToneDitherer();
                    break;
            }
            int c = Convert.ToInt32(textBox1.Text);
            int p = Convert.ToInt32(textBox2.Text);
            try
            {
                lastColorCount = -1;
                pic1.Image = currOutput = ImageBuffer.QuantizeImage(currInput, q, d, c, p);
                CountColors();
            }
            catch (Exception ex)
            { status.Text = ex.Message; }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog d = new OpenFileDialog();
            d.Filter = "*.bmp|*.bmp|*.jpg|*.jpg";
            if (d.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Image img = Image.FromFile(d.FileName);
                if (img.Width != expectedSizeX || img.Height != expectedSizeY)
                {
                    MessageBox.Show("The imported image must have the size " + expectedSizeX + "x" + expectedSizeY + "!");
                    return;
                }
                pic1.Image = currOutput = currInput = img;
                CountColors();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (lastColorCount == -1)
                return;
            if (lastColorCount > expectedCount)
            {
                MessageBox.Show("Image still has too much colors to create a " + expectedCount + " color palette!");
                return;
            }
            Color[] list = colors.ToArray();
            for (int i = 0; i < expectedCount; i++)
            {
                if (i < list.Length)
                {
                    currPalette.data[i * 4 + 0x10] = list[i].R;
                    currPalette.data[i * 4 + 0x11] = list[i].G;
                    currPalette.data[i * 4 + 0x12] = list[i].B;
                    currPalette.data[i * 4 + 0x13] = list[i].A;
                }
                else
                {
                    currPalette.data[i * 4 + 0x10] = 0;
                    currPalette.data[i * 4 + 0x11] = 0;
                    currPalette.data[i * 4 + 0x12] = 0;
                    currPalette.data[i * 4 + 0x13] = 0;
                }
            }
            Bitmap bmp = new Bitmap(currOutput);
            int pos = 0;
            Color c1, c2;
            if (expectedCount == 16)
                for (int y = 0; y < expectedSizeY; y++)
                    for (int x = 0; x < expectedSizeX / 2; x++)
                    {
                        c1 = bmp.GetPixel(x * 2 + 1, expectedSizeY - y - 1);
                        c2 = bmp.GetPixel(x * 2, expectedSizeY - y - 1);
                        currTexture.data[0x18 + pos++] = (byte)((FindColorIndex(c1, list) << 4) + FindColorIndex(c2, list));
                    }
            else if (expectedCount == 256)
                for (int y = 0; y < expectedSizeY; y++)
                    for (int x = 0; x < expectedSizeX; x++)
                    {
                        c1 = bmp.GetPixel(x, expectedSizeY - y - 1);
                        currTexture.data[0x18 + pos++] = FindColorIndex(c1, list);
                    }
            exitok = true;
            this.Close();
        }

        public static byte FindColorIndex(Color v, Color[] pal)
        {
            byte index = 0;
            for (byte i = 0; i < pal.Length; i++)
                if (pal[i].R == v.R &&
                    pal[i].G == v.G &&
                    pal[i].B == v.B &&
                    pal[i].A == v.A)
                    return i;
            return index;
        }
    }
}
