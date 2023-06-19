using System.Collections.Generic;
using Newtonsoft.Json;

namespace TocTocToc.Models.Dto;

public class DictionaryDtoModel
{
    [JsonProperty("title")]
    public string Title { get; set; }

    [JsonProperty("message")]
    public string Message { get; set; }

    [JsonProperty("resolution")]
    public string Resolution { get; set; }

    [JsonProperty("word")]
    public string Word { get; set; }

    [JsonProperty("phonetic")]
    public string Phonetic { get; set; }

    [JsonProperty("phonetics")]
    public List<PhoneticDtoModel> Phonetics { get; set; }

    [JsonProperty("meanings")]
    public List<MeaningDtoModel> Meanings { get; set; }

    [JsonProperty("license")]
    public LicenseDtoModel License { get; set; }

    [JsonProperty("sourceUrls")]
    public List<string> SourceUrls { get; set; }
}

public class DefinitionDtoModel
{
    [JsonProperty("definition")]
    public string Definition { get; set; }

    [JsonProperty("synonyms")]
    public List<object> Synonyms { get; set; }

    [JsonProperty("antonyms")]
    public List<object> Antonyms { get; set; }
}

public class LicenseDtoModel
{
    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("url")]
    public string Url { get; set; }
}

public class MeaningDtoModel
{
    [JsonProperty("partOfSpeech")]
    public string PartOfSpeech { get; set; }

    [JsonProperty("definitions")]
    public List<DefinitionDtoModel> Definitions { get; set; }

    [JsonProperty("synonyms")]
    public List<object> Synonyms { get; set; }

    [JsonProperty("antonyms")]
    public List<object> Antonyms { get; set; }
}

public class PhoneticDtoModel
{
    [JsonProperty("text")]
    public string Text { get; set; }

    [JsonProperty("audio")]
    public string Audio { get; set; }
}