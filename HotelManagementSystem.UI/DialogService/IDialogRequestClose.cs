using System;

namespace HotelManagementSystem.UI.DialogService
{
    public interface IDialogRequestClose
    {
        event EventHandler<DialogCloseRequestedEventArgs> CloseRequested;
    }
}
