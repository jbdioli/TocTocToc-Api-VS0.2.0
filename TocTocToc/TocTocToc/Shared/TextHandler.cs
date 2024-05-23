using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TocTocToc.Models.Dto;
using TocTocToc.Models.Model;

namespace TocTocToc.Shared;

public class TextHandler
{
    private readonly TextModel _textModel;

    public TextHandler(TextModel textModel)
    {
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


        var currentWordsModel = _textModel.Words;
        var wordsFromText = FindWordsInText(_textModel.Text);

        //var words = currentWords.Union(wordsFromText).DistinctBy(el => el.Word); // in .Net 8
        var uniqueWordsModel = wordsFromText.Where(tWord => currentWordsModel.All(cWord => !string.Equals(tWord.Word, cWord.Word, StringComparison.CurrentCultureIgnoreCase))).ToList();
        currentWordsModel.AddRange(uniqueWordsModel);
        //currentWordsModel = currentWordsModel.OrderBy(el => el.Word).ToList();

        if (currentWordsModel.Count == 0)
        {
            return;
        }

        currentWordsModel = FormatWords(currentWordsModel);

        var words = currentWordsModel.Select(el => el.Word).ToList();

        var separator = _textModel.SeparatorList[0];
        var text = string.Join(separator + " ", words) + separator + " ";
        
        _textModel.Text = text;

    }



    public async Task AddWordsFromTextTask()
    {
        var isReformatText = false;

        if (string.IsNullOrWhiteSpace(_textModel.Text)) return;

        var words = await FindValidWordsFromTextTask();
         
        if (words == null) return;
        _textModel.Words.Clear();

        foreach (var word in words)
        {
            var isExisting = _textModel.Words.Select(el => el.Word.ToLower().Equals(word.Word.ToLower())).LastOrDefault(el => el.Equals(true));
            if (!isExisting)
            {
                _textModel.Words.Add(word);
            }
            else
            {
                isReformatText = true;
            }
        }

        _textModel.IsInvalid = !CheckIsValidWords();

        if (isReformatText)
        {
            var wordsBuffer = _textModel.Words.Select(word => word.Word).ToList();
            _textModel.Text = FormatTextFromWords(wordsBuffer);
        }
    }



    public (bool isEdited, ErrorModel error) EditTextHandler(string newTextValue, string previousText)
    {
        if (_textModel.Words == null || _textModel.Words.Count == 0) return (false, new ErrorModel());

        var oldWords = FindWordsInText(previousText);
        var newWords = FindWordsInText(newTextValue);

        if (oldWords.Count != newWords.Count) return (false, null);

        var wordsEdited = newWords.Where(newEl => !oldWords.Any(oldEl => oldEl.Word.ToLower().Equals(newEl.Word.ToLower()))).ToList();

        foreach (var word in wordsEdited)
        {
            var isAllowed = WordHandler.IsCharAllowed(word.Word, null);
            if (isAllowed) continue;
            var error = new ErrorModel { IsWrongChar = true, IsWrongWord = false };
            return (false, error);
        }

        var indexWordEdited = _textModel.Words.FindIndex(el => !newWords.Any(word => word.Word.ToLower().Equals(el.Word.ToLower())));

        if (indexWordEdited < 0) return (false, new ErrorModel());

        _textModel.IsInvalid = true;
        _textModel.Words[indexWordEdited].IsInvalid = true;
        _textModel.Words[indexWordEdited].Word = wordsEdited[0].Word;
        return (true, null);
    }


    public void RefreshTextHandler()
    {
        var currentText = _textModel.Text;
        var words = FindWordsInText(currentText);
        var oldWords = _textModel.Words;

        var wordsEdited = words.Where(newEl => !oldWords.Any(oldEl => oldEl.Word.ToLower().Equals(newEl.Word.ToLower()))).ToList();

        var indexWordEdited = _textModel.Words.FindIndex(el => !words.Any(word => word.Word.ToLower().Equals(el.Word.ToLower())));

        if (indexWordEdited  < 0) return;

        _textModel.IsInvalid = true;
        _textModel.Words[indexWordEdited].IsInvalid = true;
        _textModel.Words[indexWordEdited].Word = wordsEdited[0].Word;

    }


    public async Task CheckWordsValidityTask()
    {
        var wordModel = new WordModel();
        var wordHandler = new WordHandler(wordModel);

        var words = _textModel.Words;

        if (words == null || words.Count == 0) return;

        foreach (var word in words.Select((value, index) => (value, index)))
        {
            wordHandler.ClearModel();
            wordModel.Word = word.value.Word;
            if (!word.value.IsInvalid) continue;

            await wordHandler.CheckWordValidityTask();
            words[word.index].IsInvalid = wordModel.IsInvalid;
            words[word.index].Word = wordModel.Word;
            words[word.index].Dictionary = wordModel.Dictionary;
            words[word.index].Log = wordModel.Log;
        }

    }


    public bool DeleteWordsFromText()
    {
        if (string.IsNullOrWhiteSpace(_textModel.Text))
        {
            return false;
        }
        
        var currentText = _textModel.Text;

        if (string.IsNullOrWhiteSpace(currentText)) return false;

        var words = FindWordsInText(currentText);

        //var isEqualWords = false;

        //foreach (var isEquals in _textModel.Words.SelectMany(word => words, (word, w) => word.Word.ToLower().Equals(w.Word.ToLower())).Where(isEquals => isEquals))
        //{
        //    isEqualWords = true;
        //}

        //if (isEqualWords) return false;

        if (words.Count >= _textModel.Words.Count) return false;

        

        var wordsKept = _textModel.Words.Where(elA => words.Exists(elB =>elB.Word.ToLower().Equals(elA.Word.ToLower()))).ToList();
        _textModel.Words.Clear();
        _textModel.Words.AddRange(wordsKept);
        return true;

    }


    public void DeleteWrongChar()
    {
        if (string.IsNullOrWhiteSpace(_textModel.Text))
        {
            return;
        }

        var newText = string.Empty;

        foreach (var charFromText in _textModel.Text.Select((value, index) => (value, index)))
        {
            var isValidChar = IsCharAllowed(charFromText.value.ToString(), null);
            if (isValidChar) continue;

            newText = _textModel.Text.Remove(charFromText.index, 1);
        }

        _textModel.Text = newText;

    }


    public void DeleteWrongWords()
    {
        var indexes = Enumerable.Range(0, _textModel.Words.Count).Where(i => _textModel.Words[i].IsInvalid == true).ToList();
        foreach (var index in indexes)
        {
            _textModel.Words.RemoveAt(index);
        }
    }


    private bool IsCharAllowed(string text, List<string> extraCharacterAllowed)
    {
        if (string.IsNullOrEmpty(text)) return false;

        var isValid = false;
        var separators = _textModel.SeparatorList;
        var extraCharacters = new List<string>();

        if (extraCharacterAllowed is { Count: > 0 })
        {
            extraCharacters.AddRange(separators);
            extraCharacters.AddRange(extraCharacterAllowed);
        }
        else
        {
            extraCharacters.AddRange(separators);
        }

        var words = FindWordsInText(text);


        foreach (var word in words)
        {
            if (text.Length == 1 && string.IsNullOrEmpty(word.Word))
            {
                isValid = WordHandler.IsCharAllowed(text, extraCharacters);
                return isValid;
            }

            isValid = WordHandler.IsCharAllowed(word.Word, extraCharacters);
            if (!isValid) return false;
        }


        //foreach (var character in text.Select(charFromText => charFromText.ToString()))
        //{
        //    isValid = character.All(c => (char.IsLetter(c) || c == (int)EDecimalCharacter.Space || c == (int)EDecimalCharacter.Hyphen || c == (int)EDecimalCharacter.Apostrophe));
        //    if (isValid) continue;
        //    isValid = separators.Contains(character);
        //}

        return isValid;
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


    public (bool isEndingChar, ErrorModel error) TextRecognition(string text)
    {
        var error = new ErrorModel();

        text = text.Trim();
        var lastCharacter = text.Substring(text.Length - 1);

        var isEndingChar = _textModel.SeparatorList.Contains(lastCharacter);

        if (!isEndingChar)
        {
            error.IsWrongChar = !IsCharAllowed(text, null);
        }
        else
        {
            error = null;
        }

        return (isEndingChar, error);
    }


    private async Task<List<WordModel>> FindValidWordsFromTextTask()
    {
        var wordToCheck = new WordModel();
        var wordHandler = new WordHandler(wordToCheck);

        //if (_textModel.Words == null) return null;
        if (string.IsNullOrWhiteSpace(_textModel.Text)) return null;

        var wordsVerified = new List<WordModel>();
        var words = FindWordsInText(_textModel.Text);

        foreach (var word in words)
        {
            wordHandler.ClearModel();
            wordToCheck.Word = word.Word;
            if (word.IsInvalid)
            {
                await wordHandler.CheckWordValidityTask();
            }

            wordsVerified.Add(new WordModel()
                {
                    IsInvalid = wordToCheck.IsInvalid,
                    Word = wordToCheck.Word,
                    Dictionary = wordToCheck.Dictionary,
                    Log = wordToCheck.Log
                }
            );
        }

        return wordsVerified;
    }


    private List<WordModel> FindWordsInText(string text)
    {
        if (string.IsNullOrEmpty(text)) return null;

        string word;
        var previousIndex = 0;

        var separatorIndexes = FindSeparatorIndexes(text);
        var words = new List<WordModel>();

        if (text.Equals(" "))
        {
            words.Add(new WordModel(){Dictionary = new DictionaryDtoModel(), IsInvalid = true, Word = text});
            return words;
        }
        
        text = text.Trim();

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
        if (string.IsNullOrWhiteSpace(text)) return string.Empty;
        
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


    private static List<WordModel> FormatWords(List<WordModel> wordsModel)
    {
        var wordModelBuffer = new WordModel();
        var wordHandler = new WordHandler(wordModelBuffer);
        var wordsModelFormatted = new List<WordModel>();

        foreach (var model in wordsModel)
        {
            wordHandler.ClearModel();
            wordModelBuffer.Dictionary = model.Dictionary ?? new DictionaryDtoModel();
            wordModelBuffer.IsInvalid = model.IsInvalid;
            wordModelBuffer.Log = model.Log;
            wordModelBuffer.Word = model.Word;

            wordHandler.FormatWord();
            wordsModelFormatted.Add(new WordModel() { Dictionary = wordModelBuffer.Dictionary, IsInvalid = wordModelBuffer.IsInvalid, Log = wordModelBuffer.Log, Word = wordModelBuffer.Word });
        }

        return wordsModelFormatted;
    }


    private bool CheckIsValidWords()
    {
        var isValidated = false;

        if (_textModel.Words is { Count: > 0 })
        {
            isValidated = _textModel.Words.All(el => el.IsInvalid.Equals(false));
        }

        return isValidated;

        //return _textModel.Words.Select(el => el.IsInvalid.Equals(true)).LastOrDefault(el => el.Equals(true));
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


    public void DeleteInvalidWords()
    {
        var words = _textModel.Words;
        words.RemoveAll(el => el.IsInvalid);
    }
}



public class WordsModelComparer : IComparer
{
    int IComparer.Compare(object objectA, object objectB)
    {
        var wordsModelA = (WordModel)objectA;
        var wordsModelB = (WordModel)objectB;


        if (wordsModelB != null && wordsModelA != null && wordsModelA.Word.ToLower().Equals(wordsModelB.Word.ToLower()))
            return 0;
        else
            return 1;
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


//public void DeleteLastChar()
//{
//    if (string.IsNullOrWhiteSpace(_textModel.Text))
//    {
//        return;
//    }

//    var textBuffer = _textModel.Text.Trim();
//    var lastCharacter = textBuffer.Last();
//    _textModel.Text = !char.IsLetter(lastCharacter) ? _textModel.Text.Remove(_textModel.Text.Length - 1, 1) : textBuffer;
//}