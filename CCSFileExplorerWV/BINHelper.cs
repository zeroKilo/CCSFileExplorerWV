using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CCSFileExplorerWV
{
    public static class BINHelper
    {
        public static void UnpackToFolder(string filename, string folder, ToolStripProgressBar pb1 = null)
        {
            FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read);
            int pos = 0;
            int start = 0;
            int tpos;
            string name;
            fs.Seek(0, SeekOrigin.End);
            long size = fs.Position;
            fs.Seek(0, 0);
            byte[] buff = new byte[4];
            if(pb1 != null) pb1.Maximum = (int)size;
            int fileindex = 0;
            while (fs.Position < size)
            {
                fs.Read(buff, 0, 4);
                if (FileHelper.isGzipMagic(buff, 0))
                {
                    pos = (int)fs.Position - 4;
                    start = pos;
                    while (pos < size)
                    {
                        pos += 0x800;
                        fs.Seek(0x7FC, SeekOrigin.Current);
                        fs.Read(buff, 0, 4);
                        if (FileHelper.isGzipMagic(buff, 0))
                        {
                            fs.Seek(-4, SeekOrigin.Current);
                            break;
                        }
                    }
                    fs.Seek(start, 0);
                    buff = new byte[pos - start];
                    fs.Read(buff, 0, pos - start);
                    buff = FileHelper.unzipArray(buff);
                    name = "";
                    tpos = 0xc;
                    while (buff[tpos] != 0)
                        name += (char)buff[tpos++];
                    File.WriteAllBytes(folder + fileindex.ToString("D8") + "-" + name + ".ccs", buff);
                    fileindex++;
                    if (pb1 != null)
                    {
                        pb1.Value = start;
                        Application.DoEvents();
                    }
                    buff = new byte[4];
                }
                else
                    fs.Seek(0x7FC, SeekOrigin.Current);
            }
            if (pb1 != null) pb1.Value = 0;
            fs.Close();
        }

        public static void RepackFromFolder(string filename, string folder, bool include, ToolStripProgressBar pb1 = null)
        {
            string[] files = Directory.GetFiles(folder, "*.ccs", SearchOption.TopDirectoryOnly);
            FileStream fs = new FileStream(filename, FileMode.Create, FileAccess.Write);
            byte[] buff;
            int count = 0;
            string infilename;
            if (pb1 != null) pb1.Maximum = files.Length;
            foreach (string file in files)
            {
                if (pb1 != null)
                {
                    pb1.Value = count++;
                    Application.DoEvents();
                }
                buff = File.ReadAllBytes(file);
                MemoryStream m = new MemoryStream();
                if (include)
                    infilename = Path.GetFileNameWithoutExtension(file).Substring(9) + ".cmp";
                else
                    infilename = "";
                buff = FileHelper.zipArray(buff, infilename);
                m.Write(buff, 0, buff.Length);
                while (m.Length % 0x800 != 0)
                    m.WriteByte(0);
                buff = m.ToArray();
                fs.Write(buff, 0, buff.Length);
            }
            if (pb1 != null) pb1.Value = 0;
            fs.Close();
        }
    }
}
