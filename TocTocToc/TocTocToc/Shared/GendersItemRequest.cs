using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TocTocToc.Interfaces;
using TocTocToc.Models.Dto;
using TocTocToc.Services;

namespace TocTocToc.Shared;

public class GendersItemRequest: IItemRequestChannel, ICopyItemsDtoHandler
{
    private readonly ItemsStorageService _itemsStorageService = new();
    private List<ItemDtoModel> _itemsDto = new();
    private List<GenderDtoModel> _gendersDto = new();



    public async Task<List<ItemDtoModel>> GetItemsAsync(ItemRequestDtoModel itemRequestDto)
    {
        _gendersDto = new List<GenderDtoModel>();

        _gendersDto = await _itemsStorageService.GetGendersAsync();
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

        foreach (var itemDto in _gendersDto.Select(gender => new ItemDtoModel
                 {
                     Id = gender.Id,
                     Item = gender.Gender
                 }))
        {
            _itemsDto.Add(itemDto);
        }
    }

    public object CopyFromItems(List<ItemDtoModel> itemsDto)
    {
        return itemsDto.Where(el => el.Id == 0).Select(item => new GenderDtoModel() { Id = item.Id, Gender = item.Item }).ToList();
    }
}