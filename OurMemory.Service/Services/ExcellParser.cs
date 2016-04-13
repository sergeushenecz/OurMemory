using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AutoMapper;
using LinqToExcel;
using OurMemory.Domain.DtoModel;
using OurMemory.Service.Helper;
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

        public void GetVeterans(out IEnumerable<VeteranBindingModel> veteran)
        {
            var rows = (from c in excel.Worksheet<VeteranMapping>()
                        select c).ToList();

            veteran = Mapper.Map<IEnumerable<VeteranMapping>, IEnumerable<VeteranBindingModel>>(rows);



            foreach (var row in rows)
            {
                string regex = @"((www\.|(http|https|ftp|news|file)+\:\/\/)[&#95;.a-z0-9-]+\.[a-z0-9\/&#95;:_@=.+?,##%&~-]*[^.|\'|\# |!|\(|?|,| |>|<|;|\)])";

                Regex regex1 = new Regex(regex, RegexOptions.IgnoreCase);

                Match match = regex1.Match(row.UrlImages);

                while (match.Success)
                {
                    var downloadData = new WebClient().DownloadData(match.Value);

                    if (downloadData != null)
                    {
                        ImageService imageService = new ImageService();
                        var imageReference = imageService.SaveImage(downloadData);
                    }
                }
            }
        }

    }
}
