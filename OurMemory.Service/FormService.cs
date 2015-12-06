using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using System.Web;
using OurMemory.Domain.Entities;
using OurMemory.Models.Veteran;
using OurMemory.Service.Interfaces;

namespace OurMemory.Service
{
    public class FormService<T> : IFormService<T> where T : class, new()
    {

        public Dictionary<string, string> SetDataFromForm(HttpContext context)
        {
            var propertyInfos = typeof(T).GetProperties();

            var dictionary = new Dictionary<string, string>();


            foreach (var propertyInfo in propertyInfos)
            {
                var value = context.Request.Form[propertyInfo.Name];

                if (value != null)
                {
                    dictionary.Add(propertyInfo.Name, value);
                }

            }

            return dictionary;
        }


        public Veteran MapperDataVeteran(Dictionary<string, string> dictionaryDataFromForm)
        {
            var veteran = new Veteran
            {

            };



            veteran.FirstName = dictionaryDataFromForm["FirstName"];

            veteran.LastName = dictionaryDataFromForm["LastName"];
//            veteran.MiddleName = dictionaryDataFromForm["MiddleName"];
//            veteran.Description = dictionaryDataFromForm["Description"];
//            veteran.DataBirth = Convert.ToDateTime(dictionaryDataFromForm["DataBirth"]);
//            veteran.Front = dictionaryDataFromForm["Front"];
//            veteran.Сalled = dictionaryDataFromForm["Сalled"];
//            veteran.CountryLive = dictionaryDataFromForm["CountryLive"];


            return veteran;

        }
    }

}