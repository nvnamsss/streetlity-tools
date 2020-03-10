using System;
using System.Collections.Generic;
using System.Text;

namespace import_data_to_db.MapGrapth
{
    public class Way : INode
    {
        public long Id { get; }
        public InformationCollection Informations { get; }

        public Way(long id)
        {
            Id = id;
            Informations = new InformationCollection();
        }
    }
}
