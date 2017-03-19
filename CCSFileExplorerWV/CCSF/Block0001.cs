using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CCSFileExplorerWV
{
    public class Block0001 : Block
    {
        public string name;

        public Block0001(Stream s)
        {
            uint size = Block.ReadUInt32(s) - 1;
            id = Block.ReadUInt32(s);
            data = new byte[size * 4];
            s.Read(data, 0, (int)(size * 4));
            name = ReadString(data, 0);
        }

        public override TreeNode ToNode()
        {
            return new TreeNode(type.ToString("X8") + " Size: 0x" + data.Length.ToString("X"));
        }

        public override void WriteBlock(Stream s)
        {
            WriteUInt32(s, type);
            WriteUInt32(s, (uint)(data.Length / 4 + 1));
            WriteUInt32(s, id);
            s.Write(data, 0, data.Length);
        }
    }
}
