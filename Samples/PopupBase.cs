using System;
using System.Collections.Generic;
using UnityEngine;
using WMK.PopupScheduler.Runtime;

namespace WMK.PopupScheduler.Samples
{
    public abstract class PopupBase : ScriptableObject, IPopup, IEquatable<PopupBase>
    {
        [SerializeField] 
        private PopupKey _key = PopupKey.None;
        [SerializeField] 
        private Priority _priority = Priority.Normal;
        [SerializeField] 
        private PopupBehaviour _behaviour = PopupBehaviour.Pend;
        
        public PopupKey Key => _key;
        public Priority Priority => _priority;
        public PopupBehaviour Behaviour => _behaviour;
        
        public event Action OnOpen;
        public event Action OnClose;

        protected void SetKey(PopupKey key) => _key = key;
        protected void SetPriority(Priority priority) => _priority = priority;
        protected void SetBehaviour(PopupBehaviour behaviour) => _behaviour = behaviour;
        public void ClearOnOpenListener() => OnOpen = null;
        public void ClearOnCloseListener() => OnClose = null;

        public virtual List<string> GetInvalidFields()
        {
            var invalidFields = new List<string>();
            if (Key == PopupKey.None) invalidFields.Add(nameof(Key));
            return invalidFields;
        }

        public void Open() => OnOpen?.Invoke();
        public void Close() => OnClose?.Invoke();

        public bool Equals(PopupBase other)
        {
            return other != null && Key == other.Key;
        }
        
        public abstract class Builder<TBuilder, TPopup> where TBuilder : Builder<TBuilder, TPopup> where TPopup : PopupBase
        {
            protected readonly TPopup Popup = CreateInstance<TPopup>();
            
            public TBuilder SetKey(PopupKey key)
            {
                Popup.SetKey(key);
                return (TBuilder) this;
            }
            
            public TBuilder SetPriority(Priority priority)
            {
                Popup.SetPriority(priority);
                return (TBuilder) this;
            }
            
            public TBuilder SetBehaviour(PopupBehaviour behaviour)
            {
                Popup.SetBehaviour(behaviour);
                return (TBuilder) this;
            }
            
            public TBuilder AddOnOpenListener(Action onOpen)
            {
                Popup.OnOpen += onOpen;
                return (TBuilder) this;
            }
            
            public TBuilder AddOnCloseListener(Action onClose)
            {
                Popup.OnClose += onClose;
                return (TBuilder) this;
            }
            
            public virtual TPopup Build()
            {
                PopupScheduler<PopupBase>.ValidatePopup(Popup);
                return Popup;
            }
        }
    }
}