using System;
using System.Collections.Generic;
using WMK.PopupScheduler.Runtime;

namespace WMK.PopupScheduler.Samples
{
    public class DecisionPop : PopupBase
    {
        public string Title { get; private set; } = string.Empty;
        public string Message { get; private set; } = string.Empty;
        public string CancelText { get; private set; } = "Cancel";
        public string OkText { get; private set; } = "OK";
        
        public Action OnCancel;
        public Action OnConfirm;
        
        public void SetTitle(string title) => Title = title;
        public void SetMessage(string message) => Message = message;
        public void SetCancelText(string cancelText) => CancelText = cancelText;
        public void SetOkText(string okText) => OkText = okText;
        public void AddOnCancelListener(Action onCancel) => OnCancel += onCancel;
        public void AddOnOkListener(Action onConfirm) => OnConfirm += onConfirm;
        
        public override List<string> GetInvalidFields()
        {
            var invalidFields = base.GetInvalidFields();
            if (string.IsNullOrEmpty(Message)) invalidFields.Add(nameof(Message));
            if (string.IsNullOrEmpty(CancelText)) invalidFields.Add(nameof(CancelText));
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
            private readonly DecisionPop _decisionPop = new DecisionPop();
            
            public Builder SetKey(PopupKey key)
            {
                _decisionPop.SetKey(key);
                return this;
            }
            
            public Builder SetPriority(Priority priority)
            {
                _decisionPop.SetPriority(priority);
                return this;
            }
            
            public Builder SetBehaviour(PopupBehaviour behaviour)
            {
                _decisionPop.SetBehaviour(behaviour);
                return this;
            }
            
            public Builder SetTitle(string title)
            {
                _decisionPop.SetTitle(title);
                return this;
            }
            
            public Builder SetMessage(string message)
            {
                _decisionPop.SetMessage(message);
                return this;
            }
            
            public Builder SetCancelText(string cancelText)
            {
                _decisionPop.SetCancelText(cancelText);
                return this;
            }
            
            public Builder SetOkText(string okText)
            {
                _decisionPop.SetOkText(okText);
                return this;
            }
            
            public Builder AddOnCancelListener(Action onCancel)
            {
                _decisionPop.AddOnCancelListener(onCancel);
                return this;
            }
            
            public Builder AddOnOkListener(Action onOk)
            {
                _decisionPop.AddOnOkListener(onOk);
                return this;
            }
            
            public DecisionPop Build()
            {
                PopupScheduler<PopupBase>.ValidatePopup(_decisionPop);
                return _decisionPop;
            }
        }
    }
}