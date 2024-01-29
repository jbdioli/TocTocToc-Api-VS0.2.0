using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using TocTocToc.Models.Dto;
using TocTocToc.Models.Model;
using TocTocToc.Shared;

namespace TocTocToc.Services;

public class DictionaryService
{
    private readonly WordModel _word;
    public DictionaryService(WordModel word)
    {
        _word = word;
        _word.Dictionary = new DictionaryDtoModel();
    }


    public async Task FindWordDefinition()
    {
        //const bool isFile = false;
        //var httpMethods = new HttpMethods(isFile);

        var setting = LocalStorageService.GetSetting();
        var language = setting.Language;

        if (language.ToLower() != "en")
        {
            _word.Log = "[WARNING] - no international language yet";
            _word.Dictionary.Word = _word.Word;
            return;
        }
           

        var url = $"https://api.dictionaryapi.dev/api/v2/entries/{language}/" + _word.Word;

        var dictionaries = await HttpMethods.HttpGetAsync<List<DictionaryDtoModel>>(url, null);
        if (dictionaries == null)
        {
            _word.Dictionary = new DictionaryDtoModel();
            return;
        }
        _word.Dictionary = dictionaries[0];

    }
}