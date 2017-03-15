using System;
using System.IO;
using System.IO.Compression;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCSFileExplorerWV
{
    public static class FileHelper
    {
        public static bool isGzipMagic(byte[] data, int start = 0)
        {
            if (data[start++] == 0x1F &&
                data[start++] == 0x8B &&
                data[start++] == 0x08 &&
                (data[start] == 0x08 || data[start] == 0x00))
                return true;
            return false;
        }

        public static byte[] unzipArray(byte[] data)
        {
            MemoryStream result = new MemoryStream();
            MemoryStream input = new MemoryStream(data);
            GZipStream stream = new GZipStream(input, CompressionMode.Decompress);
            stream.CopyTo(result);
            stream.Close();
            return result.ToArray();
        }

        public static byte[] zipArray(byte[] data, string filename)
        {
            MemoryStream m = new MemoryStream();
            GZipStream stream = new GZipStream(m, CompressionMode.Compress);
            new MemoryStream(data).CopyTo(stream);
            stream.Close(); 
            byte[] cdata = m.ToArray();
            m = new MemoryStream();
            if (filename != null && filename != "")
            {
                byte[] buff = Encoding.ASCII.GetBytes(filename);
                m.Write(cdata, 0, 3);
                m.WriteByte(8);
                m.Write(cdata, 4, 6);
                m.Write(buff, 0, buff.Length);
                m.WriteByte(0);
                m.Write(cdata, 10, cdata.Length - 10);
            }
            else
                m.Write(cdata, 0, cdata.Length);
            return m.ToArray();
        }
    }
}
