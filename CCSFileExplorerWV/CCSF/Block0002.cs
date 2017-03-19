using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CCSFileExplorerWV
{
    public class Block0002 : Block
    {
        public uint filecount;
        public uint objcount;
        public List<string> filenames;
        public List<string> objnames;
        public List<ushort> indexes;
        public Block0002(Stream s)
        {
            id = 0xFFFFFFFF;
            uint size = Block.ReadUInt32(s);
            filecount = Block.ReadUInt32(s) - 1;
            objcount = Block.ReadUInt32(s) - 1;
            data = new byte[size * 4];
            s.Read(data, 0, (int)(size * 4));
            int pos = 0x20;
            filenames = new List<string>();
            for (int i = 0; i < filecount; i++)
            {
                filenames.Add(ReadString(data, pos));
                pos += 0x20;
            }
            pos += 0x20;
            objnames = new List<string>();
            indexes = new List<ushort>();
            for (int i = 0; i < objcount; i++)
            {
                objnames.Add(ReadString(data, pos));
                indexes.Add(BitConverter.ToUInt16(data, pos + 0x1E));
                pos += 0x20;
            }
        }

        public override TreeNode ToNode()
        {
            return new TreeNode(type.ToString("X8") + " Size: 0x" + data.Length.ToString("X"));
        }
    }
}
