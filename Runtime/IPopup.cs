using System.Collections.Generic;

namespace WMK.PopupScheduler.Runtime
{
    public enum Priority
    {
        Low,
        Normal,
        High,
        Critical
    }
    
    public enum PopupBehaviour
    {
        Pend,
        StackOnTop,
        CloseTop,
        CloseSamePriority,
        CloseAll,
    }
    
    public interface IPopup
    {
        public Priority Priority { get; }
        public PopupBehaviour Behaviour { get; }
        public bool IsValid { get; }
        public void Open();
        public void Close();
    }
}
