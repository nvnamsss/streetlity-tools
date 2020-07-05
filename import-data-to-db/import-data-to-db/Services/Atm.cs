using import_data_to_db.MapGrapth;
using System;
using System.Collections.Generic;
using System.Text;

namespace import_data_to_db.Services
{
    public class Atm : Service
    {
        public const string Amenity = "atm";
        public static Dictionary<long, Atm> Atms = new Dictionary<long, Atm>();
        public string Bank { get; set; }
        public Atm(long id, float lat, float lon, string name) : base(id, lat, lon, name)
        {
            Atms.Add(id, this);
        }

        public override string GetInsertString()
        {
            StringBuilder sCommand = new StringBuilder();
            sCommand.Append("INSERT INTO nodes");
            sCommand.Append("(");
            sCommand.Append("id,");
            sCommand.Append("lat,");
            sCommand.Append("lon,");
            sCommand.Append("address");
            sCommand.Append(")");
            sCommand.Append("VALUES");
            sCommand.Append("(");
            sCommand.Append(Id);
            sCommand.Append(",");
            sCommand.Append(Latitude);
            sCommand.Append(",");
            sCommand.Append(Longitude);
            sCommand.Append(",");

            return sCommand.ToString();
        }

        public override string ToString()
        {
            string s = Amenity + ";";
            s += "id:" + Id + ";";
            s += "lat:" + Latitude + ";";
            s += "lon:" + Longitude + ";";
            s += "name:" + Name + ";";
            return s;
        }

        public static Atm Create(Node node, Information info)
        {
            string name = string.Empty;
            if (info.Contains("name"))
            {
                name = info["name"];
            }

            if (info.Contains("operator"))
            {
                name = info["operator"];
            }

            Atm a = new Atm(node.Id, node.Latitude, node.Longitude, name);
            return a;
        }
    }
}
