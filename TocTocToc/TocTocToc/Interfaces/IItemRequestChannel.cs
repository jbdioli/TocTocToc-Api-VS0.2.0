using System.Collections.Generic;
using System.Threading.Tasks;
using TocTocToc.Models.Dto;

namespace TocTocToc.Interfaces;

public interface IItemRequestChannel
{
    public Task<List<ItemDtoModel>> GetItemsAsync(ItemRequestDtoModel itemRequestDto);
    public Task<List<ItemDtoModel>> SaveItemsAsync(List<ItemDtoModel> itemsDto);
}