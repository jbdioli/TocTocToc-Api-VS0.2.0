using System;
using System.Collections.Generic;
using System.Reactive.Subjects;
using System.Threading.Tasks;
using TocTocToc.ENumerations;

namespace TocTocToc.Interfaces;

public interface IStorageServiceChannel
{
    public Task<T> GetDataAsync<T>();
    public Task<TB> SaveDataAsync<TA, TB>(TA data);
    public Task<TB> UpdateDataAsync<TA, TB>(TA data);
    public Task<T> DeleteDataAsync<T>(string publicId);
    public Task<T> SaveMediaAsync<T>(string image);
    public Task<T> UpdateMediaAsync<T>(string image, string publicId);
    public Task<TB> GenericStorageRequestAsync<TA, TB>(Enum eNum, TA value);
    //public ReplaySubject<TB> HandelStorageEvents<TA, TB>(EEventsHandler eEventHandler, TA value);
}