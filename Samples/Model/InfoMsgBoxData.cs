using System.Collections.Generic;
using UnityEngine;
using WMK.PopupScheduler.Runtime;

namespace WMK.PopupScheduler.Samples.Model
{
    public class InfoMsgBoxData : PopupBase
    {
        public string Title { get; private set; } = string.Empty;
        public string Message { get; private set; } = string.Empty;
        
        public void SetTitle(string title) => Title = title;
        public void SetMessage(string message) => Message = message;
        protected override bool OnValidate() => !string.IsNullOrEmpty(Message);
        
        public static Builder New() => new();
        public class Builder : BaseBuilder<Builder, InfoMsgBoxData>
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
            
            public override InfoMsgBoxData Build() => instance;
        }
    }
}