using NUnit.Framework;
using WMK.PopupScheduler.Runtime;

namespace PopupScheduler.Tests.EditMode
{
    public class PriorityBasedQueueTests
    {
        private PriorityBasedQueue<int> _queue;
    
        [SetUp]
        public void SetUp()
        {
            _queue = new PriorityBasedQueue<int>();
        }
    
        [Test]
        public void Dequeue_Returns_Items_In_FIFO_When_Same_Priority()
        {
            _queue.Enqueue(1, Priority.High);
            _queue.Enqueue(2, Priority.High);
            _queue.Enqueue(3, Priority.High);

            Assert.AreEqual(1, _queue.Dequeue());
            Assert.AreEqual(2, _queue.Dequeue());
            Assert.AreEqual(3, _queue.Dequeue());
        }

        [Test]
        public void Items_Dequeued_In_Priority_Order()
        {
            _queue.Enqueue(1, Priority.Normal);
            _queue.Enqueue(2, Priority.High);
            _queue.Enqueue(3, Priority.Low);
            _queue.Enqueue(4, Priority.Critical);

            Assert.AreEqual(4, _queue.Dequeue());
            Assert.AreEqual(2, _queue.Dequeue());
            Assert.AreEqual(1, _queue.Dequeue());
            Assert.AreEqual(3, _queue.Dequeue());
        }
    
        [Test]
        public void Peek_Returns_Highest_Priority_Item()
        {
            _queue.Enqueue(1, Priority.Normal);
            _queue.Enqueue(2, Priority.High);
            _queue.Enqueue(3, Priority.Low);
            _queue.Enqueue(4, Priority.Critical);

            Assert.AreEqual(4, _queue.Peek());
        }
    
        [Test]
        public void Clear_Removes_All_Items()
        {
            _queue.Enqueue(1, Priority.Normal);
            _queue.Enqueue(2, Priority.High);
            _queue.Enqueue(3, Priority.Low);
            _queue.Enqueue(4, Priority.Critical);

            _queue.Clear();
        
            Assert.AreEqual(0, _queue.Count);
            Assert.IsTrue(_queue.IsEmpty);
        }
    
        [Test]
        public void Removing_Item_Should_Not_Affect_Order_Of_Other_Priorities()
        {
            _queue.Enqueue(1, Priority.High);
            _queue.Enqueue(2, Priority.Normal);
            _queue.Enqueue(3, Priority.High);
            _queue.Remove(1);

            Assert.AreEqual(3, _queue.Dequeue());
            Assert.AreEqual(2, _queue.Dequeue());
        }
    }
}
