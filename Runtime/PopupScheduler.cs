using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;

namespace WMK.PopupScheduler.Runtime
{
    public enum PopupState
    {
        UnHandled,
        Pending,
        Opened,
    }
    
    public class PopupScheduler<T> where T : IPopup, IEquatable<T>
    {
        private readonly PriorityBasedQueue<T> _popupQueue = new();
        private readonly List<T> _popupOpened = new();
        
        public event Action<T> OnPopupScheduled;
        public event Action<T> OnPopupUnScheduled;
        public event Action<T> BeforePopupOpen;

        public int OpenedCount => _popupOpened.Count;
        
        public int QueuedCount => _popupQueue.Count;
        
        public bool AnyPending => QueuedCount > 0;
        
        public bool AnyOpened => OpenedCount > 0;
        
        public bool IsEmpty => OpenedCount == 0 && QueuedCount == 0;
        
        public bool IsOpened(T popup) => _popupOpened.Contains(popup);
        
        public bool IsPending(T popup) => _popupQueue.Contains(popup);
        
        public bool IsHandled(T popup) => IsOpened(popup) || IsPending(popup);
        
        public PopupState GetState(T popup) => IsOpened(popup)
            ? PopupState.Opened
            : (IsPending(popup) ? PopupState.Pending : PopupState.UnHandled);
        
        public T GetTopOpened() => AnyOpened ? _popupOpened.Last() 
            : throw new InvalidOperationException("No popups opened");
        
        public void Schedule(T popup)
        {
            ValidatePopup(popup);

            if (IsPending(popup))
            {
                Debug.LogWarning($"{popup.GetType().Name} already in queue");
                return;
            }
            
            if (IsOpened(popup))
            {
                Debug.LogWarning($"{popup.GetType().Name} already opened");
                return;
            }
            
            if (IsEmpty)
            {
                _popupOpened.Add(popup);
                BeforePopupOpen?.Invoke(popup);
                popup.Open();
                return;
            }
            
            _popupQueue.Enqueue(popup, popup.Priority);
            OnPopupScheduled?.Invoke(popup);
            
            var nextPopup = _popupQueue.Peek();
            if (nextPopup.Priority > GetTopOpened().Priority)
            {
                OpenNextPendingIfAny();
            }
            else if (nextPopup.Priority == GetTopOpened().Priority)
            {
                if (nextPopup.Behaviour != PopupBehaviour.Pend)
                {
                    OpenNextPendingIfAny();
                }
            }
        }
        
        private void OpenNextPendingIfAny()
        {
            if (!AnyPending) return;
            var nextPopup = _popupQueue.Peek();
            ValidatePopup(nextPopup);
            HandlePopupClosure(nextPopup);
            _popupQueue.Dequeue();
            _popupOpened.Add(nextPopup);
            BeforePopupOpen?.Invoke(nextPopup);
            nextPopup.Open();
        }
        
        private T HandlePopupClosure(T nextPopup)
        {
            Assert.IsTrue(AnyPending);
            var topOpened = GetTopOpened();
            
            switch (nextPopup.Behaviour)
            {
                case PopupBehaviour.StackOnTop:
                    break;
                case PopupBehaviour.CloseTop:
                    _popupOpened.Remove(topOpened);
                    topOpened.Close();
                    break;
                case PopupBehaviour.CloseSamePriority:
                    while (GetTopOpened().Priority == nextPopup.Priority)
                    {
                        _popupOpened.Remove(topOpened);
                        topOpened.Close();
                    }
                    break;
                case PopupBehaviour.CloseAll:
                    CloseAllOpened();
                    break;
            }
            
            return nextPopup;
        }

        public T UnSchedule(T popup)
        {
            if (!IsPending(popup))
            {
                Debug.LogWarning($"{popup.GetType().Name} not in queue");
                return default;
            }
            
            var removedPopup = _popupQueue.Remove(popup);
            OnPopupUnScheduled?.Invoke(popup);
            return removedPopup;
        }

        public void ClearAllPending()
        {
            if (!AnyPending)
            {
                Debug.LogWarning("No popups in queue");
                return;
            }
            
            _popupQueue.Clear();
        }
        
        public void Close(T popup)
        {
            if (!IsOpened(popup))
            {
                Debug.LogWarning($"{popup.GetType().Name} not opened");
                return;
            }
            
            _popupOpened.Remove(popup);
            popup.Close();
            
            if (!AnyOpened)
                OpenNextPendingIfAny();
        }
        
        public void CloseTopOpened()
        {
            if (!AnyOpened)
            {
                Debug.LogWarning("No popups opened");
                return;
            }
            var popup = GetTopOpened();
            _popupOpened.Remove(popup);
            popup.Close();
            
            if (!AnyOpened)
                OpenNextPendingIfAny();
        }
        
        public void CloseAllOpened()
        {
            if (!AnyOpened)
            {
                Debug.LogWarning("No popups opened");
                return;
            }
            
            while (AnyOpened)
            {
                var popup = GetTopOpened();
                _popupOpened.Remove(popup);
                popup.Close();
            }
            
            OpenNextPendingIfAny();
        }

        public static void ValidatePopup(T popup)
        {
            var invalidFields = popup.GetInvalidFields();
            if (invalidFields.Count == 0) return;
            var errorMessage = $"{popup.GetType().Name} has invalid fields: {string.Join(", ", invalidFields)}";
            throw new Exception(errorMessage);
        }
    }
}