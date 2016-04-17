using OurMemory.Data.Specification.Core;
using OurMemory.Domain.Entities;
using OurMemory.Service.Model;

namespace OurMemory.Service.Specification
{
    public class VeteranSpecification : SpecificationBase<Veteran>
    {
        public Specification<Veteran> KeyWord(SearchVeteranModel searchVeteranModel)
        {
            return GetMoreBirthYear(searchVeteranModel.YearBirthStart)
                .And(GetLessBirthYear(searchVeteranModel.YearBirthEnd))
                .And(GetMoreDeadYear(searchVeteranModel.YearDeathStart))
                .And(GetLessDeadYear(searchVeteranModel.YearDeathEnd))
                .And(GetMoreCallYear(searchVeteranModel.YearCallStart))
                .And(GetLessCallYear(searchVeteranModel.YearCallEnd))
                .And(GetByFirstName(searchVeteranModel.FirstName))
                .And(GetByLastName(searchVeteranModel.LastName))
                .And(GetByMiddlename(searchVeteranModel.MiddleName))
                .And(GetByBirthPlace(searchVeteranModel.BirthPlace))
                .And(!IsDeleted());
        }


        public Specification<Veteran> GetMoreBirthYear(int year)
        {
            return year != 0 ? new Specification<Veteran>(x => x.DateBirth.Value.Year >= year) : Empty();
        }

        public Specification<Veteran> GetLessBirthYear(int year)
        {
            return year != 0 ? new Specification<Veteran>(x => x.DateBirth.Value.Year <= year) : Empty();
        }


        public Specification<Veteran> GetMoreDeadYear(int year)
        {
            return year != 0 ? new Specification<Veteran>(x => x.DateDeath.Value.Year >= year) : Empty();
        }

        public Specification<Veteran> GetLessDeadYear(int year)
        {
            return year != 0 ? new Specification<Veteran>(x => x.DateDeath.Value.Year <= year) : Empty();
        }

        public Specification<Veteran> GetMoreCallYear(int year)
        {
            return year != 0 ? new Specification<Veteran>(x => x.Called.Value.Year >= year) : Empty();
        }

        public Specification<Veteran> GetLessCallYear(int? year)
        {
            return year != 0 ? new Specification<Veteran>(x => x.Called.Value.Year <= year.Value) : Empty();
        }

        public Specification<Veteran> GetYear(int? year)
        {
            return year != 0 ? new Specification<Veteran>(x => x.DateBirth.Value.Year >= year.Value) : Empty();
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

        public Specification<Veteran> GetByBirthPlace(string name)
        {
            return name == null ? Empty() : new Specification<Veteran>(x => x.BirthPlace.Contains(name));
        }
    }
}
