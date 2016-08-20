using System;
using System.IO;

namespace SerializeListNode {
    public class ListRand {
        public ListNode Head;
        public ListNode Tail;
        public int Count;

        public void Serialize(FileStream s) {
            s.AddText("{\n");
            s.AddText("count:" + Count + "\n");
            s.AddText("head:" + 0 + "\n");
            s.AddText("tail:" + (Count - 1) + "\n");
            s.AddText("[");
            var current = Head;
            while (current != Tail) {
                current.Serialize(s);
                current = current.Next;
            }
            current.Serialize(s);
            s.AddText("\n]\n");
            s.AddText("}");
        }

        public ListRand Deserialize(FileStream s) {
            using (var sr = new StreamReader(s)) {
                var line = sr.ReadLine();
                var headPosition = 0;
                var tailPosition = 0;
                if (line == "{") {
                    do {
                        line = sr.ReadLine();
                        if (line.Contains("count:")) {
                            var delimiterPos = line.IndexOf(":", StringComparison.Ordinal) + 1;
                            var field = line.Substring(delimiterPos, line.Length - delimiterPos);
                            Count = Convert.ToInt32(field);
                        }
                        if (line.Contains("head:")) {
                            var delimiterPos = line.IndexOf(":", StringComparison.Ordinal) + 1;
                            var field = line.Substring(delimiterPos, line.Length - delimiterPos);
                            headPosition = Convert.ToInt32(field);
                        }
                        if (line.Contains("tail:")) {
                            var delimiterPos = line.IndexOf(":", StringComparison.Ordinal) + 1;
                            var field = line.Substring(delimiterPos, line.Length - delimiterPos);
                            tailPosition = Convert.ToInt32(field);
                        }
                    } while (line != "[");
                    ListNode node = null;
                    var position = 0;
                    string[] randomDatas = new string[Count];
                    while (true) {
                        var rand = "";
                        var previous = node;
                        node = ListNode.Deserialize(sr, out rand);
                        if (node == null) {
                            break;
                        }
                        randomDatas.SetValue(rand, position);
                        if (previous == null) {
                            previous = node;
                        }
                        if (position == headPosition) {
                            Head = node;
                        }
                        if (position == tailPosition) {
                            Tail = node;
                            Head.Prev = Tail;
                        }
                        previous.Next = node;
                        node.Prev = previous;
                        node.Next = Head;
                        position++;
                    }
                    position = 0;
                    var current = Head;
                    do {
                        var randData = randomDatas[position];
                        current.Rand = FindRandNode(randData);
                        current = current.Next;
                        position++;
                    } while (current.Prev != Tail);
                }
            }
            return this;
        }

        private ListNode FindRandNode(string randData) {
            var rand = Head;
            while (rand != Tail) {
                if (rand.Data == randData) {
                    return rand;
                }
                rand = rand.Next;
            }
            return null;
        }
    }
}
