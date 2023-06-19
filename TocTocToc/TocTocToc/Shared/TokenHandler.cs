using System;
using System.Threading.Tasks;
using TocTocToc.Interfaces;

namespace TocTocToc.Shared;

public class TokenHandler: ITokenHandler
{

    private readonly IAuthServer _authServer;

    public TokenHandler(IAuthServer authServer)
    {
        _authServer = authServer;
    }

    public async Task<string> GetBearerAsync()
    {
        var bearer = await _authServer.RequestTokenAsync();
        if (bearer == null) throw new ArgumentNullException(nameof(bearer));

        return bearer;
    }


}