using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace CCSFileExplorerWV
{
    public class CCSFile
    {
        public byte[] raw;
        public string name;
        public List<string> filenames;
        public List<string> objectnames;
        public List<byte[]> blobs;
        public string error = "";

        public CCSFile(byte[] rawBuffer)
        {
            raw = rawBuffer;
            Reload();
        }

        public void Reload()
        {
            name = ReadString(raw, 0xC);
            int filecount = BitConverter.ToInt32(raw, 0x44) - 1;
            int objcount = BitConverter.ToInt32(raw, 0x48) - 1;
            filenames = new List<string>();
            objectnames = new List<string>();
            int pos = 0x6C;
            for (int i = 0; i < filecount; i++)
                filenames.Add(ReadString(raw, pos + i * 32));
            pos += filecount * 32 + 32;
            for (int i = 0; i < objcount; i++)
                objectnames.Add(ReadString(raw, pos + i * 32));
            pos += objcount * 32 + 8;
            int size;
            uint type;
            MemoryStream m = new MemoryStream();
            blobs = new List<byte[]>();
            try
            {
                while (pos < raw.Length)
                {
                    type = BitConverter.ToUInt32(raw, pos);
                    size = BitConverter.ToInt32(raw, pos + 4) * 4 + 8;
                    if (type == 0xcccc0300)
                        size -= 200;
                    m = new MemoryStream();
                    m.Write(raw, pos, size);
                    blobs.Add(m.ToArray());
                    pos += size;
                }
            }
            catch (Exception)
            {
                error = "Error at 0x" + pos.ToString("X8");
            }
        }

        public void Rebuild()
        {
            //todo
        }

        public void Save(string filename)
        {
            Rebuild();
            File.WriteAllBytes(filename, raw);
        }

        public static string ReadString(byte[] data, int pos)
        {
            string result = "";
            while (data[pos] != 0)
                result += (char)data[pos++];
            return result;
        }

        public string Info()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("CCSFile: " + name);
            sb.AppendLine("Errors: " + error);
            sb.AppendLine("File Names:");
            foreach (string filename in filenames)
                sb.AppendLine(" " + filename);
            sb.AppendLine();
            sb.AppendLine("Object Names:");
            foreach (string objname in objectnames)
                sb.AppendLine(" " + objname);
            return sb.ToString();
        }

        public static Bitmap ReadImage(byte[] palette, byte[] data)
        {
            List<Color> pal = new List<Color>();
            int pos = 0x1C;
            byte r, g, b, a;
            while (pos < palette.Length)
            {
                r = palette[pos];
                g = palette[pos + 1];
                b = palette[pos + 2];
                a = palette[pos + 3];
                if (a <= 128)
                    a = (byte)((a * 255) / 128);
                pal.Add(Color.FromArgb(a, r, g, b));
                pos += 4;
            }
            int sizeX = (int)Math.Pow(2, data[0x18]);
            int sizeY = (int)Math.Pow(2, data[0x19]);
            pos = 0x24;
            int dataSize = data.Length - pos;
            Bitmap result = new Bitmap(sizeX, sizeY);
            if (dataSize * 2 == sizeX * sizeY)
                for (int y = 0; y < sizeY; y++)
                    for (int x = 0; x < sizeX / 2; x++)
                    {
                        result.SetPixel(x * 2, sizeY - y - 1, pal[data[pos] % 0x10]);
                        result.SetPixel(x * 2 + 1, sizeY - y - 1, pal[data[pos] / 0x10]);
                        pos++;
                    }
            else if (dataSize == sizeX * sizeY)
                for (int y = 0; y < sizeY; y++)
                    for (int x = 0; x < sizeX; x++)
                        result.SetPixel(x, sizeY - y - 1, pal[data[pos++]]);
            return result;
        }
    }
}
