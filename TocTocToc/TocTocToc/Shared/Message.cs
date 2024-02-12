using System.Resources;
using TocTocToc.ENumerations;
using TocTocToc.Interfaces;
using TocTocToc.Resx;

namespace TocTocToc.Shared;

public class Message: IMessage
{
    private readonly ResourceManager _translate = new(typeof(AppResources));

    public string MessageBody { get; set; } = "No Message";
    public string MessageTitle { get; set; } = "";
    public string MessageValidation { get; set; } = "OK";

    public Message()
    {
        //var text = AppResources.ResourceManager.GetString("IncorrectWordDefinition");
    }

    public void MessageHandler(ENotificationType eNotificationType, string customMessage)
    {
        switch (eNotificationType)
        {
            case ENotificationType.IncorrectWordDefinition:
                SetMessage(_translate.GetString(nameof(ENotificationType.IncorrectWordDefinition)));
                SetMessageType(_translate.GetString(nameof(EMessageType.Error)));
                SetMessageValidation("OK");
                break;
            case ENotificationType.IncorrectWordSpelling:
                SetMessage(_translate.GetString(nameof(ENotificationType.IncorrectWordSpelling)));
                SetMessageType(_translate.GetString(nameof(EMessageType.Error)));
                SetMessageValidation("OK");
                break; 
            case ENotificationType.IncorrectValidWord:
                SetMessage(_translate.GetString(nameof(ENotificationType.IncorrectValidWord)));
                SetMessageType(_translate.GetString(nameof(EMessageType.Error)));
                SetMessageValidation("OK");
                break;
            case ENotificationType.IncorrectChar:
                SetMessage(_translate.GetString(nameof(ENotificationType.IncorrectChar)));
                SetMessageType(_translate.GetString(nameof(EMessageType.Error)));
                SetMessageValidation("OK");
                break;
            case ENotificationType.IsActiveAddress:
                SetMessage(customMessage + " " + _translate.GetString(nameof(ENotificationType.IsActiveAddress)));
                SetMessageType(_translate.GetString(nameof(EMessageType.Info)));
                SetMessageValidation("OK");
                break;
            case ENotificationType.IsDeleteActiveAddressInvalid:
                SetMessage(_translate.GetString(nameof(ENotificationType.IsDeleteActiveAddressInvalid)));
                SetMessageType(_translate.GetString(nameof(EMessageType.Warning)));
                SetMessageValidation("OK");
                break;
            case ENotificationType.IsEmptyAddressInvalid:
                SetMessage(_translate.GetString(nameof(ENotificationType.IsEmptyAddressInvalid)));
                SetMessageType(_translate.GetString(nameof(EMessageType.Warning)));
                SetMessageValidation("OK");
                break;
            case ENotificationType.IsOneAddressNeeded:
                SetMessage(_translate.GetString(nameof(ENotificationType.IsOneAddressNeeded)));
                SetMessageType(_translate.GetString(nameof(EMessageType.Warning)));
                SetMessageValidation("OK");
                break;
            case ENotificationType.HttpError500:
                SetMessage(_translate.GetString(nameof(ENotificationType.HttpError500)));
                SetMessageType(_translate.GetString(nameof(EMessageType.Error)));
                SetMessageValidation("OK");
                break;
            case ENotificationType.IsPayedNeed:
                SetMessage(_translate.GetString(nameof(ENotificationType.IsPayedNeed)));
                SetMessageType(_translate.GetString(nameof(EMessageType.Error)));
                SetMessageValidation("OK");
                break;
            case ENotificationType.IsAreaSelectedIncomplete:
                SetMessage(_translate.GetString(nameof(ENotificationType.IsAreaSelectedIncomplete)));
                SetMessageType(_translate.GetString(nameof(EMessageType.Error)));
                SetMessageValidation("OK");
                break;
            case ENotificationType.HttpError503:
                SetMessage(_translate.GetString(nameof(ENotificationType.HttpError503)));
                SetMessageType(_translate.GetString(nameof(EMessageType.Error)));
                SetMessageValidation("OK");
                break;
        }
    }

    public void GetMessage(ENotificationType eNotificationType)
    {
        // Get message form table translation
    }

    private void SetMessage(string message)
    {
        MessageBody = message;
    }

    private void SetMessageType(string messageType)
    {
        MessageTitle = messageType;
    }

    private void SetMessageValidation(string messageValidation)
    {
        MessageValidation = messageValidation;
    }
}