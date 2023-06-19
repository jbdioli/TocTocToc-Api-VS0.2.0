using System.Threading.Tasks;
using TocTocToc.Interfaces;
using Xamarin.Forms;

namespace TocTocToc.Shared;

public class AutoCompleteEntryHandler: IAutoCompleteEntryHandler
{
    private readonly IAutoCompeteEntry _autoCompeteEntry;


    public AutoCompleteEntryHandler(IAutoCompeteEntry autoCompeteEntry)
    {
        _autoCompeteEntry = autoCompeteEntry;
    }

    protected AutoCompleteEntryHandler()
    {

    }


    public void TextChanged(TextChangedEventArgs e)
    {
        _autoCompeteEntry.TextChanged(e);
    }

    public void ItemTapped(object sender, ItemTappedEventArgs e)
    {
        _autoCompeteEntry.ItemTapped(sender, e);
    }

    public async Task TextCompleted(object sender)
    {
        await _autoCompeteEntry.TextCompleted(sender);
    }

    public async Task Unfocused()
    {
        await _autoCompeteEntry.Unfocused();
    }
}