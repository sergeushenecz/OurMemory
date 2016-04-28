using System.Collections.Generic;
using OurMemory.Domain.Entities;

namespace OurMemory.Service.Interfaces
{
    public interface IImageVeteranService
    {
        void DeleteImages(IEnumerable<Image> id);
    }
}