using System;
using System.Linq;
using System.Threading.Tasks;
using TocTocToc.Models.Dto;

namespace TocTocToc.Shared;

public class WordHandler
{

    private readonly WordDtoModel _wordDto;
    private readonly WordDefinitionHandler _wordDefinitionHandler;

    public WordHandler(WordDtoModel wordDto)
    {
        _wordDto = wordDto;
        _wordDefinitionHandler = new WordDefinitionHandler(_wordDto);
    }


    public async Task CtrlWordValidity()
    {
        var isDefinitionValid = await IsDefinitionValid();
        if (!isDefinitionValid)
        {
            _wordDto.WordRequested = string.Empty;
            _wordDto.Invalid = true;
            return;
        }

        CapitalizeFirstLetter();
        _wordDto.Invalid = false;
    }


    private async Task<bool> IsDefinitionValid()
    {
        if (string.IsNullOrWhiteSpace(_wordDto.WordRequested))
        {
            throw new ArgumentNullException( nameof(_wordDto.WordRequested), "[ERROR] - In function IsDefinitionValid - Object WordHandler");
        }

        await _wordDefinitionHandler.GetWordDefinition();
        return !_wordDto.Invalid;
    }


    private void CapitalizeFirstLetter()
    {
        //var word = string.IsNullOrWhiteSpace(_wordDto.Log) ? _wordDto.Dictionary.Word : _wordDto.WordRequested;

        var word = string.IsNullOrEmpty(_wordDto.Dictionary.Word) ? _wordDto.WordRequested : _wordDto.Dictionary.Word;

        if (string.IsNullOrWhiteSpace(word)) return;

        word = word.Length switch
        {
            0 => word,
            1 => char.ToUpper(word[0]).ToString(),
            _ => char.ToUpper(word[0]) + word.Substring(1)
        };

        _wordDto.WordRequested = word;
    }

    public void CleaningWord()
    {
        if (string.IsNullOrWhiteSpace(_wordDto.WordRequested))
        {
            throw new ArgumentNullException(nameof(_wordDto.WordRequested), "[ERROR] - In function CleaningWord - Object WordHandler");
        }

        var textBuffer = _wordDto.WordRequested.Trim();
        var lastCharacter = textBuffer.Last();
        _wordDto.WordRequested = !char.IsLetter(lastCharacter) ? _wordDto.WordRequested.Remove(_wordDto.WordRequested.Length - 1, 1) : textBuffer;
    }

    public void CleaningWordDefinition()
    {
        _wordDto.Log = string.Empty;
        _wordDto.Invalid = true;
        _wordDto.Dictionary = new DictionaryDtoModel();
    }


}