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


        #region IVeteranRepository Members

        public void CreateVeteran(Veteran veteran)
        {
            _veteranRepository.Add(veteran);
            SaveVeteran();
        }

        public void SaveVeteran()
        {
            _unitOfWork.Commit();
        }

        public IEnumerable<Veteran> GetAllVeterans()
        {
            return _veteranRepository.GetAll();
        }

        #endregion
    }
}