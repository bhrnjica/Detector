using Detector.Contract.DTO;
using Detector.Contract.Interfaces;
using Detector.Contract.Mappers;
using Detector.Contract.Models;
using Detector.WebApi.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using System.Linq.Expressions;

namespace Detector.WebApi.Repositories;

public class DetectorRepository : IDetectorRepository
{
    DetectorContext _context;
    public DetectorRepository(DetectorContext context)
    {
        _context= context;
    }
    public async Task<DetectorItem> CreateAsync(DetectorItem item)
    {
        (var data, var details) = item.ToEntity();

        data.DetectorDetails= details;
       
        var newitem = _context.DetectorData.Add(data);

        await _context.SaveChangesAsync();

        item.Id = newitem.Entity.Id;

        return item;    
    }

    public async Task<DetectorItem> UpdateAsync(DetectorItem item)
    {
        var det = await _context.DetectorData.Include(nameof(DetectorDetails)).Where(x => x.Id == item.Id).SingleOrDefaultAsync();

        det.Name = item.Name;

        det.Version = item.Version;
        
        det.DetectorDetails.Notes= item.Notes;

        await _context.SaveChangesAsync();

        return item;
    }

    public async Task<bool> DeleteAsync(int id)
    {

        var retVal = await _context.DetectorData.Where(x => x.Id == id).FirstOrDefaultAsync();
        
        if(retVal != null)
        {
           _context.DetectorData.Remove(retVal);
        }
        
        await _context.SaveChangesAsync();

        return retVal != null;
    }

    public async Task<List<DetectorItem>> FindAllAsync()
    {
        var results = await _context.DetectorData.Include(nameof(DetectorDetails)).ToListAsync();

        var retVal = new List<DetectorItem>();

        foreach( var d in results )
        {
            var itm = d.ToItem(d.DetectorDetails);

            retVal.Add(itm);
        }

        return retVal;
    }

    public async Task<DetectorItem> FindByIdAsync(int id)
    {
        var ret = await _context.DetectorData
                                        .Include(nameof(DetectorDetails))
                                        .Where(x => x.Id == id)
                                        .Select(x=>x).FirstOrDefaultAsync();

        return ret?.ToItem(ret.DetectorDetails);
    }

    public async Task<DetectorItem> FindByNameAsync(string name)
    {
        var ret = await _context.DetectorData
                                        .Include(nameof(DetectorDetails))
                                        .Where(x => x.Name == name)
                                        .Select(x => x).FirstOrDefaultAsync();

        return ret?.ToItem(ret.DetectorDetails);
    }
}
