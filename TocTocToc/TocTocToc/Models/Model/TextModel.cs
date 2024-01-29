using System.Collections.Generic;
using TocTocToc.Models.Dto;

namespace TocTocToc.Models.Model;

public class TextModel
{
    public string Text { get; set; } = string.Empty;
    public bool IsInvalid { get; set; } = true;
    public List<WordModel> Words { get; set; } = [];
    public List<string> SeparatorList { get; set; } = [];
}