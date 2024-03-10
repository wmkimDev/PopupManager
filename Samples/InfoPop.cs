using System.Collections.Generic;
using UnityEngine;
using WMK.PopupScheduler.Runtime;

namespace WMK.PopupScheduler.Samples
{
    [CreateAssetMenu(fileName = "InfoPop", menuName = "Popup/Common/InfoPop")]
    public class InfoPop : PopupBase
    {
        [SerializeField]
        private string _title = string.Empty;
        [SerializeField]
        private string _message = string.Empty;
        
        public string Title => _title;

        public string Message => _message;

        public void SetTitle(string title) => _title = title;
        public void SetMessage(string message) => _message = message;

        public override List<string> GetInvalidFields()
        {
            var invalidFields = base.GetInvalidFields();
            if (string.IsNullOrEmpty(Message)) invalidFields.Add(nameof(Message));
            return invalidFields;
        }
        
        public static Builder New() => new();
        public class Builder : Builder<Builder, InfoPop>
        {
            public Builder SetTitle(string title)
            {
                Popup.SetTitle(title);
                return this;
            }
            
            public Builder SetMessage(string message)
            {
                Popup.SetMessage(message);
                return this;
            }
            
            public override InfoPop Build()
            {
                PopupScheduler<PopupBase>.ValidatePopup(Popup);
                return Popup;
            }
        }
    }
}