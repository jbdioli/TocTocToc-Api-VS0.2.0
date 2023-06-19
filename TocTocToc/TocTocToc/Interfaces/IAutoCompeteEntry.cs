using System.Threading.Tasks;
using Xamarin.Forms;

namespace TocTocToc.Interfaces;

public interface IAutoCompeteEntry
{
    public void ItemTapped(object sender, ItemTappedEventArgs e);
    public Task TextChanged(TextChangedEventArgs e);
    public Task TextCompleted(object sender);
    public Task Unfocused();
    public bool IsNextOrEndingCharacter(string text);
    public void ShowSuggestions();
    public void HideSuggestions();


}