using System;
using PropertyChanged;

namespace TocTocToc.Models.View;

[AddINotifyPropertyChangedInterface]
public class BudgetViewModel
{
    public string Budget { get; set; }

    public DateTime StartDate { get; set; }

    public string Duration { get; set; }

    public DateTime EndDate { get; set; }

    public int BudgetMini { get; set; } = 1;

    public int BudgetMaxi { get; set; } = 500;

    public bool IsWrongTime { get; set; } = false;

    public bool IsBudgetEntry { get; set; } = false;

    public bool IsDurationMissing { get; set; } = false;
}