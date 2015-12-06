using System.Collections.Generic;
using System.Web;
using OurMemory.Domain.Entities;

namespace OurMemory.Service.Interfaces
{
    public interface IFormService<in T>
    {
        Dictionary<string, string> GetDataFromForm(HttpContext context);
        Veteran MapperDataVeteran(Dictionary<string, string> dictionaryDataFromForm);
    }
}