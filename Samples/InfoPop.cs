using System.Collections.Generic;
using WMK.PopupScheduler.Runtime;

namespace WMK.PopupScheduler.Samples
{
    public class InfoPop : PopupBase
    {
        public string Title { get; private set; } = string.Empty;

        public string Message { get; private set; } = string.Empty;

        public void SetTitle(string title) => Title = title;
        public void SetMessage(string message) => Message = message;

        public override List<string> GetInvalidFields()
        {
            var invalidFields = base.GetInvalidFields();
            if (string.IsNullOrEmpty(Message)) invalidFields.Add(nameof(Message));
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
            private readonly InfoPop _infoPop = new InfoPop();
            
            public Builder SetKey(PopupKey key)
            {
                _infoPop.SetKey(key);
                return this;
            }
            
            public Builder SetPriority(Priority priority)
            {
                _infoPop.SetPriority(priority);
                return this;
            }
            
            public Builder SetBehaviour(PopupBehaviour behaviour)
            {
                _infoPop.SetBehaviour(behaviour);
                return this;
            }
            
            public Builder SetTitle(string title)
            {
                _infoPop.SetTitle(title);
                return this;
            }
            
            public Builder SetMessage(string message)
            {
                _infoPop.SetMessage(message);
                return this;
            }
            
            public InfoPop Build()
            {
                PopupScheduler<PopupBase>.ValidatePopup(_infoPop);
                return _infoPop;
            }
        }
    }
}