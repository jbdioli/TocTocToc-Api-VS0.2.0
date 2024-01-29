using System.Threading.Tasks;
using Xamarin.Forms;

namespace TocTocToc.Shared;

public class FocusTriggerAction : TriggerAction<Entry>
{
    public bool Focused { get; set; }

    protected override void Invoke(Entry entry)
    {
        //await Task.Delay(1000);

        if (Focused)
        {
            entry.Focus();
        }
        else
        {
            entry.Unfocus();
        }
    }
}