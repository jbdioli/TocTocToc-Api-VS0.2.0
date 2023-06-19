using System;

namespace TocTocToc.Models.Dto;

public class BudgetDtoModel
{
    public double Budget { get; set; }

    public DateTime StartDate { get; set; }

    public int Duration { get; set; }

    public DateTime EndDate { get; set; }

    public int BudgetMini { get; set; } = 1;

    public int BudgetMaxi { get; set; } = 500;

    public bool IsPayed { get; set; } = false;

    public bool IsEPayBudget { get; set; } = false;
}