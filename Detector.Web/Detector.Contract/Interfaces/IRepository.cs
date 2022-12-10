using Detector.Contract.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Detector.Contract.Interfaces;

public interface IDetectorRepository
{
    Task<List< DetectorItem >> FindAllAsync();

    Task< DetectorItem> FindByIdAsync(int id);

    Task< DetectorItem> FindByNameAsync(string name);

    Task<DetectorItem> CreateAsync( DetectorItem entity );

    Task<DetectorItem> UpdateAsync(DetectorItem entity );
    
    Task<bool> DeleteAsync(int id );
}
