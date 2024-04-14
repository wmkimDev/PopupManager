using System;
using System.Collections.Generic;
using UnityEngine;
using WMK.PopupScheduler.Runtime;
using WMK.PopupScheduler.Samples.Model;

namespace WMK.PopupScheduler.Samples
{
    /// <summary>
    /// Manages popups by their keys, providing a higher-level interface to the PopupQueue.
    /// </summary>
    public class PopupManager
    {
        private readonly PopupScheduler<PopupBase> _popupScheduler = new();
        private readonly Dictionary<PopupKey, PopupBase> _popups = new();

        public event Action<PopupBase> OnPopupScheduled
        {
            add => _popupScheduler.OnPopupScheduled += value;
            remove => _popupScheduler.OnPopupScheduled -= value;
        }
        
        public event Action<PopupBase> OnPopupUnScheduled
        {
            add => _popupScheduler.OnPopupUnScheduled += value;
            remove => _popupScheduler.OnPopupUnScheduled -= value;
        }
        
        public event Action<PopupBase> BeforePopupOpen
        {
            add => _popupScheduler.BeforePopupOpen += value;
            remove => _popupScheduler.BeforePopupOpen -= value;
        }
        
        public bool IsOpened(PopupKey key) => _popups.TryGetValue(key, out var popup) && _popupScheduler.IsOpened(popup);
        public bool IsPending(PopupKey key) => _popups.TryGetValue(key, out var popup) && _popupScheduler.IsPending(popup);
        public bool IsHandled(PopupKey key) => _popups.TryGetValue(key, out var popup) && _popupScheduler.IsHandled(popup);
        public PopupState GetState(PopupKey key) => _popups.TryGetValue(key, out var popup) ? _popupScheduler.GetState(popup) : PopupState.UnHandled;
        public PopupBase GetTopOpened() => _popupScheduler.GetTopOpened();

        public PopupBase Get(PopupKey key)
        {
            if (_popups.TryGetValue(key, out var popup)) return popup;
            throw new KeyNotFoundException($"Popup with key {key} not found");
        }

        public void Schedule(PopupBase popup)
        {
            _popupScheduler.Schedule(popup);
            _popups[popup.Key] = popup;
        }
        
        public void UnSchedule(PopupKey key)
        {
            if (_popups.TryGetValue(key, out var popup))
            {
                _popupScheduler.UnSchedule(popup);
                _popups.Remove(key);
            }
            else
            {
                Debug.LogWarning($"Popup with key {key} not found");
            }
        }
        
        public void ClearAllPending() => _popupScheduler.ClearAllPending();
        public void Close(PopupKey key)
        {
            if (_popups.TryGetValue(key, out var popup))
            {
                _popupScheduler.Close(popup);
                _popups.Remove(key);
            }
            else
            {
                Debug.LogWarning($"Popup with key {key} not found");
            }
        }
        
        public void CloseTopOpened() => _popupScheduler.CloseTopOpened();
        public void CloseAllOpened() => _popupScheduler.CloseAllOpened();
    }
}