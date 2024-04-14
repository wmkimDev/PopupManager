using System;
using UnityEngine;
using WMK.PopupScheduler.Runtime;

namespace WMK.PopupScheduler.Samples.Model
{
    /// <summary>
    /// Contains the basic properties of a popup.
    /// </summary>
    public abstract class PopupBase : IPopup, IEquatable<PopupBase>
    {
        public bool IsValid => Key != PopupKey.None && OnValidate();
        public PopupKey Key { get; private set; } = PopupKey.None;
        public Priority Priority { get; private set; } = Priority.Normal;
        public PopupBehaviour Behaviour { get; private set; } = PopupBehaviour.Pend;
        public event Action OnOpen;
        public event Action OnClose;

        private void SetKey(PopupKey key) => Key = key;
        private void SetPriority(Priority priority) => Priority = priority;
        public void SetBehaviour(PopupBehaviour behaviour) => Behaviour = behaviour;
        public void ClearOnOpenListener() => OnOpen = null;
        public void ClearOnCloseListener() => OnClose = null;

        public void Open() => OnOpen?.Invoke();
        public void Close() => OnClose?.Invoke();
        protected abstract bool OnValidate();

        public bool Equals(PopupBase other)
        {
            return other != null && Key == other.Key;
        }
        
        public abstract class BaseBuilder<TBuilder, TPopup> where TBuilder : BaseBuilder<TBuilder, TPopup>, new() where TPopup : PopupBase, new()
        {
            protected readonly TPopup instance = new TPopup();

            public TBuilder SetKey(PopupKey key)
            {
                instance.SetKey(key);
                return this as TBuilder;
            }

            public TBuilder SetPriority(Priority priority)
            {
                instance.SetPriority(priority);
                return this as TBuilder;
            }

            public TBuilder SetBehaviour(PopupBehaviour behaviour)
            {
                instance.SetBehaviour(behaviour);
                return this as TBuilder;
            }

            public abstract TPopup Build();
        }
    }
}