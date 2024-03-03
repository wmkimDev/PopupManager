using System.Collections.Generic;
using WMK.PopupScheduler.Runtime;

namespace WMK.PopupScheduler.Samples
{
    /// <summary>
    /// Manages popups by their keys, providing a higher-level interface to PopupService.
    /// </summary>
    public class PopupManager
    {
        private readonly PopupQueue<PopupBase> _popupQueue = new();
        private readonly Dictionary<PopupKey, PopupBase> _popups = new();
        
        public bool IsOpened(PopupKey key) => _popups.TryGetValue(key, out var popup) && _popupQueue.IsOpened(popup);
        public bool IsPending(PopupKey key) => _popups.TryGetValue(key, out var popup) && _popupQueue.IsPending(popup);
        public bool IsHandled(PopupKey key) => _popups.TryGetValue(key, out var popup) && _popupQueue.IsHandled(popup);
        public PopupState GetState(PopupKey key) => _popups.TryGetValue(key, out var popup) ? _popupQueue.GetState(popup) : PopupState.UnHandled;
        public PopupBase GetTopOpened() => _popupQueue.GetTopOpened();
        public PopupBase Get(PopupKey key) => _popups.GetValueOrDefault(key);
        
        public void Schedule(PopupBase popup)
        {
            _popupQueue.Schedule(popup);
            _popups[popup.Key] = popup;
        }
        
        public void UnSchedule(PopupKey key)
        {
            if (_popups.TryGetValue(key, out var popup))
            {
                _popupQueue.UnSchedule(popup);
                _popups.Remove(key);
            }
        }
        
        public void ClearAllPending() => _popupQueue.ClearAllPending();
        public void Close(PopupKey key)
        {
            if (_popups.TryGetValue(key, out var popup))
            {
                _popupQueue.Close(popup);
                _popups.Remove(key);
            }
        }
        
        public void CloseTopOpened() => _popupQueue.CloseTopOpened();
        public void CloseAllOpened() => _popupQueue.CloseAllOpened();
    }
}