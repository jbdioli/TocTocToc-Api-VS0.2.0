using System.Threading.Tasks;

namespace TocTocToc.Interfaces;

public interface ITokenHandler
{
    public Task<string> GetBearerAsync();
}