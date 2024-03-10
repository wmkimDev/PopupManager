using System;
using System.Collections.Generic;
using UnityEngine;
using WMK.PopupScheduler.Runtime;

namespace WMK.PopupScheduler.Samples
{
    [CreateAssetMenu(fileName = "DecisionPop", menuName = "Popup/Common/DecisionPop")]
    public class DecisionPop : PopupBase
    {
        [SerializeField] 
        private string _title = string.Empty;
        [SerializeField]
        private string _message = string.Empty;
        [SerializeField]
        private string _cancelText = "Cancel";
        [SerializeField]
        private string _okText = "OK";
        
        public string Title => _title;
        public string Message => _message;
        public string CancelText => _cancelText;
        public string OkText => _okText;

        public event Action OnCancel;
        public event Action OnOk;

        public void SetTitle(string title) => _title = title;
        public void SetMessage(string message) => _message = message;
        public void SetCancelText(string cancelText) => _cancelText = cancelText;
        public void SetOkText(string okText) => _okText = okText;
        public void ClearOnCancelListener() => OnCancel = null;
        public void ClearOnOkListener() => OnOk = null;
        
        public override List<string> GetInvalidFields()
        {
            var invalidFields = base.GetInvalidFields();
            if (string.IsNullOrEmpty(Message)) invalidFields.Add(nameof(Message));
            if (string.IsNullOrEmpty(CancelText)) invalidFields.Add(nameof(CancelText));
            if (string.IsNullOrEmpty(OkText)) invalidFields.Add(nameof(OkText));
            return invalidFields;
        }
        
        public void Cancel() => OnCancel?.Invoke();
        public void Ok() => OnOk?.Invoke();
        
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
                _decisionPop.OnCancel += onCancel;
                return this;
            }
            
            public Builder AddOnOkListener(Action onOk)
            {
                _decisionPop.OnOk += onOk;
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