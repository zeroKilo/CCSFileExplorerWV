using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CCSFileExplorerWV
{
    public class Block0800 : Block
    {
        public List<ModelData> models;

        public Block0800(uint _type, uint _id, byte[] _data)
        {
            type = _type;
            id = _id;
            data = _data;
        }

        public Block0800(Stream s)
        {
            uint size = Block.ReadUInt32(s) - 1;
            id = Block.ReadUInt32(s);
            MemoryStream m = new MemoryStream();
            uint u = 0;
            while (!isValidBlockType(u = ReadUInt32(s)))
                m.Write(BitConverter.GetBytes(u), 0, 4);
            s.Seek(-4, SeekOrigin.Current);
            data = m.ToArray();
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

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Found " + models.Count + " models:");
            int count = 1;
            foreach (ModelData mdl in models)
                sb.AppendLine(" Model " + (count++) + " has " + mdl.nVertices + " vertices");
            return sb.ToString();
        }

        public void ProcessData()
        {
            models = new List<ModelData>();
            MemoryStream m = new MemoryStream();
            m.Write(data, 0x10, data.Length - 0x10);
            long len = m.Length;
            m.Seek(0, 0);
            while (m.Position < m.Length)
                models.Add(new ModelData(m));
        }

        public void CopyToScene(int index)
        {
            SceneHelper.InitScene(ModelToFloats(models[index]));
        }

        public void SaveModel(int n, string filename)
        {
            StringBuilder sb = new StringBuilder();
            ModelData mdl = models[n];
            List<float[]> trilist = ModelToFloats(mdl);
            foreach (float[] v in trilist)
                sb.AppendFormat("v {0} {1} {2}\r\n", v[0], v[1], v[2]);
            sb.AppendLine();
            for (int i = 0; i < trilist.Count; i += 3)
                sb.AppendFormat("f {0} {1} {2}\r\n", i + 1, i + 2, i + 3);
            File.WriteAllText(filename, sb.ToString());
        }

        public List<float[]> ModelToFloats(ModelData mdl)
        {
            List<float[]> result = new List<float[]>();
            List<int> strip = new List<int>();
            int pos = 0;
            bool isStart = true;
            while (pos < mdl.tristrips.Count - 1)
            {
                if (isStart)
                {
                    strip.Add(pos);
                    strip.Add(pos + 1);
                    isStart = false;
                    pos += 2;
                }
                else
                {
                    byte b = (byte)(mdl.tristrips[pos] >> 24);
                    if (b == 1)
                    {
                        isStart = true;
                        result.AddRange(stripToList(strip, mdl.vertices));
                        strip = new List<int>();
                    }
                    else
                        strip.Add(pos++);
                }
            }
            if(strip.Count != 0)
                result.AddRange(stripToList(strip, mdl.vertices));
            return result;
        }

        public List<float[]> stripToList(List<int> strip, List<byte[]> vertices)
        {
            List<float[]> result = new List<float[]>(); 
            int i2, i3;
            for (int i = 0; i < strip.Count - 2; i++)
            {
                if (i % 2 == 0)
                {
                    i2 = i + 1;
                    i3 = i + 2;
                }
                else
                {
                    i2 = i + 2;
                    i3 = i + 1;
                }
                float[] v = new float[5];
                for (int j = 0; j < 3; j++)
                    v[j] = BitConverter.ToInt16(vertices[strip[i]], j * 2);
                result.Add(v);
                v = new float[5];
                for (int j = 0; j < 3; j++)
                    v[j] = BitConverter.ToInt16(vertices[strip[i2]], j * 2);
                result.Add(v);
                v = new float[5];
                for (int j = 0; j < 3; j++)
                    v[j] = BitConverter.ToInt16(vertices[strip[i3]], j * 2);
                result.Add(v);
            }
            return result;
        }

        public class ModelData
        {
            public uint unk1;
            public uint unk2;
            public uint nVertices;
            public List<byte[]> vertices;
            public List<uint> tristrips;
            public List<uint> unknown;
            public List<byte[]> uvs;
            public ModelData(Stream s)
            {
                unk1 = ReadUInt32(s);
                unk2 = ReadUInt32(s);
                nVertices = ReadUInt32(s);
                vertices = new List<byte[]>();
                byte[] buff;
                long len = s.Length;
                for (int i = 0; i < nVertices; i++)
                {
                    buff = new byte[6];
                    s.Read(buff, 0, 6);
                    vertices.Add(buff);
                    if (s.Position >= len)
                    {
                        nVertices = 0;
                        vertices = new List<byte[]>();
                        tristrips = new List<uint>();
                        unknown = new List<uint>();
                        uvs = new List<byte[]>();
                        return;
                    }
                }
                tristrips = new List<uint>();
                for (int i = 0; i < nVertices; i++)
                    tristrips.Add(ReadUInt32(s));
                unknown = new List<uint>();
                for (int i = 0; i < nVertices; i++)
                    unknown.Add(ReadUInt32(s));
                uvs = new List<byte[]>();
                for (int i = 0; i < nVertices; i++)
                {
                    buff = new byte[4];
                    s.Read(buff, 0, 4);
                    uvs.Add(buff);
                }
            }
        }
    }
}
