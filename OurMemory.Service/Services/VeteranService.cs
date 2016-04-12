using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using OurMemory.Data.Infrastructure;
using OurMemory.Data.Specification.Core;
using OurMemory.Domain.Entities;
using OurMemory.Service.Extenshions;
using OurMemory.Service.Interfaces;
using OurMemory.Service.Model;
using OurMemory.Service.Specification;

namespace OurMemory.Service.Services
{
    public class VeteranService : IVeteranService
    {

        private readonly IRepository<Veteran> _veteranRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly VeteranSpecification _veteranSpecification;

        public VeteranService(IRepository<Veteran> veteranRepository, IUnitOfWork unitOfWork, VeteranSpecification veteranSpecification1)
        {
            _veteranRepository = veteranRepository;
            _unitOfWork = unitOfWork;
            _veteranSpecification = veteranSpecification1;
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

            return veteran;
        }

        public void UpdateVeteran(Veteran veteran)
        {
            _veteranRepository.Update(veteran);
            SaveVeteran();
        }

        public IEnumerable<Veteran> GetAll()
        {
            return _veteranRepository.GetAll().Where(x => !x.IsDeleted);
        }

        public IEnumerable<Veteran> SearchVeterans(SearchVeteranModel searchVeteranModel)
        {
            Specification<Veteran> keyWord = _veteranSpecification.KeyWord(searchVeteranModel);

            var veterans = _veteranRepository.GetSpec(keyWord.Predicate)
                .OrderBy(x => x.FirstName)
                .Pagination(searchVeteranModel.Skip, searchVeteranModel.Size);

            return veterans;
        }

        public void SaveVeteran()
        {
            _unitOfWork.Commit();
        }

        #endregion
    }
}