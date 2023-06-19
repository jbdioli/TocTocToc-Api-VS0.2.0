using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TocTocToc.Interfaces;
using TocTocToc.Models.Dto;
using TocTocToc.Services;

namespace TocTocToc.Shared;

public class MaritalStatusItemRequest: IItemRequestChannel, ICopyItemsDtoHandler
{
    private readonly ItemsStorageService _itemsStorageService = new();
    private List<ItemDtoModel> _itemsDto = new();
    private List<MaritalStatusDtoModel> _maritalStatusDto= new();


    public async Task<List<ItemDtoModel>> GetItemsAsync(ItemRequestDtoModel itemRequestDto)
    {
        _maritalStatusDto = new List<MaritalStatusDtoModel>();

        _maritalStatusDto = await _itemsStorageService.GetMaritalStatusAsync();
        CopyToItems();
        return _itemsDto;
    }

    public Task<List<ItemDtoModel>> SaveItemsAsync(List<ItemDtoModel> itemsDto)
    {
        return null;
    }

    public void CopyToItems()
    {
        _itemsDto = new List<ItemDtoModel>();

        foreach (var itemDto in _maritalStatusDto.Select(maritalStatus => new ItemDtoModel
                 {
                     Id = maritalStatus.Id,
                     Item = maritalStatus.MaritalStatus
                 }))
        {
            _itemsDto.Add(itemDto);
        }
    }

    public object CopyFromItems(List<ItemDtoModel> itemsDto)
    {
        return itemsDto.Where(el => el.Id == 0).Select(item => new MaritalStatusDtoModel() { Id = item.Id, MaritalStatus = item.Item }).ToList();

    }
}