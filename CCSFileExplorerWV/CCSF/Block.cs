using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CCSFileExplorerWV
{
    public abstract class Block
    {
        public uint type;
        public uint id;
        public byte[] data;

        public abstract TreeNode ToNode();
        public abstract void WriteBlock(Stream s);

        public static Block ReadBlock(Stream s)
        {
            Block result = null;
            uint type = ReadUInt32(s);
            switch (type)
            {
                case 0xCCCC0001:
                    result = new Block0001(s);
                    break;
                case 0xCCCC0002:
                    result = new Block0002(s);
                    break;
                case 0xCCCC0005:
                    result = new Block0005(s);
                    break;
                case 0xCCCC0300:
                    result = new Block0300(s);
                    break;
                case 0xCCCC0800:
                    result = new Block0800(s);
                    break;
                default:
                    result = new BlockDefault(s);
                    break;
            }
            result.type = type;
            return result;
        }

        public static uint ReadUInt32(Stream s)
        {
            byte[] buff = new byte[4];
            s.Read(buff, 0, 4);
            return BitConverter.ToUInt32(buff, 0);
        }

        public static string ReadString(byte[] buff, int pos)
        {
            string result = "";
            while (buff[pos] != 0)
                result += (char)buff[pos++];
            return result;
        }

        public static void WriteUInt32(Stream s, uint u)        
        {
            s.Write(BitConverter.GetBytes(u), 0, 4);
        }

        public static void WriteString(Stream s, string t, int minsize = -1)
        {
            MemoryStream m = new MemoryStream();
            foreach (char c in t)
                m.WriteByte((byte)c);
            m.WriteByte(0);
            if(minsize != -1)
                while (m.Length != minsize)
                    m.WriteByte(0);
            s.Write(m.ToArray(), 0, (int)m.Length);
        }

        public static uint[] validBlockTypes = new uint[] { 
            0xCCCC0001, 0xCCCC0002, 0xCCCC0005, 0xCCCC0100,
            0xCCCC0102, 0xCCCC0108, 0xCCCC0200, 0xCCCC0300,
            0xCCCC0400, 0xCCCC0500, 0xCCCC0502, 0xCCCC0600,
            0xCCCC0601, 0xCCCC0603, 0xCCCC0609, 0xCCCC0700,
            0xCCCC0800, 0xCCCC0900, 0xCCCC0A00, 0xCCCC0B00,
            0xCCCC0C00, 0xCCCC0E00, 0xCCCC1100, 0xCCCC1200,
            0xCCCC1300, 0xCCCC1400, 0xCCCC1900, 0xCCCC1901,
            0xCCCC2000, 0xCCCCFF01, 0xCCCC0202
        };

        public static bool isValidBlockType(uint u)
        {
            foreach (uint vu in validBlockTypes)
                if (u == vu)
                    return true;
            return false;
        }
    }
}
