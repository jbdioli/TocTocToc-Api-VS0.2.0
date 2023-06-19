using System.Collections.Generic;
using TocTocToc.Models.Dto;

namespace TocTocToc.Interfaces;

public interface ICopyItemsDtoHandler
{
    public void CopyToItems();
    public object CopyFromItems(List<ItemDtoModel> itemsDto);
}