using System;
using System.Collections.Generic;
using System.Text;

namespace import_data_to_db.Services
{
    public class Maintenance : Service
    {
        public const string Amenity = "maintenance";

        public static Dictionary<long, Maintenance> Maintenances = new Dictionary<long, Maintenance>();

        public Maintenance(long id, float lat, float lon, string name) :base(id, lat, lon, name)
        {
            Maintenances.Add(id, this);
        }
        public override string GetInsertString()
        {
            throw new NotImplementedException();
        }
    }
}
