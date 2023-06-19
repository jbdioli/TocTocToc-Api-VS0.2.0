using TocTocToc.ENumerations;

namespace TocTocToc.Interfaces;

public interface INotificationChannelHandler
{
    public void SendNotification(ENotificationType eNotificationType, string customMessage);
}