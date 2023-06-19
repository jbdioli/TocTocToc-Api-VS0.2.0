using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TocTocToc.Interfaces;
using TocTocToc.Models.Dto;
using TocTocToc.Services;

namespace TocTocToc.Shared;

public class HousingTypesItemRequest: IItemRequestChannel, ICopyItemsDtoHandler
{
    private readonly ItemsStorageService _itemsStorageService = new();
    private List<ItemDtoModel> _itemsDto = new();
    private List<HousingTypeDtoModel> _housingTypesDto = new();



    public async Task<List<ItemDtoModel>> GetItemsAsync(ItemRequestDtoModel itemRequestDto)
    {
        _housingTypesDto = new List<HousingTypeDtoModel>();

        _housingTypesDto = await _itemsStorageService.GetHousingTypesAsync();
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

        foreach (var itemDto in _housingTypesDto.Select(housingType => new ItemDtoModel
                 {
                     Id = housingType.Id,
                     Item = housingType.Type
                 }))
        {
            _itemsDto.Add(itemDto);
        }
    }


    public object CopyFromItems(List<ItemDtoModel> itemsDto)
    {
        return itemsDto.Where(el => el.Id == 0).Select(item => new HousingTypeDtoModel() { Id = item.Id, Type = item.Item }).ToList();

    }
}