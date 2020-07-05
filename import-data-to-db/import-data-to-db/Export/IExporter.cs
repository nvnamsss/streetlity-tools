using import_data_to_db.MapGrapth;
using import_data_to_db.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace import_data_to_db.Export
{
    public interface IExporter
    {
        public bool ExportFile(string filename, Service service);
        public bool ExportFile(string filename, INode node);
        public bool ExportFile(string filename, IEnumerable<Service> services);
    }
}
