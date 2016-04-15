using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using AutoMapper;
using LinqToExcel;
using OurMemory.Domain.DtoModel;
using OurMemory.Service.Interfaces;
using OurMemory.Service.Model;
using OurMemory.Service.Services;

namespace OurMemory.Service.Parsers
{
    public class ExcellParser : IExcellParser
    {
        private readonly ExcelQueryFactory excel;

        public ExcellParser(string filePath)
        {
            excel = new ExcelQueryFactory(filePath);

        }

        public List<VeteranMapping> GetVeterans()
        {
            var rows = (from c in excel.Worksheet<VeteranMapping>()
                        select c).ToList();

            //            veteran = Mapper.Map<IEnumerable<VeteranMapping>, IEnumerable<VeteranBindingModel>>(rows);


            return rows;
        }

    }
}
