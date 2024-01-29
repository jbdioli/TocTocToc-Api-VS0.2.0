using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic;
using System.Threading.Tasks;
using TocTocToc.Models.Dto;
using TocTocToc.Models.Model;

namespace TocTocToc.Interfaces;

public interface IItemRequestChannelHandler
{
    public Task<List<ItemDtoModel>> GetItemsAsync(ItemRequestDtoModel itemRequestDto);
    public Task<List<ItemDtoModel>> SaveItemsAsync(List<ItemDtoModel> itemsDto);
    public ObservableCollection<ItemModel> ConverterToObservableCollection();
    public List<ItemModel> ConverterToModels();
}