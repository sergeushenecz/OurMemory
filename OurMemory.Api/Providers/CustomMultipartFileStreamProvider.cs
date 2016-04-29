using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using OurMemory.Models;

namespace OurMemory.Providers
{
    class CustomMultipartFileStreamProvider : MultipartMemoryStreamProvider
    {
        public SelectionImageBindingModel CustomData { get; set; }

        public CustomMultipartFileStreamProvider()
        {
            CustomData = new SelectionImageBindingModel();
        }

        public override Task ExecutePostProcessingAsync()
        {
            foreach (var file in Contents)
            {
                var parameters = file.Headers.ContentDisposition.Parameters;
                var data = new SelectionImageBindingModel
                {
                    X = int.Parse(GetNameHeaderValue(parameters, "X")),
                    Y = int.Parse(GetNameHeaderValue(parameters, "Y")),
                    Width = int.Parse(GetNameHeaderValue(parameters, "Width")),
                    Height = int.Parse(GetNameHeaderValue(parameters, "Heigh")),
                };

                CustomData = data;


            }

            return base.ExecutePostProcessingAsync();
        }

        private static string GetNameHeaderValue(ICollection<NameValueHeaderValue> headerValues, string name)
        {
            var nameValueHeader = headerValues.FirstOrDefault(
                x => x.Name.Equals(name, StringComparison.OrdinalIgnoreCase));

            return nameValueHeader != null ? nameValueHeader.Value : null;
        }
    }
}