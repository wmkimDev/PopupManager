using System;
using System.Collections.Generic;
using WMK.PopupScheduler.Runtime;

namespace WMK.PopupScheduler.Samples
{
    public class OkMsgBox : PopupBase
    {
        public string Title { get; private set; } = string.Empty;
        public string Message { get; private set; } = string.Empty;
        
        public string OkText { get; private set; } = "OK";

        public Action OnOk;
        
        public void SetTitle(string title) => Title = title;
        public void SetMessage(string message) => Message = message;
        public void SetOkText(string okText) => OkText = okText;
        public void AddOnOkListener(Action onOk) => OnOk += onOk;
        
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
            private readonly OkMsgBox _okMsgBox = new OkMsgBox();
            
            public Builder SetKey(PopupKey key)
            {
                _okMsgBox.SetKey(key);
                return this;
            }
            
            public Builder SetPriority(Priority priority)
            {
                _okMsgBox.SetPriority(priority);
                return this;
            }
            
            public Builder SetBehaviour(PopupBehaviour behaviour)
            {
                _okMsgBox.SetBehaviour(behaviour);
                return this;
            }
            
            public Builder SetTitle(string title)
            {
                _okMsgBox.SetTitle(title);
                return this;
            }
            
            public Builder SetMessage(string message)
            {
                _okMsgBox.SetMessage(message);
                return this;
            }
            
            public Builder SetOkText(string okText)
            {
                _okMsgBox.SetOkText(okText);
                return this;
            }
            
            public Builder AddOnOkListener(Action onOk)
            {
                _okMsgBox.AddOnOkListener(onOk);
                return this;
            }
            
            public OkMsgBox Build()
            {
                PopupQueue<PopupBase>.ValidatePopup(_okMsgBox);
                return _okMsgBox;
            }
        }
    }
}