using System;
using System.IO;
using System.Net;
using System.Web;
using GoogleMaps.LocationServices;
using OurMemory.Service.Model;

namespace OurMemory.Service.Services
{
    public class GoogleMapsService
    {
        private string API_KEY = string.Empty;

        public GoogleMapsService(string api_key)
        {
            this.API_KEY = api_key;
        }

        public void SetApiKey(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentException("API Key is invalid");
            }

            this.API_KEY = key;

        }

        /// <summary>
        /// Perform a geocode lookup of an address
        /// </summary>
        /// <param name="addr">The address in CSV form line1, line2, postcode</param>
        /// <param name="output">CSV or XML</param>
        /// <returns>LatLng object</returns>
        /// 
        public LatLng GetLatLng(string address)
        {

            var locationService = new GoogleLocationService();
            var point = locationService.GetLatLongFromAddress(address);

            var latitude = point.Latitude;
            var longitude = point.Longitude;

            var latLng = new LatLng()
            {
                Latitude = latitude,
                Longitude = longitude
            };

            return latLng;
        }
    }
}