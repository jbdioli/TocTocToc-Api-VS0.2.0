using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TocTocToc.Interfaces;
using TocTocToc.Models.Dto;
using TocTocToc.Services;

namespace TocTocToc.Shared;

public class CountriesItemRequest: IItemRequestChannel, ICopyItemsDtoHandler
{

    private readonly ItemsStorageService _itemsStorageService = new();
    private List<ItemDtoModel> _itemsDto = new();
    private List<CountryDtoModel> _countriesDto = new();

    public CountriesItemRequest() {}

    public async Task<List<ItemDtoModel>> GetItemsAsync(ItemRequestDtoModel itemRequestDto)
    {
        _countriesDto = new List<CountryDtoModel>();

        var buffer = _itemsStorageService.GetCountriesAsync();
        _countriesDto = await buffer;
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

        foreach (var itemDto in _countriesDto.Select(country => new ItemDtoModel
                 {
                     Id = country.Id,
                     Item = country.Country
                 }))
        {
            _itemsDto.Add(itemDto);
        }
    }

    public object CopyFromItems(List<ItemDtoModel> itemsDto)
    {
        return itemsDto.Where(el => el.Id == 0).Select(item => new CountryDtoModel() { Id = item.Id, Country = item.Item }).ToList();
    }
}