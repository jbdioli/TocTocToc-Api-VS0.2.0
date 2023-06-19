using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TocTocToc.Interfaces;
using TocTocToc.Models.Dto;
using TocTocToc.Services;

namespace TocTocToc.Shared;

public class StatesItemRequest: IItemRequestChannel, ICopyItemsDtoHandler
{
    private readonly ItemsStorageService _itemsStorageService = new();
    private List<ItemDtoModel> _itemsDto = new();
    private List<StateDtoModel> _statesDto = new();

    public async Task<List<ItemDtoModel>> GetItemsAsync(ItemRequestDtoModel itemRequestDto)
    {
        _statesDto = new List<StateDtoModel>();

        if (itemRequestDto == null)
        {
            throw new System.ArgumentNullException("", "[ERROR] - In StatesItemRequest, RequestedItem can't be empty or null");
        }

        var buffer = _itemsStorageService.GetStatesAsync(new CountryDtoModel() {Id = itemRequestDto.RequestedId, Country = itemRequestDto.RequestedItem});
        _statesDto = await buffer;
        CopyToItems();
        return _itemsDto;
    }

    public async Task<List<ItemDtoModel>> SaveItemsAsync(List<ItemDtoModel> itemsDto)
    {
        var states = CopyFromItems(itemsDto) as List<StateDtoModel>;
        var responseStates = await _itemsStorageService.SaveStatesAsync(states);
        if (responseStates == null) return new List<ItemDtoModel>();

        _itemsDto = new List<ItemDtoModel>();
        _statesDto = new List<StateDtoModel>();
        _statesDto = responseStates;
        CopyToItems();

        return _itemsDto;
    }

    public void CopyToItems()
    {
        _itemsDto = new List<ItemDtoModel>();

        foreach (var itemDto in _statesDto.Select(state => new ItemDtoModel
                 {
                     Id = state.Id,
                     Item = state.State
                 }))
        {
            _itemsDto.Add(itemDto);
        }
    }

    public object CopyFromItems(List<ItemDtoModel> itemsDto)
    {
        return itemsDto.Where(el => el.Id == 0).Select(item => new StateDtoModel() { Id = item.Id, IdCountries = item.IdParents, State = item.Item }).ToList();
    }
}