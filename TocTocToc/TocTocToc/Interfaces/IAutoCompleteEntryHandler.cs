using System.Threading.Tasks;
using Xamarin.Forms;

namespace TocTocToc.Interfaces;

public interface IAutoCompleteEntryHandler
{
    public void TextChanged(TextChangedEventArgs e);
    public void ItemTapped(object sender, ItemTappedEventArgs e);
    public Task TextCompleted(object sender);
    public Task Unfocused();
}