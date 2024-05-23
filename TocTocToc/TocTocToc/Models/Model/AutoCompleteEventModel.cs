using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TocTocToc.Models.Model;

public class AutoCompleteEventModel
{
    public bool ItemTappedDisable { get; set; } = false;
    public bool TextChangedDisable { get; set; } = false;
    public bool TextCompletedDisable { get; set; } = false;
    public bool UnfocusedDisable { get; set; } = false;

    public static async Task CheckEventOrder(IEnumerable<Task> tasks)
    {
        await Task.Delay(10);
        try
        {
            await Task.WhenAll(tasks);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            //throw;
        }
    }
}