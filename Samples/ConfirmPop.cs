using System;
using System.Collections.Generic;
using UnityEngine.Serialization;
using WMK.PopupScheduler.Runtime;

namespace WMK.PopupScheduler.Samples
{
    public class ConfirmPop : PopupBase
    {
        public string Title { get; private set; } = string.Empty;
        public string Message { get; private set; } = string.Empty;
        
        public string OkText { get; private set; } = "OK";

        public Action OnConfirm;
        
        public void SetTitle(string title) => Title = title;
        public void SetMessage(string message) => Message = message;
        public void SetOkText(string okText) => OkText = okText;
        public void AddOnOkListener(Action onConfirm) => OnConfirm += onConfirm;
        
        public override List<string> GetInvalidFields()
        {
            var invalidFields = base.GetInvalidFields();
            if (string.IsNullOrEmpty(Message)) invalidFields.Add(nameof(Message));
            if (string.IsNullOrEmpty(OkText)) invalidFields.Add(nameof(OkText));
            return invalidFields;
        }
        
        protected override void OnOpen()
        {
            
        }
        
        protected override void OnClose()
        {
            
        }
        
        public static Builder New() => new();
        public class Builder
        {
            private readonly ConfirmPop _confirmPop = new ConfirmPop();
            
            public Builder SetKey(PopupKey key)
            {
                _confirmPop.SetKey(key);
                return this;
            }
            
            public Builder SetPriority(Priority priority)
            {
                _confirmPop.SetPriority(priority);
                return this;
            }
            
            public Builder SetBehaviour(PopupBehaviour behaviour)
            {
                _confirmPop.SetBehaviour(behaviour);
                return this;
            }
            
            public Builder SetTitle(string title)
            {
                _confirmPop.SetTitle(title);
                return this;
            }
            
            public Builder SetMessage(string message)
            {
                _confirmPop.SetMessage(message);
                return this;
            }
            
            public Builder SetOkText(string okText)
            {
                _confirmPop.SetOkText(okText);
                return this;
            }
            
            public Builder AddOnOkListener(Action onOk)
            {
                _confirmPop.AddOnOkListener(onOk);
                return this;
            }
            
            public ConfirmPop Build()
            {
                PopupScheduler<PopupBase>.ValidatePopup(_confirmPop);
                return _confirmPop;
            }
        }
    }
}