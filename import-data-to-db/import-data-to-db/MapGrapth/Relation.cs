using System;
using System.Collections.Generic;
using System.Text;

namespace import_data_to_db.MapGrapth
{
    public class Relation : INode
    {
        public static Dictionary<long, Relation> Relations = new Dictionary<long, Relation>();
        public long Id { get; }
        public InformationCollection Informations { get; }
        public Relation(long id)
        {
            Id = id;
            Informations = new InformationCollection();
            Relations.Add(id, this);
        }
    }
}
