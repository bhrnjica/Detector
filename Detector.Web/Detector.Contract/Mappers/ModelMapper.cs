

using Detector.Contract.DTO;
using Detector.Contract.Models;


namespace Detector.Contract.Mappers;

public static class ModelMapper
{
    public static (DetectorData data, DetectorDetails details) ToEntity(this DetectorItem item)
    {
        var data = new DetectorData();
        var details = new DetectorDetails();

        data.Name = item.Name;  

        data.Version= item.Version; 
        
        details.Notes= item.Notes;

        return (data, details);
    }


    public static DetectorItem ToItem(this DetectorData data, DetectorDetails details)
    {
        var item = new DetectorItem();

        if (data.Id > 0)
        {
            item.Id = data.Id;
        }

        item.Name= data.Name;

        item.Version = data.Version;

        if(details!=null)
        {
            item.Notes = details.Notes;
        }

        
        return item;
    }

    public static DetectorItem ToItem(this DetectorDetails details, DetectorData data)
    {
        var item = new DetectorItem();

        if (data.Id > 0)
        {
            item.Id= data.Id;   
        }
        item.Name = data.Name;

        item.Version = data.Version;

        item.Notes = details.Notes;


        return item;
    }

}
