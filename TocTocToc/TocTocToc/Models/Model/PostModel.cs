using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using TocTocToc.Models.View;

namespace TocTocToc.Models.Model;

public partial class PostModel : BaseViewModel
{
    [ObservableProperty]
    private List<MediaModel> _mediaList = [];

    [ObservableProperty]
    private string _title;

    [ObservableProperty]
    private DateTime _date;
    
    [ObservableProperty]
    private string _post;

    [ObservableProperty]
    private bool _isPin;
}