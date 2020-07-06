using import_data_to_db.MapGrapth;
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

        public static Toilet Create(Node node)
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
            Toilet t = new Toilet(node.Id, node.Latitude, node.Longitude, name);
            return t;
        }
    }
}
