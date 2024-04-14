using System;
using System.Collections.Generic;
using UnityEngine;
using WMK.PopupScheduler.Runtime;

namespace WMK.PopupScheduler.Samples.Model
{
    public class OkMsgBoxData : PopupBase
    {
        public string Title { get; private set; } = string.Empty;
        public string Message  { get; private set; } = string.Empty;
        public string OkText { get; private set; } = "OK";

        public void SetTitle(string title) => Title = title;
        public void SetMessage(string message) => Message = message;
        public void SetOkText(string okText) => OkText = okText;
        protected override bool OnValidate() => !string.IsNullOrEmpty(Message);

        public new static Builder New() => new();
        public class Builder : BaseBuilder<Builder, OkMsgBoxData>
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
            
            public Builder SetOkText(string okText)
            {
                instance.SetOkText(okText);
                return this;
            }
            
            public override OkMsgBoxData Build() => instance;
        }
    }
}