using Newtonsoft.Json;
using System;

namespace TocTocToc.Models.Dto;

public class BudgetDtoModel
{
    [JsonProperty("budget")]
    public double Budget { get; set; }

    [JsonProperty("startDate")]
    public DateTime StartDate { get; set; }

    [JsonProperty("duration")]
    public int Duration { get; set; }

    [JsonProperty("endDate")]
    public DateTime EndDate { get; set; }

    [JsonProperty("budgetMini")]
    public int BudgetMini { get; set; } = 1;

    [JsonProperty("budgetMaxi")]
    public int BudgetMaxi { get; set; } = 500;

    [JsonProperty("isPayed")]
    public bool IsPayed { get; set; } = false;

    [JsonProperty("isEPayBudget")]
    public bool IsEPayBudget { get; set; } = false;
}