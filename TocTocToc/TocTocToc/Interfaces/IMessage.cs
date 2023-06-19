using TocTocToc.ENumerations;

namespace TocTocToc.Interfaces;

public interface IMessage
{
    public string MessageBody { get; set; }
    public string MessageTitle { get; set; }
    public string MessageValidation { get; set; }
    public void MessageHandler(ENotificationType eNotificationType, string customMessage);
    public void GetMessage(ENotificationType eNotificationType);
}