using System;
using System.Collections.Generic;
using WMK.PopupScheduler.Runtime;

namespace WMK.PopupScheduler.Samples
{
    public class CancelOkMsgBox : PopupBase
    {
        public string Title { get; private set; } = string.Empty;
        public string Message { get; private set; } = string.Empty;
        public string CancelText { get; private set; } = "Cancel";
        public string OkText { get; private set; } = "OK";
        
        public Action OnCancel;
        public Action OnOk;
        
        public void SetTitle(string title) => Title = title;
        public void SetMessage(string message) => Message = message;
        public void SetCancelText(string cancelText) => CancelText = cancelText;
        public void SetOkText(string okText) => OkText = okText;
        public void AddOnCancelListener(Action onCancel) => OnCancel += onCancel;
        public void AddOnOkListener(Action onOk) => OnOk += onOk;
        
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
            private readonly CancelOkMsgBox _cancelOkMsgBox = new CancelOkMsgBox();
            
            public Builder SetKey(PopupKey key)
            {
                _cancelOkMsgBox.SetKey(key);
                return this;
            }
            
            public Builder SetPriority(Priority priority)
            {
                _cancelOkMsgBox.SetPriority(priority);
                return this;
            }
            
            public Builder SetBehaviour(PopupBehaviour behaviour)
            {
                _cancelOkMsgBox.SetBehaviour(behaviour);
                return this;
            }
            
            public Builder SetTitle(string title)
            {
                _cancelOkMsgBox.SetTitle(title);
                return this;
            }
            
            public Builder SetCancelText(string cancelText)
            {
                _cancelOkMsgBox.SetCancelText(cancelText);
                return this;
            }
            
            public Builder SetOkText(string okText)
            {
                _cancelOkMsgBox.SetOkText(okText);
                return this;
            }
            
            public Builder AddOnCancelListener(Action onCancel)
            {
                _cancelOkMsgBox.AddOnCancelListener(onCancel);
                return this;
            }
            
            public Builder AddOnOkListener(Action onOk)
            {
                _cancelOkMsgBox.AddOnOkListener(onOk);
                return this;
            }
            
            public CancelOkMsgBox Build()
            {
                PopupQueue<PopupBase>.ValidatePopup(_cancelOkMsgBox);
                return _cancelOkMsgBox;
            }
        }
    }
}