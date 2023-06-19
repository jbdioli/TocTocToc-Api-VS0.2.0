using System.Threading.Tasks;

namespace TocTocToc.Interfaces;

public interface IAuthServer
{
    public Task<string> RequestTokenAsync();
    public void Logout();
}