using System.Collections.Generic;

namespace TocTocToc.Models.Dto;

public class TextDtoModel
{
    public TextDtoModel()
    {
        Words = new List<WordDtoModel>();
        SeparatorList = new List<string>();
    }

    public string TextRequested { get; set; }
    public bool Invalid { get; set; } = true;
    public List<WordDtoModel> Words { get; set; }
    public List<string> SeparatorList { get; set; }

}