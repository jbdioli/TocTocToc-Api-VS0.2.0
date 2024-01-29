using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TocTocToc.Interfaces;
using TocTocToc.Models.Dto;
using TocTocToc.Services;

namespace TocTocToc.Shared;

public class InterestsItemRequest: IItemRequestChannel, ICopyItemsDtoHandler
{

    private readonly ItemsStorageService _itemsStorageService = new();
    private List<ItemDtoModel> _itemsDto = [];
    private List<InterestDtoModel> _interestsDto = [];

    public async Task<List<ItemDtoModel>> GetItemsAsync(ItemRequestDtoModel itemRequestDto)
    {
        _interestsDto = [];

        _interestsDto = await _itemsStorageService.GetInterestsAsync();
        CopyToItems();
        return _itemsDto;
    }

    public async Task<List<ItemDtoModel>> SaveItemsAsync(List<ItemDtoModel> itemsDto)
    {
        var interests = CopyFromItems(itemsDto) as List<InterestDtoModel>;
        if (interests is { Count: 0 }) return [];

        var responseInterests = await _itemsStorageService.SaveInterestsAsync(interests);
        if (responseInterests == null) return [];

        _itemsDto = [];
        _interestsDto = [];
        _interestsDto = responseInterests;
        CopyToItems();

        return _itemsDto;
    }

    public void CopyToItems()
    {
        _itemsDto = [];

        foreach (var itemDto in _interestsDto.Select(interest => new ItemDtoModel
                 {
                     Id = interest.Id,
                     Item = interest.Interest
                 }))
        {
            _itemsDto.Add(itemDto);
        }
    }

    public object CopyFromItems(List<ItemDtoModel> itemsDto)
    {
        return itemsDto.Where(el => el.Id == 0).Select(item => new InterestDtoModel() { Id = item.Id, Interest = item.Item }).ToList();

    }
}