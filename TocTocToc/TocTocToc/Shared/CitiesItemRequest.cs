using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TocTocToc.Interfaces;
using TocTocToc.Models.Dto;
using TocTocToc.Services;

namespace TocTocToc.Shared;

public class CitiesItemRequest : IItemRequestChannel, ICopyItemsDtoHandler
{
    private readonly ItemsStorageService _itemsStorageService = new();
    private List<ItemDtoModel> _itemsDto = new();
    private List<CityDtoModel> _citiesDto = new();


    public async Task<List<ItemDtoModel>> GetItemsAsync(ItemRequestDtoModel itemRequestDto)
    {
        _citiesDto = new List<CityDtoModel>();

        if (itemRequestDto == null)
        {
            throw new System.ArgumentNullException("", "[ERROR] - In StatesItemRequest, RequestedItem can't be empty or null");
        }

        var buffer = _itemsStorageService.GetCitiesAsync(new CountyDtoModel() { Id = itemRequestDto.RequestedId, County = itemRequestDto.RequestedItem });
        _citiesDto = await buffer;
        CopyToItems();
        return _itemsDto;

    }

    public async Task<List<ItemDtoModel>> SaveItemsAsync(List<ItemDtoModel> itemsDto)
    {
        var cities = CopyFromItems(itemsDto) as List<CityDtoModel>;
        var responseCities = await _itemsStorageService.SaveCitiesAsync(cities);
        if (responseCities == null) return new List<ItemDtoModel>();

        _itemsDto = new List<ItemDtoModel>();
        _citiesDto = new List<CityDtoModel>();
        _citiesDto = responseCities;
        CopyToItems();

        return _itemsDto;

    }

    public void CopyToItems()
    {
        _itemsDto = new List<ItemDtoModel>();

        foreach (var itemDto in _citiesDto.Select(city => new ItemDtoModel
                 {
                     Id = city.Id,
                     Item = city.City
                 }))
        {
            _itemsDto.Add(itemDto);
        }

    }

    public object CopyFromItems(List<ItemDtoModel> itemsDto)
    {
        return itemsDto.Where(el => el.Id == 0).Select(item => new CityDtoModel() { Id = item.Id, IdCounties = item.IdParents, City = item.Item }).ToList();
    }
}