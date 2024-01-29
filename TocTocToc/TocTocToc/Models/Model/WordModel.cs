using TocTocToc.Models.Dto;

namespace TocTocToc.Models.Model;

public class WordModel
{
    public string Word { get; set; }

    public bool IsInvalid { get; set; } = true;

    public string Log { get; set; }

    public DictionaryDtoModel Dictionary { get; set; }
}