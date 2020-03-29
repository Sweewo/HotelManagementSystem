using System;

namespace HotelManagementSystem.UI.DialogService
{
    public class DialogCloseRequestedEventArgs : EventArgs
    {
        public bool? DialogResult { get; }

        public DialogCloseRequestedEventArgs(bool dialogResult)
        {
            DialogResult = dialogResult;
        }
    }
}
