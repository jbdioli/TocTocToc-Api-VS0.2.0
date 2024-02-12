namespace TocTocToc.ENumerations;

public enum ENotificationType
{
    IncorrectWordSpelling = 1,
    IncorrectWordDefinition = 2, // The definition of the word is incorrect
    IncorrectSeparatorCharacter = 3, //"Please insert the words separated by ',' or ';' "
    IncorrectValidWord = 4, //"Please write a valid word"
    IncorrectChar = 5, //"Please write a valid character"
    IsActiveAddress = 6,
    IsDeleteActiveAddressInvalid = 7,
    IsEmptyAddressInvalid = 8,
    IsOneAddressNeeded = 9,
    HttpError500 = 10,
    IsUpdatedMediaFalse = 11,
    IsPayedNeed = 12,
    IsAreaSelectedIncomplete = 13,
    HttpError503 = 14
}
