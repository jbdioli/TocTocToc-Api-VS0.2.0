using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TocTocToc.Models.Model;
using TocTocToc.Views;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace TocTocToc.Models.View;

public partial class BlogViewModel : BaseViewModel
{
    private ICommand Init { get; }
    //public AsyncCommand OpenAddOrHistoryBlogPageCommand { get; set; }

    public ObservableRangeCollection<PostModel> PostsCollection { get; set; }

    [ObservableProperty]
    private UserModel _userProfile;
    
    public BlogViewModel()
    {
        Init = new AsyncCommand(IniTask);
        Init.Execute(null);
        //OpenAddOrHistoryBlogPageCommand = new AsyncCommand(OpenAddOrHistoryBlogPageTask);
    }

    private Task IniTask()
    {
        PostsCollection =
        [
            new PostModel() { Title = "Title 1", Post = "bla bla bla 1", MediaList = [new MediaModel() {Path = "https://upload.wikimedia.org/wikipedia/commons/4/44/", Image = "Eva_Herzigova_1997.jpg" }] },
            new PostModel() { Title = "Title 2", Post = "bla bla bla 2", MediaList = [new MediaModel() {Path = "https://upload.wikimedia.org/wikipedia/commons/4/44/", Image = "Eva_Herzigova_1997.jpg" }] }
        ];
 
        var i = PostsCollection[0].MediaList[0].DisplayImage;

        return Task.CompletedTask;
    }


    [RelayCommand]
    private async Task OpenAddOrHistoryBlogPageTask()
    {
        await Application.Current.MainPage.Navigation.PushAsync(new BlogAddOrEditPage());
    }
    
}