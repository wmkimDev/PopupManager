using System;
using System.Collections.Generic;
using WMK.PopupScheduler.Runtime;

namespace WMK.PopupScheduler.Samples
{
    public abstract class PopupBase : IPopup, IEquatable<PopupBase>
    {
        public PopupKey Key { get; private set; } = PopupKey.None;

        public Priority Priority { get; private set; } = Priority.Normal;

        public PopupBehaviour Behaviour { get; private set; } = PopupBehaviour.Pend;

        protected void SetKey(PopupKey key) => Key = key;
        protected void SetPriority(Priority priority) => Priority = priority;
        protected void SetBehaviour(PopupBehaviour behaviour) => Behaviour = behaviour;
        
        public event Action<PopupBase> OnOpened;
        public event Action<PopupBase> OnClosed;

        public virtual List<string> GetInvalidFields()
        {
            var invalidFields = new List<string>();
            if (Key == PopupKey.None) invalidFields.Add(nameof(Key));
            return invalidFields;
        }

        public void Open()
        {
            OnOpen();
            OnOpened?.Invoke(this);
        }

        public void Close()
        {
            OnClose();
            OnClosed?.Invoke(this);
        }
        
        protected abstract void OnOpen();
        protected abstract void OnClose();

        public bool Equals(PopupBase other)
        {
            return other != null && Key == other.Key;
        }
    }
}