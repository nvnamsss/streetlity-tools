using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace import_data_to_db.Services
{
    public abstract class Service
    {
        public string Name { get; set; }
        public long Id { get; }
        public float Latitude { get; set; }
        public float Longitude { get; set; }
        public string Address { get; set; }
        public abstract string GetInsertString();
        protected Service(long id, float lat, float lon, string name)
        {
            Id = id;
            Latitude = lat;
            Longitude = lon;
            Name = name;
            ReverseGeocoding().Wait();
        }

        public async Task<string> ReverseGeocoding()
        {
            HttpResponseMessage response = new HttpResponseMessage();
            string query = "lat=" + Latitude + "&" + "lon=" + Longitude;
            Uri uri = new Uri("http://localhost:3000/geocoding/reverse?" + query);
            HttpClient client = new HttpClient();
            var headers = client.DefaultRequestHeaders;
            response = await client.GetAsync(uri);

            Address = await response.Content.ReadAsStringAsync();
            Console.WriteLine("Reverse geocoding got {0}", Address);
            return Address;
        }
    }
}
