using OurMemory.Data.Infrastructure;
using OurMemory.Domain.Entities;
using OurMemory.Service.Interfaces;

namespace OurMemory.Service
{
    public class UserService : IUserService
    {

        private readonly IRepository<User> _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IRepository<User> userRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public User GetById(string id)
        {
            var byId = _userRepository.GetById(id);
            return byId;
        }
    }
}