using OurMemory.Data.Infrastructure;
using OurMemory.Data.Interfaces;
using OurMemory.Domain.Entities;
using OurMemory.Service.Interfaces;

namespace OurMemory.Service
{
    public class VeteranService : IVeteranService
    {

        private readonly IVeteranRepository _veteranRepository;
        private readonly IUnitOfWork _unitOfWork;

        public VeteranService(IVeteranRepository veteranRepository, IUnitOfWork unitOfWork)
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


        #endregion
    }
}