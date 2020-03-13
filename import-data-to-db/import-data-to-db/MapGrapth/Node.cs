using System;
using System.Collections.Generic;
using System.Text;

namespace import_data_to_db.MapGrapth
{
    public class Node : INode
    {
        public static Dictionary<long, Node> Nodes = new Dictionary<long, Node>();
        public long Id { get; }
        public float Latitude { get; set; }
        public float Longitude { get; set; }
        public List<Way> Streets { get; set; }
        public InformationCollection Informations { get; }

        public Node(long id)
        {
            Id = id;
            Informations = new InformationCollection();
            Streets = new List<Way>();
            Nodes.Add(id, this);
        }

        public Node(long id, float latitude, float longitude) : this(id)
        {
            Latitude = latitude;
            Longitude = longitude;
        }

        public string StreetValue()
        {
            string s = string.Empty;

            if (Streets.Count == 0)
            {
                Console.WriteLine("[Node] " + Id + " - is not locate on any streets");
                return s;
            }

            s += Streets[0].Id;
            for (int loop = 1; loop < Streets.Count; loop++)
            {
                s += ";";
                s += Streets[loop].Id;
            }

            return s;
        }

        public override string ToString()
        {
            return "Node: " + Id;
        }

        public string GetInsertString()
        {
            StringBuilder sCommand = new StringBuilder();
            sCommand.Append("INSERT INTO nodes");
            sCommand.Append("(");
            sCommand.Append("id,");
            sCommand.Append("lat,");
            sCommand.Append("lon,");
            sCommand.Append("streets");
            sCommand.Append(")");
            sCommand.Append("VALUES");
            sCommand.Append("(");
            sCommand.Append(Id);
            sCommand.Append(",");
            sCommand.Append(Latitude);
            sCommand.Append(",");
            sCommand.Append(Longitude);
            sCommand.Append(",");
            sCommand.Append(StreetValue());
            sCommand.Append(")");

            return sCommand.ToString();
        }
    }
}
