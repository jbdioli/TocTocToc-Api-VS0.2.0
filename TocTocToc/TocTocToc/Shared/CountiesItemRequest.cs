using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TocTocToc.Interfaces;
using TocTocToc.Models.Dto;
using TocTocToc.Services;

namespace TocTocToc.Shared;

public class CountiesItemRequest : IItemRequestChannel, ICopyItemsDtoHandler
{
    private readonly ItemsStorageService _itemsStorageService = new();
    private List<ItemDtoModel> _itemsDto = new();
    private List<CountyDtoModel> _countiesDto = new();


    public async Task<List<ItemDtoModel>> GetItemsAsync(ItemRequestDtoModel itemRequestDto)
    {
        _countiesDto = new List<CountyDtoModel>();

        if (itemRequestDto == null)
        {
            throw new System.ArgumentNullException("", "[ERROR] - In StatesItemRequest, RequestedItem can't be empty or null");
        }

        var buffer = _itemsStorageService.GetCountiesAsync(new StateDtoModel() { Id = itemRequestDto.RequestedId, State = itemRequestDto.RequestedItem });
        _countiesDto = await buffer;
        CopyToItems();
        return _itemsDto;

    }

    public async Task<List<ItemDtoModel>> SaveItemsAsync(List<ItemDtoModel> itemsDto)
    {
        var counties = CopyFromItems(itemsDto) as List<CountyDtoModel>;
        var responseCounties = await _itemsStorageService.SaveCountiesAsync(counties);
        if (responseCounties == null) return new List<ItemDtoModel>();

        _itemsDto = new List<ItemDtoModel>();
        _countiesDto = new List<CountyDtoModel>();
        _countiesDto = responseCounties;
        CopyToItems();

        return _itemsDto;

    }

    public void CopyToItems()
    {
        _itemsDto = new List<ItemDtoModel>();

        foreach (var itemDto in _countiesDto.Select(county => new ItemDtoModel
                 {
                     Id = county.Id,
                     Item = county.County
                 }))
        {
            _itemsDto.Add(itemDto);
        }

    }

    public object CopyFromItems(List<ItemDtoModel> itemsDto)
    {
        return itemsDto.Where(el => el.Id == 0).Select(item => new CountyDtoModel() { Id = item.Id, IdStates = item.IdParents, County = item.Item }).ToList();

    }
}