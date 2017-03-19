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
    }
}
