using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace WMK.PopupScheduler.Runtime
{
    public class PriorityBasedQueue<T> where T : IEquatable<T>
    {
        private readonly SortedDictionary<Priority, List<T>> _queues = new();
        private readonly Dictionary<T, Priority> _items = new();
        
        public int Count => _queues.Values.Sum(queue => queue.Count);
        
        public bool IsEmpty => _queues.Count == 0;
        
        public bool Contains(T item) => _items.ContainsKey(item);
        
        public T Peek() => IsEmpty ? default : _queues.Last().Value.Last();
        
        public void Enqueue(T item, Priority priority)
        {
            ValidateItem(item);
            if (_items.ContainsKey(item))
            {
                Debug.LogWarning($"{typeof(T).Name} already in queue");
                return;
            }
            
            if (!_queues.ContainsKey(priority))
            {
                _queues[priority] = new List<T>();
            }
            
            _queues[priority].Add(item);
            _items.Add(item, priority);
        }
        
        public void EnqueueRange(IEnumerable<(T item, Priority priority)> items)
        {
            foreach (var (item, priority) in items)
            {
                Enqueue(item, priority);
            }
        }
        
        public T Dequeue()
        {
            if (IsEmpty) return default;
            
            var queue = _queues.Last().Value;
            var item = queue.Last();
            queue.Remove(item);
            _items.Remove(item);
            CleanupEmptyQueue(_queues.Last().Key);
            return item;
        }
        
        public T Remove(T item)
        {
            ValidateItem(item);
            if (!_items.TryGetValue(item, out var priority))
            {
                Debug.LogWarning($"{typeof(T).Name} not in queue");
                return default;
            }
            
            var queue = _queues[priority];
            queue.Remove(item);
            _items.Remove(item);
            CleanupEmptyQueue(priority);
            return item;
        }
        
        public void RemoveRange(IEnumerable<T> items)
        {
            foreach (var item in items)
            {
                Remove(item);
            }
        }
        
        public void Clear()
        {
            _queues.Clear();
            _items.Clear();
        }
        
        private void CleanupEmptyQueue(Priority priority)
        {
            if (_queues[priority].Count == 0)
            {
                _queues.Remove(priority);
            }
        }
        
        private static void ValidateItem(T item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));
        }
    }
}