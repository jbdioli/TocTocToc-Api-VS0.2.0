using Xamarin.Forms.Maps;

namespace TocTocToc.Models.Dto;

public class LocationDtoModel
{
    public LocationDtoModel()
    {
    }

    public string LocationName { get; set; }
    public double Lat { get; set; }
    public double Lon { get; set; }
    public double Distance { get; set; } = 200;
    //public string FullAddress { get; set; }
    public string FullAddress => $"{Address}, {ZipCode} {City}, {Country}";
    public string Address { get; set; }
    public string BuildingName { get; set; }
    public string BuildingNumber { get; set; }
    public string Country { get; set; }
    public string State { get; set; }
    public string County { get; set; }
    public string City { get; set; }
    public string ZipCode { get; set; }
    public Map XNameMap { get; set; }
}


// var newAddress = "44 Boulevard Napoléon III, 06200 Nice, France";