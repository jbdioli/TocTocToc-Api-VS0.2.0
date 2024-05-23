using CommunityToolkit.Mvvm.ComponentModel;

namespace TocTocToc.Models.Model;

public partial class BlogModel : PostModel
{
    [ObservableProperty]
    private string _blogPostId;
}