using System.Threading.Tasks;
using TocTocToc.Models.Model;
using Xamarin.Forms;

namespace TocTocToc.Interfaces;

public interface IAutoCompleteEntryHandler
{
    public Task TextChanged(TextChangedEventArgs e);
    public Task ItemTapped(ItemModel itemModel);
    public Task TextCompleted();
    public Task Unfocused();
}