using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using WMK.PopupScheduler.Runtime;

namespace WMK.PopupScheduler.Samples
{
    [CreateAssetMenu(fileName = "OkPop", menuName = "Popup/Common/OkPop")]
    public class OkPop : PopupBase
    {
        [SerializeField]
        private string _title = string.Empty;
        [SerializeField]
        private string _message = string.Empty;
        [SerializeField]
        private string _okText = "OK";
        
        public string Title => _title;
        public string Message => _message;
        public string OkText => _okText;
        
        public event Action OnOk;

        public void SetTitle(string title) => _title = title;
        public void SetMessage(string message) => _message = message;
        public void SetOkText(string okText) => _okText = okText;
        public void ClearOnOkListener() => OnOk = null;
        
        public override List<string> GetInvalidFields()
        {
            var invalidFields = base.GetInvalidFields();
            if (string.IsNullOrEmpty(Message)) invalidFields.Add(nameof(Message));
            if (string.IsNullOrEmpty(OkText)) invalidFields.Add(nameof(OkText));
            return invalidFields;
        }
        
        public void Ok() => OnOk?.Invoke();
        
        public static Builder New() => new();
        public class Builder
        {
            private readonly OkPop _okPop = new OkPop();
            
            public Builder SetKey(PopupKey key)
            {
                _okPop.SetKey(key);
                return this;
            }
            
            public Builder SetPriority(Priority priority)
            {
                _okPop.SetPriority(priority);
                return this;
            }
            
            public Builder SetBehaviour(PopupBehaviour behaviour)
            {
                _okPop.SetBehaviour(behaviour);
                return this;
            }
            
            public Builder SetTitle(string title)
            {
                _okPop.SetTitle(title);
                return this;
            }
            
            public Builder SetMessage(string message)
            {
                _okPop.SetMessage(message);
                return this;
            }
            
            public Builder SetOkText(string okText)
            {
                _okPop.SetOkText(okText);
                return this;
            }
            
            public Builder AddOnOkListener(Action onOk)
            {
                _okPop.OnOk += onOk;
                return this;
            }
            
            public OkPop Build()
            {
                PopupScheduler<PopupBase>.ValidatePopup(_okPop);
                return _okPop;
            }
        }
    }
}