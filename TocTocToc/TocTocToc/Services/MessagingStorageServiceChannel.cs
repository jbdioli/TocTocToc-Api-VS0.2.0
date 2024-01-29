using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TocTocToc.Interfaces;
using TocTocToc.Models.Dto;
using TocTocToc.Shared;
using Xamarin.Forms;

namespace TocTocToc.Services;

public class MessagingStorageServiceChannel: IStorageServiceChannel
{

    private readonly string _url = WebConstants.Url;
    private readonly string _userId = LocalStorageService.GetUserId();


    public async Task<T> GetDataAsync<T>()
    {
        var result = default(T);



        if (result == null) throw new ArgumentNullException(nameof(result));

        result ??= (T)Activator.CreateInstance(typeof(T));

        if (result is MessagingDtoModel messaging)
        {

            var messages = new List<MessageDtoModel>
            {
                new()
                {
                    Date = new DateTime(), Message = "bla bla 1", MessageId = "e3f86607-5c20-4729-914b-415ba386b20b", User = new UserDtoModel()
                },
                new()
                {
                    Date = new DateTime(), Message = "bla bla 2", MessageId = "A3f86607-5c20-4729-914b-315ba386b20b", User = new UserDtoModel()
                }
            };

            messaging = new MessagingDtoModel(){IsGroup = false, Messages = messages, MessagingId = "Av6671ac-cc98-4880-bbc5-74eb5438080b"};

        }

        return result;
    }

    public Task<TB> SaveDataAsync<TA, TB>(TA data)
    {
        throw new NotImplementedException();
    }

    public Task<TB> UpdateDataAsync<TA, TB>(TA data)
    {
        throw new NotImplementedException();
    }

    public Task<T> DeleteDataAsync<T>(string publicId)
    {
        throw new NotImplementedException();
    }

    public Task<T> SaveMediaAsync<T>(string image)
    {
        throw new NotImplementedException();
    }

    public Task<T> UpdateMediaAsync<T>(string image, string publicId)
    {
        throw new NotImplementedException();
    }

    public Task<TB> GenericStorageRequestAsync<TA, TB>(Enum eNum, TA value)
    {
        throw new NotImplementedException();
    }
}