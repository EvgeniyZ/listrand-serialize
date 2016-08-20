using System;
using System.IO;

namespace SerializeListNode {
    public class ListNode {
        public ListNode Prev;
        public ListNode Rand;
        public ListNode Next;
        public string Data;

        public void Serialize(FileStream s) {
            s.AddText("\n{\n");
            s.AddText("data:" + Data + "\n");
            s.AddText("prev:" + Prev.Data + "\n");
            s.AddText("next:" + Next.Data + "\n");
            s.AddText("rand:" + Rand.Data + "\n");
            s.AddText("}");
        }

        public static ListNode Deserialize(StreamReader s, out string rand) {
            rand = "";
            var line = s.ReadLine();
            if (line == "{") {
                var listNode = new ListNode();
                do {
                    line = s.ReadLine();
                    if (line.Contains("data:")) {
                        var delimiterPos = line.IndexOf(":", StringComparison.Ordinal) + 1;
                        var field = line.Substring(delimiterPos, line.Length - delimiterPos);
                        listNode.Data = field;
                    }
                    if (line.Contains("rand:")) {
                        var delimiterPos = line.IndexOf(":", StringComparison.Ordinal) + 1;
                        var field = line.Substring(delimiterPos, line.Length - delimiterPos);
                        rand = field;
                    }
                } while (line != "}");
                return listNode;
            }
            return null;
        }
    }
}
