namespace TocTocToc.ENumerations;

public enum ENotificationType
{
    IncorrectWordSpelling = 1,
    IncorrectWordDefinition = 2, // The definition of the word is incorrect
    IncorrectSeparatorCharacter = 3, //"Please insert the words separated by ',' or ';' "
    IncorrectValidWord = 4, //"Please write a valid word"
    IsActiveAddress =  5,
    IsDeleteActiveAddressInvalid = 6,
    IsEmptyAddressInvalid = 7,
    IsOneAddressNeeded = 8,
    HttpError500 = 9,
    IsUpdatedMediaFalse = 10,
    IsPayedNeed = 11,
    IsAreaSelectedIncomplete = 12,
}
