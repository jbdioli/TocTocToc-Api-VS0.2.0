using System.Threading.Tasks;
using TocTocToc.Interfaces;
using TocTocToc.Models.Model;
using Xamarin.Forms;

namespace TocTocToc.Shared;

public class AutoCompleteEntryHandler: IAutoCompleteEntryHandler
{
    private readonly IAutoCompeteEntry _autoCompeteEntry;

    protected AutoCompleteEntryHandler()
    {

    }

    public AutoCompleteEntryHandler(IAutoCompeteEntry autoCompeteEntry)
    {
        _autoCompeteEntry = autoCompeteEntry;
    }


    public async Task TextChanged(TextChangedEventArgs e, int cursorPosition)
    {
        await _autoCompeteEntry.TextChanged(e, cursorPosition);
    }

    public async Task ItemTapped(ItemModel itemModel)
    {
        await _autoCompeteEntry.ItemTapped(itemModel);
    }


    public async Task TextCompleted()
    {
        await _autoCompeteEntry.TextCompleted();
    }

    public async Task Unfocused()
    {
        await _autoCompeteEntry.Unfocused();
    }
}