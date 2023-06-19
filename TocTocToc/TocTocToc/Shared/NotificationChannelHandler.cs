using TocTocToc.ENumerations;
using TocTocToc.Interfaces;

namespace TocTocToc.Shared;

public class NotificationChannelHandler: INotificationChannelHandler
{
    private readonly INotificationChannel _notificationChannel;


    public NotificationChannelHandler(INotificationChannel notificationChannel)
    {
        _notificationChannel = notificationChannel;
    }

    public void SendNotification(ENotificationType eNotificationType, string customMessage)
    {
        var message = new Message();
        message.MessageHandler(eNotificationType, customMessage);
        _notificationChannel.SendMessageAsync(message);

    }
}