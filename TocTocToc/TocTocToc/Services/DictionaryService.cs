using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using TocTocToc.Models.Dto;
using TocTocToc.Shared;

namespace TocTocToc.Services;

public class DictionaryService
{
    private readonly WordDtoModel _wordDto;
    public DictionaryService(WordDtoModel wordDto)
    {
        _wordDto = wordDto;
        _wordDto.Dictionary = new DictionaryDtoModel();
    }


    public async Task FindWordDefinition()
    {
        //const bool isFile = false;
        //var httpMethods = new HttpMethods(isFile);

        var setting = LocalStorageService.GetSetting();
        var language = setting.Language;

        if (language.ToLower() != "en")
        {
            _wordDto.Log = "[WARNING] - no international language yet";
            _wordDto.Dictionary.Word = _wordDto.WordRequested;
            return;
        }
           

        var url = $"https://api.dictionaryapi.dev/api/v2/entries/{language}/" + _wordDto.WordRequested;

        var dictionaries = await HttpMethods.HttpGetAsync<List<DictionaryDtoModel>>(url, null);
        if (dictionaries == null)
        {
            _wordDto.Dictionary = new DictionaryDtoModel();
            return;
        }
        _wordDto.Dictionary = dictionaries[0];

    }
}