using System.Threading.Tasks;
using TocTocToc.Models.Dto;
using TocTocToc.Models.Model;
using Xamarin.Forms;

namespace TocTocToc.Interfaces;

public interface IAutoCompeteEntry
{
    public bool IsCompletedEntry { get; set; }
    public bool IsEditing { get; set; }

    //public Task ItemTapped(ItemModel itemModel);
    //public Task TextChanged(TextChangedEventArgs e);
    //public Task TextCompleted(object sender);
    public Task ItemTapped(ItemModel itemModel);
    public Task TextChanged(TextChangedEventArgs e);
    public Task TextCompleted();
    public Task Unfocused();
    //public bool IsNextOrEndingCharacter(string text);
    public void ShowSuggestions();
    public void HideSuggestions();


}