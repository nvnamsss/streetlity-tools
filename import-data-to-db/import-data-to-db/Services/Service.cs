using System;
using System.Collections.Generic;
using System.Text;

namespace import_data_to_db.Services
{
    public abstract class Service
    {
        public string Name { get; set; }
        public long Id { get; }
        public float Latitude { get; set; }
        public float Longitude { get; set; }
        public abstract string GetInsertString();
        protected Service(long id, float lat, float lon, string name)
        {
            Id = id;
            Latitude = lat;
            Longitude = lon;
            Name = name;
        }
    }
}
