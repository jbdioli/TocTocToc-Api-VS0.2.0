using CommunityToolkit.Mvvm.ComponentModel;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace TocTocToc.Models.View;

public partial class ForumViewModel : BaseViewModel
{
    public AsyncCommand<object> TextChangedAsyncCommand { get; }
    public AsyncCommand UnfocusedAsyncCommand { get; }
    public AsyncCommand IsAsyncCommand { get; }

    [ObservableProperty]
    private string _textTest;

    [ObservableProperty]
    private int _textCursorPosition;

    [ObservableProperty]
    private bool _isEnabled;


    public ForumViewModel()
    {
        TextChangedAsyncCommand = new AsyncCommand<object>(TextChangedTask);
        UnfocusedAsyncCommand = new AsyncCommand(UnfocusedTask);
        IsAsyncCommand = new AsyncCommand(IsTextTask);
    }



    private Task TextChangedTask(object arg)
    {
        if (arg is not TextChangedEventArgs e) return Task.CompletedTask;

        var entryCursorPosition = TextCursorPosition;
        
        
        return Task.CompletedTask;
    }


    private Task UnfocusedTask()
    {
        return Task.CompletedTask;
    }


    private Task IsTextTask()
    {
        IsEnabled = true;

        return Task.CompletedTask;
    }


}