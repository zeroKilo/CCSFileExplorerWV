using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CCSFileExplorerWV
{
    public class ObjectEntry
    {
        public string name;
        public List<Block> blocks;

        public ObjectEntry(string desc)
        {
            name = desc;
            blocks = new List<Block>();
        }

        public TreeNode ToNode()
        {
            TreeNode result = new TreeNode(name);
            foreach (Block b in blocks)
                result.Nodes.Add(b.ToNode());
            return result;
        }
    }
}
