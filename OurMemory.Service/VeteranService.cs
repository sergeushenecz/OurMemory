using System;
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
            Veteran veteran = _veteranRepository.GetById(id);
            return veteran.IsDeleted == false ? veteran : null;
        }

        public void UpdateVeteran(Veteran veteran)
        {
            _veteranRepository.Update(veteran);
            SaveVeteran();
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