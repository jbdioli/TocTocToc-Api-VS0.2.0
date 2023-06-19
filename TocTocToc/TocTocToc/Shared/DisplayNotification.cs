using TocTocToc.Interfaces;
using Xamarin.Forms;

namespace TocTocToc.Shared;

public class DisplayNotification: INotificationChannel
{
    public async void SendMessageAsync(Message message)
    {
        await Application.Current.MainPage.DisplayAlert(message.MessageTitle, message.MessageBody, message.MessageValidation);
    }
}