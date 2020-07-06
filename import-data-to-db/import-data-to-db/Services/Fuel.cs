using import_data_to_db.MapGrapth;
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

        public override string ToString()
        {
            string s = Amenity + ";";
            s += "id:" + Id + ";";
            s += "lat:" + Latitude + ";";
            s += "lon:" + Longitude + ";";
            s += "address:" + Address + ";";
            s += "name:" + Name + ";";
            return s;
        }

        public static Fuel Create(Node node)
        {
            InformationCollection infos = node.Informations;
            List<Information> l = infos["tag"];
            string name = string.Empty;
            foreach (Information info in l)
            {
                if (info.Contains("name"))
                {
                    name = info["name"];
                }
            }
            Fuel f = new Fuel(node.Id, node.Latitude, node.Longitude, name);
            return f;
        }
    }
}
