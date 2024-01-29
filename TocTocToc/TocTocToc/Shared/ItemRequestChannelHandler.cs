using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using TocTocToc.Interfaces;
using TocTocToc.Models.Dto;
using TocTocToc.Models.Model;

namespace TocTocToc.Shared;

public class ItemRequestChannelHandler: IItemRequestChannelHandler
{
    private List<ItemDtoModel> _itemsDto = [];

    private readonly IItemRequestChannel _itemRequestChannel;

    public ItemRequestChannelHandler(IItemRequestChannel itemRequestChannel)
    {
        _itemRequestChannel = itemRequestChannel;
    }

    public async Task<List<ItemDtoModel>> GetItemsAsync(ItemRequestDtoModel itemRequestDto)
    {
        _itemsDto = await _itemRequestChannel.GetItemsAsync(itemRequestDto);
        return _itemsDto;
    }

    public async Task<List<ItemDtoModel>> SaveItemsAsync(List<ItemDtoModel> itemsDto)
    {
        _itemsDto = await _itemRequestChannel.SaveItemsAsync(itemsDto);
        return _itemsDto;
    }

    public ObservableCollection<ItemModel> ConverterToObservableCollection()
    {
        var itemsCollection = new ObservableCollection<ItemModel>();
        foreach (var itemDto in _itemsDto)
        {
            itemsCollection.Add(new ItemModel(){Id = itemDto.Id, Item = itemDto.Item});
        }

        return itemsCollection;
    }

    public List<ItemModel> ConverterToModels()
    {
        var itemsModel = new List<ItemModel>();
        
        if (_itemsDto == null) return itemsModel;

        itemsModel.AddRange(_itemsDto.Select(itemDto => new ItemModel() { Id = itemDto.Id, Item = itemDto.Item, IdParents = itemDto.IdParents }));

        return itemsModel;
    }
}