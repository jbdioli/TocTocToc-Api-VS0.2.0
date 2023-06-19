using System;
using System.Collections.Generic;
using System.Reactive.Subjects;
using System.Threading.Tasks;
using TocTocToc.ENumerations;

namespace TocTocToc.Interfaces;

public interface IHttpRequestChannelHandler
{
    public Task<T> GetHttpAsync<T>();
    public Task<TB> SaveHttpAsync<TA, TB>(TA data);
    public Task<TB> UpdateHttpAsync<TA, TB>(TA data);
    public Task<T> DeleteHttpAsync<T>(string publicId);
    public Task<T> SaveHttpMediaAsync<T>(string image);
    public Task<T> UpdateHttpMediaAsync<T>(string image, string publicId);
    public Task<TB> GenericHttpRequestAsync<TA, TB>(Enum eNum, TA value);
    //public ReplaySubject<TB> HandelHttpEvents<TA, TB>(EEventsHandler eEventHandler, TA value);

}