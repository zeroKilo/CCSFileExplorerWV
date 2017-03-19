using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CCSFileExplorerWV
{
    public class FileEntry
    {
        public string name;
        public List<ObjectEntry> objects;

        public FileEntry(string desc)
        {
            name = desc;
            objects = new List<ObjectEntry>();
        }

        public TreeNode ToNode()
        {
            TreeNode result = new TreeNode(name);
            foreach (ObjectEntry entry in objects)
                result.Nodes.Add(entry.ToNode());
            return result;
        }
    }
}
