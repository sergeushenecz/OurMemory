using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OurMemory.Domain.DtoModel;
using OurMemory.Service.Model;

namespace OurMemory.Service.Interfaces
{
    public interface IExcellParser
    {
        void GetVeterans(out IEnumerable<VeteranBindingModel> veteranBindingModel);
    }
}
