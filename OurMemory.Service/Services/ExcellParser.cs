using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinqToExcel;
using OurMemory.Service.Interfaces;
using OurMemory.Service.Model;

namespace OurMemory.Service.Services
{
    public class ExcellParser : IExcellParser
    {
        private readonly ExcelQueryFactory excel;

        public ExcellParser(string filePath)
        {
            excel = new ExcelQueryFactory(filePath);

        }

        public void GetVeterans()
        {
            var rows = (from c in excel.Worksheet<VeteranRow>()
                        select c).ToList();

            foreach (var row in rows)
            {

            }
        }

        private void Map(VeteranMapping veteranMapping)
        {
            excel.AddMapping<VeteranRow>(x => x.FirstName, veteranMapping.FirstName);
            excel.AddMapping<VeteranRow>(x => x.LastName, veteranMapping.LastName);
            excel.AddMapping<VeteranRow>(x => x.MiddleName, veteranMapping.MiddleName);
            excel.AddMapping<VeteranRow>(x => x.DateBirth, veteranMapping.DateBirth);
            excel.AddMapping<VeteranRow>(x => x.DateDeath, veteranMapping.DateDeath);
            excel.AddMapping<VeteranRow>(x => x.BirthPlace, veteranMapping.BirthPlace);
            excel.AddMapping<VeteranRow>(x => x.Awards, veteranMapping.Awards);
            excel.AddMapping<VeteranRow>(x => x.Description, veteranMapping.Description);
            excel.AddMapping<VeteranRow>(x => x.Troops, veteranMapping.Troops);
            excel.AddMapping<VeteranRow>(x => x.UrlImages, veteranMapping.UrlImages);
            excel.AddMapping<VeteranRow>(x => x.Called, veteranMapping.Called);
            excel.AddMapping<VeteranRow>(x => x.Description, veteranMapping.Description);
        }
    }
}
