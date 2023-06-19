using PropertyChanged;

namespace TocTocToc.Models.View;

[AddINotifyPropertyChangedInterface]
public class AgeViewModel
{
    public string AgeMaxi { get; set; }

    public string AgeMini { get; set; }

    public bool IsAllAge { get; set; } = false;

    public bool IsAgeMini { get; set; } = false;

    public bool IsAgeMaxi { get; set; } = false;

    public bool IsAgeValid { get; set; } = false;
}