namespace TocTocToc.Models.Dto;

public class WordDtoModel
{
    public string WordRequested { get; set; }

    public bool Invalid { get; set; } = true;

    public string Log { get; set; }

    public DictionaryDtoModel Dictionary { get; set; }
}