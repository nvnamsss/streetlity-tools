using import_data_to_db.MapGrapth;
using import_data_to_db.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace import_data_to_db.Export
{
    public class RawTextExporter : IExporter
    {
        public bool ExportFile(string filename, Service service)
        {
            throw new NotImplementedException();
        }

        public bool ExportFile(string filename, INode node)
        {
            throw new NotImplementedException();
        }

        public bool ExportFile(string filename, IEnumerable<Service> services)
        {
            StreamWriter f = new StreamWriter(filename);
            foreach (Service s in services)
            {
                f.WriteLine(s.ToString());
            }

            f.Close();
            return true;
        }
    }
}
