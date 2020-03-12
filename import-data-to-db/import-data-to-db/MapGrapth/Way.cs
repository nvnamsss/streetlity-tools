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

        /// <summary>
        /// Get string data to import to SQL
        /// </summary>
        public string NodeValue()
        {
            StringBuilder s = new StringBuilder();

            foreach (Information item in Informations)
            {
                if (item.Name.Equals("nd"))
                {
                    s.Append(item["ref"] + ";");
                }
            }

            s.Remove(s.Length, 1);
            return s.ToString();
        }
    }
}
