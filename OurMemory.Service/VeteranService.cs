using System.Collections.Generic;
using OurMemory.Data.Infrastructure;
using OurMemory.Domain.Entities;
using OurMemory.Service.Interfaces;

namespace OurMemory.Service
{
    public class VeteranService : IVeteranService
    {

        private readonly IRepository<Veteran> _veteranRepository;
        private readonly IUnitOfWork _unitOfWork;

        public VeteranService(IRepository<Veteran> veteranRepository, IUnitOfWork unitOfWork)
        {
            _veteranRepository = veteranRepository;
            _unitOfWork = unitOfWork;
        }


        #region VeteranService Members

        public void Add(Veteran veteran)
        {
            _veteranRepository.Add(veteran);
            SaveVeteran();
        }

        public Veteran GetById(int id)
        {
            return _veteranRepository.GetById(id);
        }

        public IEnumerable<Veteran> GetAll()
        {
            return _veteranRepository.GetAll();
        }

        public void SaveVeteran()
        {
            _unitOfWork.Commit();
        }

        #endregion
    }
}