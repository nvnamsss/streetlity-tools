using System;
using System.Collections.Generic;
using System.Text;

namespace import_data_to_db.Services
{
    public class Atm : Service
    {
        public const string Amenity = "atm";
        public static Dictionary<long, Atm> Atms = new Dictionary<long, Atm>();

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
    }
}
