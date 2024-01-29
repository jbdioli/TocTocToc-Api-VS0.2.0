using System.Linq;
using System.Threading.Tasks;
using TocTocToc.ENumerations;
using TocTocToc.Models.Dto;
using TocTocToc.Models.Model;

namespace TocTocToc.Shared;

public class WordHandler
{

    private readonly WordModel _word;
    private readonly WordDefinitionHandler _wordDefinitionHandler;

    public WordHandler(WordModel word)
    {
        _word = word;
        _wordDefinitionHandler = new WordDefinitionHandler(_word);
    }


    public async Task CheckWordValidity()
    {
        await CheckWordDefinition();
        if (_word.IsInvalid)
        {
            return;
        }

        FormatWord();
    }

    public static bool IsCharAllowed(string text)
    {
        if (string.IsNullOrWhiteSpace(text)) return false;

        //var textBuffer = text.Trim();
        //var lastCharacter = textBuffer.Last();
        //return char.IsLetter(lastCharacter);
        //return !lastCharacter.All(c => (char.IsLetter(c) || c == (int)EDecimalCharacter.Space || c == (int)EDecimalCharacter.Hyphen || c == (int)EDecimalCharacter.Apostrophe));

        var lastCharacter = text.Substring(text.Length - 1);
        return lastCharacter.All(c => (char.IsLetter(c) || c == (int)EDecimalCharacter.Space || c == (int)EDecimalCharacter.Hyphen || c == (int)EDecimalCharacter.Apostrophe));
    }


    private async Task CheckWordDefinition()
    {
        if (string.IsNullOrWhiteSpace(_word.Word))
        {
            return;
        }

        await _wordDefinitionHandler.GetWordDefinition();
    }


    private void CapitalizeFirstLetter()
    {
        var word = string.IsNullOrEmpty(_word.Dictionary.Word) ? _word.Word : _word.Dictionary.Word;

        if (string.IsNullOrWhiteSpace(word)) return;

        word = word.Length switch
        {
            0 => word,
            1 => char.ToUpper(word[0]).ToString(),
            _ => char.ToUpper(word[0]) + word.Substring(1)
        };

        _word.Word = word;
    }

    public void DeleteWord()
    {
        if (string.IsNullOrWhiteSpace(_word.Word))
        {
            return;
        }

        _word.Word = string.Empty;
        ClearWordDefinition();
    }


    public void DeleteLastChar()
    {
        if (string.IsNullOrWhiteSpace(_word.Word))
        {
            return;
        }

        var textBuffer = _word.Word.Trim();
        var lastCharacter = textBuffer.Last();
        _word.Word = !char.IsLetter(lastCharacter) ? _word.Word.Remove(_word.Word.Length - 1, 1) : textBuffer;
        ClearWordDefinition();
    }


    public void FormatWord()
    {
        _word.Word = _word.Word.Trim();
        CapitalizeFirstLetter();
    }

    public void Clear()
    {
        _word.Word = string.Empty;
        ClearWordDefinition();
    }

    private void ClearWordDefinition()
    {
        _word.Log = string.Empty;
        _word.IsInvalid = true;
        _word.Dictionary = new DictionaryDtoModel();
    }

    

}