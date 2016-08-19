using System.IO;
using NUnit.Framework;

namespace SerializeListNode
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public void TestListNodeSerialize()
        {
            var head = new ListNode
            {
                Data = "one",
            };
            var next = new ListNode
            {
                Data = "two",
                Prev = head,
                Rand = head
            };
            var tail = new ListNode
            {
                Data = "three",
                Next = head,
                Prev = next,
                Rand = next,
            };
            head.Next = next;
            head.Prev = tail;
            head.Rand = head;
            next.Next = tail;
            string path = @"d:\NodeSerialize.txt";
            ListNode actual = null;
            // Delete the file if it exists.
            if (File.Exists(path))
            {
                File.Delete(path);
            }
            using (FileStream fs = File.Create(path))
            {
                head.Serialize(fs);
                next.Serialize(fs);
                tail.Serialize(fs);
            }
            using (FileStream fs = File.OpenRead(path))
            {
                using (StreamReader sr = new StreamReader(fs))
                {
                    fs.Seek(0, SeekOrigin.Begin);
                    string rand;
                    actual = ListNode.Deserialize(sr, out rand);
                }
            }
            Assert.AreEqual(head.Data, actual.Data);
        }

        [Test]
        public void TestListRandSerialize()
        {
            var head = new ListNode
            {
                Data = "one",
            };
            var next = new ListNode
            {
                Data = "two",
                Prev = head,
                Rand = head
            };
            var tail = new ListNode
            {
                Data = "three",
                Next = head,
                Prev = next,
                Rand = next,
            };
            head.Next = next;
            head.Prev = tail;
            head.Rand = head;
            next.Rand = head;
            next.Next = tail;
            var listRand = new ListRand
            {
                Head = head,
                Count = 3,
                Tail = tail
            };
            string path = @"d:\RandSerialize.txt";
            ListRand actual = new ListRand();
            // Delete the file if it exists.
            if (File.Exists(path))
            {
                File.Delete(path);
            }
            using (FileStream fs = File.Create(path))
            {
                listRand.Serialize(fs);
            }
            using (FileStream fs = File.OpenRead(path))
            {
                fs.Seek(0, SeekOrigin.Begin);
                actual = actual.Deserialize(fs);
            }
            
            Assert.True(false);
        }
    }
}
