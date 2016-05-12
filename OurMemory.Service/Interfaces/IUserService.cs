using System.Collections.Generic;
using OurMemory.Domain.Entities;

namespace OurMemory.Service.Interfaces
{
    public interface IUserService
    {
        User GetById(string id);
        void UpdateUser(User user);
        void SaveUser();
    }
}