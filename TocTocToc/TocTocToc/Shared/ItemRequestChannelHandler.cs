using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using TocTocToc.Interfaces;
using TocTocToc.Models.Dto;
using TocTocToc.Models.View;

namespace TocTocToc.Shared;

public class ItemRequestChannelHandler: IItemRequestChannelHandler
{
    private List<ItemDtoModel> _itemsDto = new();

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

    public ObservableCollection<ItemViewModel> ConverterToObservableCollection()
    {
        var itemsCollection = new ObservableCollection<ItemViewModel>();
        foreach (var itemDto in _itemsDto)
        {
            itemsCollection.Add(new ItemViewModel(){Id = itemDto.Id, Item = itemDto.Item});
        }

        return itemsCollection;
    }

}