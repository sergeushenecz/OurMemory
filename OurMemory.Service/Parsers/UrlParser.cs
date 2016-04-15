using System.Collections.Generic;
using System.Net;
using System.Text.RegularExpressions;
using OurMemory.Domain.DtoModel;
using OurMemory.Service.Model;
using OurMemory.Service.Services;

namespace OurMemory.Service.Parsers
{
    public static class UrlParser
    {
        private static string _regex = @"((www\.|(http|https|ftp|news|file)+\:\/\/)[&#95;.a-z0-9-]+\.[a-z0-9\/&#95;:_@=.+?,##%&~-]*[^.|\'|\# |!|\(|?|,| |>|<|;|\)])";

        public static List<string> Parse(string urls)
        {
            List<string> parsedUrls = new List<string>();

            foreach (Match match in Regex.Matches(urls, _regex))
            {
                parsedUrls.Add(match.Value);
            }

            return parsedUrls;

        }



        public static IEnumerable<ImageReference> DownloadFromUrls(List<string> urls)
        {
            List<ImageReference> imageReferences = new List<ImageReference>();

            foreach (var url in urls)
            {
                var downloadData = new WebClient().DownloadData(url);

                if (downloadData != null)
                {
                    ImageService imageService = new ImageService();
                    var imageReference = imageService.SaveImage(downloadData);
                    imageReferences.Add(imageReference);
                }
            }

            return imageReferences;
        }
    }
}

