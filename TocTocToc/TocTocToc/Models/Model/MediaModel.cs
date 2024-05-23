using CommunityToolkit.Mvvm.ComponentModel;
using TocTocToc.Models.View;

namespace TocTocToc.Models.Model;

public partial class MediaModel: BaseViewModel
{
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(DisplayVideo))]
    private string _video;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(DisplayImage))]
    private string _image;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(DisplayVideo))]
    private string _path;

    public string DisplayVideo => $"{_path}{_video}";
    public string DisplayImage => $"{_path}{_image}";


}