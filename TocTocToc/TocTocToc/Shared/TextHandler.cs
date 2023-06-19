using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TocTocToc.Models.Dto;

namespace TocTocToc.Shared;

public class TextHandler
{
    private readonly TextDtoModel _textDto;

    public TextHandler(TextDtoModel textDto)
    {
        _textDto = textDto;
    }

    public void AddWordsToTextRequested()
    {
        var wordsDto = _textDto.Words;

        if (wordsDto == null)
            throw new ArgumentNullException(nameof(wordsDto), "[ERROR] - In AddWordsToTextRequested -  Words object is empty or null");

        var words = wordsDto.Select(word => word.WordRequested).ToList();

        if (words.Count == 0)
        {
            _textDto.TextRequested = string.Empty;
            _textDto.Invalid = true;
            return;
        }

        _textDto.TextRequested = string.Join("; ", words) + "; ";

        _textDto.Invalid = wordsDto.Select(el => el.Invalid.Equals(true)).LastOrDefault(el => el.Equals(true));
    }

    public void AddWordsToWordsDto(List<WordDtoModel> words)
    {
        foreach (var word in words)
        {
            var isExisting = _textDto.Words.Select(el => el.WordRequested.Equals(word.WordRequested)).LastOrDefault(el => el.Equals(true));
            if (!isExisting)
                _textDto.Words.Add(word);
        }
    }



    public void DeleteWordsFromTextRequested(string previousText)
    {
        var currentText = _textDto.TextRequested;

        if (string.IsNullOrEmpty(currentText))
        {
            _textDto.Words.Clear();
            return;
        }

        if (currentText.Length >= previousText.Length) return;

        var lastChar = currentText[currentText.Length - 1];
        if (string.IsNullOrEmpty(lastChar.ToString()))
        {
            lastChar = currentText[currentText.Length - 2];
        }

        var isEndingChar = _textDto.SeparatorList.Select(el => el.Equals(lastChar.ToString())).LastOrDefault(el => el.Equals(true));
        if (!isEndingChar) return;
            
        currentText = currentText.Trim();
        currentText = currentText.Substring(0, currentText.Length - 1);
        _textDto.TextRequested = currentText;

        var words = FindWordsInText();
        if (words.Count >= _textDto.Words.Count) return;

        var wordsToKeep = _textDto.Words.Where(elA => words.Exists(elB =>elB.WordRequested.ToLower().Equals(elA.WordRequested.ToLower()))).ToList();
        _textDto.Words.Clear();
        _textDto.Words = wordsToKeep;

    }




    public async Task GetWordsFromTextRequested()
    {
        var words = FindWordsInText();
        AddWordsToWordsDto(words);
        await CheckWordsValidity();
        
    }


    public string GetLastWord()
    {
        var text = _textDto.TextRequested;
        var separatorIndexes = FindSeparatorIndexes(text);

        if (separatorIndexes.Count == 0) return text;

        var index = separatorIndexes.Count; 
        var word = text.Substring(separatorIndexes[index - 1] + 1);
        word = word.Trim();
        return word;
    }


    public void CleaningTextDefinition()
    {
        if (!string.IsNullOrEmpty(_textDto.TextRequested)) return;
        
        _textDto.Invalid = true;
        _textDto.Words.Clear();
    }


    private List<WordDtoModel> FindWordsInText()
    {
        if (string.IsNullOrWhiteSpace(_textDto.TextRequested))
            throw new ArgumentNullException("", "[ERROR] - In FindWordsInText - TextRequested is empty or null");

        var text = _textDto.TextRequested;
        string word;
        var previousIndex = 0;

        var separatorIndexes = FindSeparatorIndexes(text);
        var wordsBuffer = new List<WordDtoModel>();
        
        foreach (var index in separatorIndexes)
        {
            var length = index - previousIndex;
            word = text.Substring(previousIndex, length);
            previousIndex = index + 1;
            word = word.Trim(); // Clean from space character
            wordsBuffer.Add(new WordDtoModel() { WordRequested = word });
        }

        if (_textDto.TextRequested.Length <= previousIndex) return wordsBuffer;
        
        word = _textDto.TextRequested.Substring(previousIndex);
        word = word.Trim();
        wordsBuffer.Add(new WordDtoModel() { WordRequested = word });
        return wordsBuffer;
    }



    private List<int> FindSeparatorIndexes(string text)
    {

        if (!_textDto.SeparatorList.Any())
            throw new ArgumentNullException("", "[ERROR] - In FindSeparatorIndexes - SeparatorList is empty or null");

        var separatorList = _textDto.SeparatorList;

        var indexes = new List<int>();

        foreach (var result in separatorList.Select(separator =>
                     text.Select<char, int>((value, index) => value.Equals(char.Parse(separator)) ? index : -1)
                         .Where(index => index != -1).ToList()))
        {
            indexes.AddRange(result);
        }
        indexes.Sort();
        return indexes;
    }

    private async Task CheckWordsValidity()
    {
        foreach (var word in _textDto.Words)
        {
            if (string.IsNullOrEmpty(word.WordRequested)) continue;
            
            var wordHandler = new WordHandler(word);
            wordHandler.CleaningWord();
            if (word.Invalid)
                await wordHandler.CtrlWordValidity();
        }

        DeleteInvalidWords();
    }

    private void DeleteInvalidWords()
    {
        var deleteIndexes = (from result in _textDto.Words.Select((value, index) => new { index, value })
            let value = result.value
            let index = result.index
            where value.Invalid == true
            select index).ToList();

        foreach (var index in deleteIndexes)
        { 
            _textDto.Words.RemoveAt(index);
        }
    }


}