using System.IO;
using NUnit.Framework;

namespace SerializeListNode {
    [TestFixture]
    public class Tests {
        [Test]
        public void TestListRandSerialize() {
            var head = new ListNode {
                Data = "one",
            };
            var next = new ListNode {
                Data = "two",
                Prev = head,
                Rand = head
            };
            head.Next = next;
            var third = new ListNode {
                Data = "three",
                Prev = next,
                Rand = head,
            };
            next.Next = third;
            var fourth = new ListNode {
                Data = "four",
                Prev = third
            };
            third.Next = fourth;
            var fifth = new ListNode {
                Data = "five",
                Prev = fourth,
            };
            fourth.Next = fifth;
            var tail = new ListNode {
                Data = "six",
                Next = head,
                Prev = fifth,
                Rand = next,
            };
            fifth.Next = tail;
            head.Prev = tail;
            head.Rand = head;
            fifth.Rand = tail;
            fourth.Rand = fourth;
            var expected = new ListRand {
                Head = head,
                Count = 6,
                Tail = tail
            };
            string path = @"d:\RandSerialize.txt";
            ListRand actual = new ListRand();
            if (File.Exists(path)) {
                File.Delete(path);
            }
            using (FileStream fs = File.Create(path)) {
                expected.Serialize(fs);
            }
            using (FileStream fs = File.OpenRead(path)) {
                fs.Seek(0, SeekOrigin.Begin);
                actual = actual.Deserialize(fs);
            }
            Assert.AreEqual(expected.Count, actual.Count);
            var actualCurrent = actual.Head;
            var expectedCurrent = expected.Head;
            do {
                Assert.AreEqual(expectedCurrent.Data, actualCurrent.Data);
                actualCurrent = actualCurrent.Next;
                expectedCurrent = expectedCurrent.Next;
            } while (actualCurrent.Prev != actual.Tail);
        }

        [Test]
        public void TestListRandSerializeSingleNode() {
            var head = new ListNode {
                Data = "one",
            };
            head.Next = head;
            head.Prev = head;
            head.Rand = head;
            var expected = new ListRand {
                Head = head,
                Count = 1,
                Tail = head
            };
            string path = @"d:\RandSerializeSingleNode.txt";
            ListRand actual = new ListRand();
            if (File.Exists(path)) {
                File.Delete(path);
            }
            using (FileStream fs = File.Create(path)) {
                expected.Serialize(fs);
            }
            using (FileStream fs = File.OpenRead(path)) {
                fs.Seek(0, SeekOrigin.Begin);
                actual = actual.Deserialize(fs);
            }
            Assert.AreEqual(expected.Count, actual.Count);
            var actualCurrent = actual.Head;
            var expectedCurrent = expected.Head;
            do {
                Assert.AreEqual(expectedCurrent.Data, actualCurrent.Data);
                actualCurrent = actualCurrent.Next;
                expectedCurrent = expectedCurrent.Next;
            } while (actualCurrent.Prev != actual.Tail);
        }

        [Test]
        public void TestListRandSerializeTwoNodes() {
            var head = new ListNode {
                Data = "one",
            };
            var next = new ListNode {
                Data = "two",
                Prev = head
            };
            head.Next = next;
            head.Prev = next;
            head.Rand = next;
            next.Next = head;
            next.Rand = head;
            var expected = new ListRand {
                Head = head,
                Count = 2,
                Tail = next
            };
            string path = @"d:\RandSerializeTwoNodes.txt";
            ListRand actual = new ListRand();
            if (File.Exists(path)) {
                File.Delete(path);
            }
            using (FileStream fs = File.Create(path)) {
                expected.Serialize(fs);
            }
            using (FileStream fs = File.OpenRead(path)) {
                fs.Seek(0, SeekOrigin.Begin);
                actual = actual.Deserialize(fs);
            }
            Assert.AreEqual(expected.Count, actual.Count);
            var actualCurrent = actual.Head;
            var expectedCurrent = expected.Head;
            do {
                Assert.AreEqual(expectedCurrent.Data, actualCurrent.Data);
                actualCurrent = actualCurrent.Next;
                expectedCurrent = expectedCurrent.Next;
            } while (actualCurrent.Prev != actual.Tail);
        }
    }
}
