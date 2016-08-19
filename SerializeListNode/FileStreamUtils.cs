using System.IO;
using System.Text;

namespace SerializeListNode
{
    public static class FileStreamUtils
    {
        public static void AddText(this FileStream fs, string value)
        {
            byte[] info = new UTF8Encoding(true).GetBytes(value);
            fs.Write(info, 0, info.Length);
        }
    }
}
