using System;
using System.Collections.Generic;
using System.Text;

namespace import_data_to_db.MapGrapth
{
    public interface INode
    {
        long Id { get; }
        InformationCollection Informations { get; }
    }
}
