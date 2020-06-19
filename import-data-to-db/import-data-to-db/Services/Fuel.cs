using System;
using System.Collections.Generic;
using System.Text;

namespace import_data_to_db.Services
{
    public class Fuel : Service
    {
        public const string Amenity = "fuel";
        public static Dictionary<long, Fuel> Fuels = new Dictionary<long, Fuel>();

        public Fuel(long id, float lat, float lon, string name) : base(id, lat, lon, name)
        {
            Fuels.Add(id, this);
        }

        public override string GetInsertString()
        {
            throw new NotImplementedException();
        }
    }
}
