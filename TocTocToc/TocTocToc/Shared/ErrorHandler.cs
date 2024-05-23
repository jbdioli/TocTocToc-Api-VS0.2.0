using TocTocToc.ENumerations;
using TocTocToc.Models.Dto;
using TocTocToc.Models.Model;

namespace TocTocToc.Shared;

public class ErrorHandler
{
    private static readonly NotificationChannelHandler NOTIFICATION_HANDLER = new(new DisplayNotification());


    //private bool IsManyWordsEntryHandelError(ErrorModel error, TextModel text, AutoCompleteEntryModel autoCompleteEntry)
    //{

    //    var textHandler = new TextHandler(text);

    //    var isTreatedError = true;

    //    if (error != null && (error.IsWrongChar || error.IsWrongWord))
    //    {
    //        autoCompleteEntry.EventOrder.TextChangedDisable = true;
    //        if (error.IsWrongChar)
    //        {
    //            textHandler.DeleteWrongChar();
    //            autoCompleteEntry.Text = text.Text;
    //            NOTIFICATION_HANDLER.SendNotification(ENotificationType.IncorrectChar, null);
    //        }

    //        if (error.IsWrongWord)
    //        {
    //            autoCompleteEntry.EntryItems.Clear();
    //            textHandler.DeleteWrongWords();
    //            foreach (var word in text.Words)
    //            {
    //                if (word.IsInvalid) continue;
    //                var item = new ItemModel();
    //                CopyModel.WordModelToItem(word, item);
    //                AddToEntryItems(item);
    //            }
    //            NOTIFICATION_HANDLER.SendNotification(ENotificationType.IncorrectValidWord, null);
    //        }
    //    }
    //    else
    //    {
    //        isTreatedError = false;
    //    }

    //    autoCompleteEntry.Error = error;
    //    return isTreatedError;
    //}
}