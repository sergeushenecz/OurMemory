using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using System.Web;
using OurMemory.Domain.Entities;
using OurMemory.Service.Interfaces;

namespace OurMemory.Service
{
    public class FormService<T> : IFormService<T> where T : class, new()
    {

        public Dictionary<string, string> GetDataFromForm(HttpContext context)
        {
            var propertyInfos = typeof(T).GetProperties();

            var dictionary = new Dictionary<string, string>();


            foreach (var propertyInfo in propertyInfos)
            {
                string propertyName = propertyInfo.Name;

                var value = context.Request.Form[propertyName];

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
                FirstName = dictionaryDataFromForm["FirstName"],
                LastName = dictionaryDataFromForm["LastName"],
                MiddleName = dictionaryDataFromForm["MiddleName"],
                Description = dictionaryDataFromForm["Description"],
                DateBirth = Convert.ToDateTime(dictionaryDataFromForm["DateBirth"]),
                BirthPlace = dictionaryDataFromForm["BirthPlace"],
                DateDeath = Convert.ToDateTime(dictionaryDataFromForm["DateDeath"]),
                Latitude = Convert.ToDouble(dictionaryDataFromForm["Latitude"]),
                Longitude = Convert.ToDouble(dictionaryDataFromForm["Longitude"]),
                Called = Convert.ToDateTime(dictionaryDataFromForm["Called"]),
                Awards = dictionaryDataFromForm["Awards"],
                Troops = dictionaryDataFromForm["Troops"]
            };

            return veteran;

        }


        
    }

}