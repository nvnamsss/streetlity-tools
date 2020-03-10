using System;
using System.Collections.Generic;
using System.Text;

namespace import_data_to_db.MapGrapth
{
    public class Node : INode
    {
        public long Id { get; }
        public float Latitude { get; set; }
        public float Longitude { get; set; }
        public InformationCollection Informations { get; }

        public Node(long id)
        {
            Id = id;
            Informations = new InformationCollection();
        }

        public Node(long id, float latitude, float longitude) : this(id)
        {
            Latitude = latitude;
            Longitude = longitude;
        }

        public override string ToString()
        {
            return "Node: " + Id;
        }
    }
}
