using System;
using System.Threading.Tasks;
using TocTocToc.Interfaces;

namespace TocTocToc.Shared;

public class HttpRequestChannelHandler: IHttpRequestChannelHandler
{
    private readonly IStorageServiceChannel _storageServiceChannel;

    public HttpRequestChannelHandler(IStorageServiceChannel storageServiceChannel)
    {
        _storageServiceChannel = storageServiceChannel;
    }


    public async Task<T> GetHttpAsync<T>()
    {
       var returnValue = await _storageServiceChannel.GetDataAsync<T>();

       return returnValue;
    }

    public async Task<TB> SaveHttpAsync<TA, TB>(TA data)
    {
        var returnValue = await _storageServiceChannel.SaveDataAsync<TA, TB>(data);
        
        return returnValue;
    }

    public async Task<TB> UpdateHttpAsync<TA, TB>(TA data)
    {
        var returnValue = await _storageServiceChannel.UpdateDataAsync<TA, TB>(data);

        return returnValue;
    }

    public async Task<T> DeleteHttpAsync<T>(string publicId)
    {
        var returnValue = await _storageServiceChannel.DeleteDataAsync<T>(publicId);

        return returnValue;
    }

    public async Task<T> SaveHttpMediaAsync<T>(string data)
    {
        var returnValue = await _storageServiceChannel.SaveMediaAsync<T>(data);

        return returnValue;
    }

    public async Task<T> UpdateHttpMediaAsync<T>(string image, string publicId)
    {
        var returnValue = await _storageServiceChannel.UpdateMediaAsync<T>(image, publicId);

        return returnValue;
    }


    public async Task<TB> GenericHttpRequestAsync<TA, TB>(Enum eNum, TA value)
    {
        var returnValue = await _storageServiceChannel.GenericStorageRequestAsync<TA, TB>(eNum, value);

        return returnValue;
    }

    //public ReplaySubject<TB> HandelHttpEvents<TA, TB>(EEventsHandler eEventsHandler, TA value)
    //{
    //    var returnValue = _storageServiceChannel.HandelStorageEvents<TA, TB>(eEventsHandler, value);

    //    return returnValue;
    //}
}