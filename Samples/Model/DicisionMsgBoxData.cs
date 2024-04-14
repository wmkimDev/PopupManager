using System;
using System.Collections.Generic;
using UnityEngine;
using WMK.PopupScheduler.Runtime;

namespace WMK.PopupScheduler.Samples.Model
{
    public class DecisionMsgBoxData : PopupBase
    {
        public string Title { get; private set; } = string.Empty;
        public string Message { get; private set; } = string.Empty;
        public string CancelText { get; private set; } = "Cancel";
        public string OkText { get; private set; } = "OK";

        public void SetTitle(string title) => Title = title;
        public void SetMessage(string message) => Message = message;
        public void SetCancelText(string cancelText) => CancelText = cancelText;
        public void SetOkText(string okText) => OkText = okText;
        protected override bool OnValidate() => !string.IsNullOrEmpty(Message);
        
        public static Builder New() => new();
        public class Builder : BaseBuilder<Builder, DecisionMsgBoxData>
        {
            public Builder SetTitle(string title)
            {
                instance.SetTitle(title);
                return this;
            }
            
            public Builder SetMessage(string message)
            {
                instance.SetMessage(message);
                return this;
            }
            
            public Builder SetCancelText(string cancelText)
            {
                instance.SetCancelText(cancelText);
                return this;
            }
            
            public Builder SetOkText(string okText)
            {
                instance.SetOkText(okText);
                return this;
            }
            
            public override DecisionMsgBoxData Build() => instance;
        }
    }
}