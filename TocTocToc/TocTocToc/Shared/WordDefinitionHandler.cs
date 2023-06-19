using System.Linq;
using System.Threading.Tasks;
using TocTocToc.ENumerations;
using TocTocToc.Models.Dto;
using TocTocToc.Services;

namespace TocTocToc.Shared;

public class WordDefinitionHandler
{
    private readonly WordDtoModel _wordDto;

    private readonly DictionaryService _dictionaryService;

    private static readonly NotificationChannelHandler NotificationHandler = new(new DisplayNotification());

    public WordDefinitionHandler(WordDtoModel wordDto)
    {
        _wordDto = wordDto;
        _dictionaryService = new DictionaryService(_wordDto);
    }


    public async Task GetWordDefinition()
    {

        var isForbiddenCharacters = IsForbiddenCharacters();
        if (isForbiddenCharacters)
        {
            _wordDto.Invalid = true;
            return;
        }
        else
        {
            _wordDto.Invalid = false;
        }

        var isDefinition = await IsWordInDictionary();
        if (!isDefinition)
        {
            _wordDto.Invalid = true;
            return;
        }

        _wordDto.Invalid = false;
    }


    private bool IsForbiddenCharacters()
    {
        var word = _wordDto.WordRequested.Trim();
        if (string.IsNullOrWhiteSpace(word))
            return true;

        var alphaCharIsMatch = word.All(c => (char.IsLetter(c) || c == (int)EDecimalCharacter.Space || c == (int)EDecimalCharacter.Hyphen || c == (int)EDecimalCharacter.Apostrophe));
        //var alphaCharIsMatch = word.All(char.IsLetter);
        if (alphaCharIsMatch) return false;

        NotificationHandler.SendNotification(ENotificationType.IncorrectValidWord, null);

        return true;

    }

    private async Task<bool> IsWordInDictionary()
    {
        
        await _dictionaryService.FindWordDefinition();
        var dictionary = _wordDto.Dictionary;
        if (dictionary != null && !string.IsNullOrWhiteSpace(dictionary.Word)) return true;
        NotificationHandler.SendNotification(ENotificationType.IncorrectWordDefinition, null);
        return false;
    }
}


//public bool CheckTextValidity(string text)
//{
//    //const string authorizedChar = "^[a-zA-Z,#\\ ]*$";
//    //var alphaCharIsMatch = Regex.IsMatch("hello,#-", authorizedChar);
//    //var alphaCharIsMatch = text.All(c => ( c == 32 || c == 35 || c == 39 || c == 44 || c >= 65 && c <= 90 || c >= 97 && c <= 122));
//    //var alphaCharIsMatch = text.All(c => (char.IsLetter(c) || c == 32 || c == 35 || c == 39 || c == 44 || c == 59)); // any letter or space or # or ' or , or ; in decimal values
//    var alphaCharIsMatch = text.All(c => (char.IsLetter(c) || c == (int)EDecimalCharacter.Space || c == (int)EDecimalCharacter.Hash || c == (int)EDecimalCharacter.Apostrophe
//                                          || c == (int)EDecimalCharacter.Comma || c == (int)EDecimalCharacter.Semicolon));

//    NotificationHandler.SendNotification(ENotificationType.IncorrectSeparatorCharacter);

//    return false;

//}