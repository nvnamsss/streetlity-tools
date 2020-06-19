using System;
using System.Collections.Generic;
using System.Text;

namespace import_data_to_db.Services
{
    public class Toilet : Service
    {
        public const string Amenity = "toilets";
        public static Dictionary<long, Toilet> Toilets = new Dictionary<long, Toilet>();
        public Toilet(long id, float lat, float lon, string name) : base(id, lat, lon, name)
        {
            Toilets.Add(id, this);
        }

        public override string GetInsertString()
        {
            throw new NotImplementedException();
        }
    }
}
