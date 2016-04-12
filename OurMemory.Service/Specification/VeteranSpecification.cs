using OurMemory.Data.Specification.Core;
using OurMemory.Domain.Entities;
using OurMemory.Service.Model;

namespace OurMemory.Service.Specification
{
    public class VeteranSpecification : SpecificationBase<Veteran>
    {
        public Specification<Veteran> KeyWord(SearchVeteranModel searchVeteranModel)
        {
            return GetMoreYear(searchVeteranModel.DateBirthStart)
                .And(GetLessYear(searchVeteranModel.DateBirthEnd))
                .And(GetByFirstName(searchVeteranModel.FirstName))
                .And(GetByLastName(searchVeteranModel.LastName))
                .And(GetByMiddlename(searchVeteranModel.MiddleName))
                .And(!IsDeleted());
        }


        public Specification<Veteran> GetMoreYear(int? year)
        {
            return year.HasValue ? new Specification<Veteran>(x => x.DateBirth.Value.Year >= year.Value) : Empty();
        }

        public Specification<Veteran> GetLessYear(int? year)
        {
            return year.HasValue ? new Specification<Veteran>(x => x.DateBirth.Value.Year <= year.Value) : Empty();
        }

        public Specification<Veteran> GetYear(int? year)
        {
            return year.HasValue ? new Specification<Veteran>(x => x.DateBirth.Value.Year >= year.Value) : Empty();
        }


        public Specification<Veteran> GetBetweenYears(int yearStart, int yearEnd)
        {
            return new Specification<Veteran>(x => x.DateBirth.Value.Year >= yearStart && x.DateBirth.Value.Year <= yearEnd);
        }

        public Specification<Veteran> GetByFirstName(string name)
        {
            return name == null ? Empty() : new Specification<Veteran>(x => x.FirstName.Contains(name));
        }

        public Specification<Veteran> GetByLastName(string name)
        {
            return name == null ? Empty() : new Specification<Veteran>(x => x.LastName.Contains(name));
        }

        public Specification<Veteran> GetByMiddlename(string name)
        {
            return name == null ? Empty() : new Specification<Veteran>(x => x.MiddleName.Contains(name));
        }

    }
}
