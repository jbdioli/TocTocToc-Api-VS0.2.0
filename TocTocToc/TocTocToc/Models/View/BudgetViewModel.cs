using System;
using CommunityToolkit.Mvvm.ComponentModel;

namespace TocTocToc.Models.View;

public partial class BudgetViewModel : ObservableObject
{
    [ObservableProperty]
    private string _budget;

    [ObservableProperty]
    private DateTime _startDate;

    [ObservableProperty]
    private string _duration;

    [ObservableProperty]
    private DateTime _endDate;

    [ObservableProperty]
    private int _budgetMini = 1;

    [ObservableProperty]
    private int _budgetMaxi = 500;

    [ObservableProperty]
    private bool _isWrongTime = false;

    [ObservableProperty]
    private bool _isBudgetEntry = false;

    [ObservableProperty]
    private bool _isDurationMissing = false;
}