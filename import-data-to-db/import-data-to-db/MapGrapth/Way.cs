using System;
using System.Collections.Generic;
using System.Text;

namespace import_data_to_db.MapGrapth
{
    public class Way : INode
    {
        public static Dictionary<long, Way> Ways = new Dictionary<long, Way>();

        public long Id { get; }
        public InformationCollection Informations { get; }

        public Way(long id)
        {
            Id = id;
            Informations = new InformationCollection();
            Ways.Add(id, this);

            Informations.Added += (s, e) => 
            {
                if (e.Name.Equals("nd"))
                {
                    long nodeID = long.Parse(e["ref"]);
                    if (Node.Nodes[nodeID].Streets.Contains(this))
                    {
                        Console.WriteLine("Street " + Id + " is duplicated");
                        //throw new Exception("Street " + Id + " is duplicated");
                    }
                    else
                    {
                        Node.Nodes[nodeID].Streets.Add(this);
                    }

                }
            };
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

        public string GeneralInfoValue()
        {
            StringBuilder s = new StringBuilder();

            foreach (Information item in Informations)
            {
                if (item.Name.Equals("tag"))
                {
                    s.Append(item["k"]);
                    s.Append("=");
                    s.Append(item["v"]);
                    s.Append(";");
                }
            }

            if (s.Length > 0)
                s.Remove(s.Length - 1, 1);

            return s.ToString();
        }

        public string GetInsertString()
        {
            StringBuilder sCommand = new StringBuilder();
            sCommand.Append("INSERT INTO street");
            sCommand.Append("(");
            sCommand.Append("id,");
            sCommand.Append("generalinfo,");
            sCommand.Append("oneway,");
            sCommand.Append("onewayfrom");
            sCommand.Append(")");
            sCommand.Append("VALUES");
            sCommand.Append("(");
            sCommand.Append(Id);
            sCommand.Append(",");
            sCommand.Append(GeneralInfoValue());
            sCommand.Append(",");
            sCommand.Append(true);
            sCommand.Append(",");
            sCommand.Append("onewayfrom");
            sCommand.Append(")");

            return sCommand.ToString();
        }
    }
}
