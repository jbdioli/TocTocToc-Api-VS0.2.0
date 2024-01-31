using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TocTocToc.Models.Dto;
using TocTocToc.Models.Model;

namespace TocTocToc.Shared;

public class TextHandler
{
    private readonly TextModel _textModel;
    private readonly WordHandler _wordHandler;
    private readonly WordModel _wordModel = new();

    public TextHandler(TextModel textModel)
    {
        _wordHandler = new WordHandler(_wordModel);
        _textModel = textModel;
        if (_textModel.SeparatorList.Count == 0 ) _textModel.SeparatorList = [";", ","];
    }

    
    public void AddWordsToText()
    {
        if (_textModel == null) return;
        if (_textModel.SeparatorList.Count == 0) return;

        if (_textModel.Words.Count == 0)
        {
            _textModel.IsInvalid = true;
            return;
        }

        

        var words = _textModel.Words.Select(word => word.Word).ToList();

        if (words.Count == 0)
        {
            _textModel.IsInvalid = true;
            _textModel.Text = string.Empty;
            return;
        }

        var text = FormatTextFromWords(words);
        _textModel.Text = text;

        CheckTextValidity();
    }


    public async Task AddWordsFromTextTask()
    {
        if (string.IsNullOrWhiteSpace(_textModel.Text)) return;

        var words = await CheckWordsValidityFromTextTask();
        
        if (words == null) return;
        
        _textModel.IsInvalid = CheckTextValidity();

        foreach (var word in words)
        {
            var isExisting = _textModel.Words.Select(el => el.Word.Equals(word.Word)).LastOrDefault(el => el.Equals(true));
            if (!isExisting)
                _textModel.Words.Add(word);
        }
    }



    public async Task<List<WordModel>> EditTextHandler(string newTextValue, string previousText)
    {
        var words = new List<WordModel>();

        var oldWords = FindWordsInText(previousText);
        var newWords = FindWordsInText(newTextValue);

        if (oldWords.Count != newWords.Count) return words;

        //var wordsEdited = oldWords.Where(oldEl => !newWords.Any(newEl => newEl.Word.ToLower().Equals(oldEl.Word.ToLower()))).ToList();
        var wordsEdited = newWords.Where(newEl => !oldWords.Any(oldEl => oldEl.Word.ToLower().Equals(newEl.Word.ToLower()))).ToList();

        words = _textModel.Words;

        //var indexWordEdited = words.FindIndex(el => wordsEdited.Any(word => word.Word.ToLower().Equals(el.Word.ToLower())));
        var indexWordEdited = words.FindIndex(el => !newWords.Any(word => word.Word.ToLower().Equals(el.Word.ToLower())));

        words[indexWordEdited].IsInvalid = true;
        words[indexWordEdited].Word = wordsEdited[0].Word;

        _wordModel.Word = words[indexWordEdited].Word;
        _wordModel.Dictionary = new DictionaryDtoModel();
        _wordModel.IsInvalid = true;
        await _wordHandler.CheckWordValidity();

        words[indexWordEdited].IsInvalid = _wordModel.IsInvalid;

        return words;
    }



    public async Task<List<WordModel>> CheckWordsValidityTask(List<WordModel> words)
    {
        if (words == null) return [];

        var wordsVerified = new List<WordModel>();

        foreach (var word in words)
        {
            _wordHandler.Clear();
            _wordModel.Word = word.Word;
            if (word.IsInvalid)
            {
                await _wordHandler.CheckWordValidity();
            }

            wordsVerified.Add(new WordModel()
                {
                    IsInvalid = _wordModel.IsInvalid,
                    Word = _wordModel.Word,
                    Dictionary = _wordModel.Dictionary,
                    Log = _wordModel.Log
                }
            );
        }

        return wordsVerified;
    }


    private async Task<List<WordModel>> CheckWordsValidityFromTextTask()
    {
        if (_textModel.Words == null) return null;
        if (string.IsNullOrWhiteSpace(_textModel.Text)) return null;

        var wordsVerified = new List<WordModel>();
        var words = FindWordsInText(_textModel.Text);

        foreach (var word in words)
        {
            _wordHandler.Clear();
            _wordModel.Word = word.Word;
            if (word.IsInvalid)
            {
                await _wordHandler.CheckWordValidity();
            }

            wordsVerified.Add(new WordModel()
                {
                    IsInvalid = _wordModel.IsInvalid,
                    Word = _wordModel.Word,
                    Dictionary = _wordModel.Dictionary,
                    Log = _wordModel.Log
                }
            );
        }

        return wordsVerified;
    }


    public void DeleteWordsFromText(string previousText)
    {
        if (string.IsNullOrWhiteSpace(_textModel.Text))
        {
            return;
        }
        
        var currentText = _textModel.Text;

        if (currentText.Length >= previousText.Length) return;

        var words = FindWordsInText(currentText);

        var wordsToKeep = _textModel.Words.Where(elA => words.Exists(elB =>elB.Word.ToLower().Equals(elA.Word.ToLower()))).ToList();
        _textModel.Words.Clear();
        _textModel.Words.AddRange(wordsToKeep);

    }


    public void DeleteLastChar()
    {
        if (string.IsNullOrWhiteSpace(_textModel.Text))
        {
            return;
        }

        var textBuffer = _textModel.Text.Trim();
        var lastCharacter = textBuffer.Last();
        _textModel.Text = !char.IsLetter(lastCharacter) ? _textModel.Text.Remove(_textModel.Text.Length - 1, 1) : textBuffer;
    }



    public string GetCurrentWord( string text)
    {
        if (string.IsNullOrEmpty(text)) return string.Empty;

        var separatorIndexes = FindSeparatorIndexes(text);

        if (separatorIndexes.Count == 0) return text;

        var index = separatorIndexes.Count; 
        var word = text.Substring(separatorIndexes[index - 1] + 1);
        word = word.Trim();
        return word;
    }


    public void Clear()
    {
        _textModel.Text = string.Empty;
        _textModel.Words.Clear();
        _textModel.IsInvalid = true;
    }


    public (bool isEndingChar, bool isWrongChar) TextRecognition(string text)
    {
        var isWrongChar = false;

        var lastCharacter = text.Substring(text.Length - 1);
        
        var isEndingChar = _textModel.SeparatorList.Contains(lastCharacter);

        if (!isEndingChar)
        {
            isWrongChar = !WordHandler.IsCharAllowed(text);
        }

        return (isEndingChar, isWrongChar);
    }


    public List<WordModel> FindWordsInText(string text)
    {
        if (string.IsNullOrWhiteSpace(text)) return null;

        string word;
        var previousIndex = 0;

        var separatorIndexes = FindSeparatorIndexes(text);
        var words = new List<WordModel>();
        
        foreach (var index in separatorIndexes)
        {
            var length = index - previousIndex;
            word = text.Substring(previousIndex, length);
            previousIndex = index + 1;
            word = word.Trim(); // Clean from space character
            words.Add(new WordModel() { Word = word });
        }

        if (text.Length <= previousIndex) return words;
        
        word = text.Substring(previousIndex);
        word = word.Trim();
        words.Add(new WordModel() { Word = word });
        return words;
    }


    public string FormatText(string text)
    {
        var wordsModel = FindWordsInText(text);
        var words = wordsModel.Select(el => el.Word).ToList();
        var textFormatted = FormatTextFromWords(words);
        return textFormatted;
    }


    private string FormatTextFromWords(List<string> words)
    {
        if (_textModel.SeparatorList.Count == 0) return string.Empty;

        var separator = _textModel.SeparatorList[0];
        return string.Join(separator + " ", words) + separator + " ";
    }


    private bool CheckTextValidity()
    {
        return _textModel.Words.Select(el => el.IsInvalid.Equals(true)).LastOrDefault(el => el.Equals(true));
    }



    private List<int> FindSeparatorIndexes(string text)
    {
        var separatorList = _textModel.SeparatorList;

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

}


//if (wordsModel == null) return;
//throw new ArgumentNullException(nameof(wordsModel), "[ERROR] - In AddWordsToText -  Words object is empty or null");

//var lastChar = currentText[currentText.Length - 1];
//if (string.IsNullOrEmpty(lastChar.ToString()))
//{
//    lastChar = currentText[currentText.Length - 2];
//}

//var isEndingChar = _textModel.SeparatorList.Select(el => el.Equals(lastChar.ToString())).LastOrDefault(el => el.Equals(true));
//if (!isEndingChar) return;

//currentText = currentText.Trim();
//currentText = currentText.Substring(0, currentText.Length - 1);
//_textModel.Text = currentText;


