using TocTocToc.Shared;

namespace TocTocToc.Interfaces;

public interface INotificationChannel
{
    public void SendMessageAsync(Message message);
}