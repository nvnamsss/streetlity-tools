using import_data_to_db.MapGrapth;
using System;
using System.Collections.Generic;
using System.Xml;
using System.Linq;
using import_data_to_db.Import;
using System.Threading;
using System.Configuration;

namespace import_data_to_db
{
    public class Program
    {
        static MySqlImport import;

        static ManualResetEvent QuitEvent = new ManualResetEvent(false);
        static string server = ConfigurationManager.AppSettings["Server"];
        static string database = ConfigurationManager.AppSettings["Database"];
        static string username = ConfigurationManager.AppSettings["Username"];
        static string password = ConfigurationManager.AppSettings["Password"];

        static void Main(string[] args)
        {
            import = new MySqlImport(server, username, password, database);
            XmlDocument xml = new XmlDocument();
            xml.Load("district-1.osm");
            XmlNode osm = null;
            foreach (XmlNode item in xml?.ChildNodes)
            {
                if (item.Name.Equals("osm")) osm = item;
            }

            if (osm != null) IterateNode(osm);

            Import();

            Console.ReadKey();
            //QuitEvent.WaitOne();
        }

        /// <summary>
        /// Iterate over root node and initialize data
        /// </summary>
        /// <param name="osm"></param>
        static void IterateNode(XmlNode osm)
        {
            foreach (XmlNode item in osm.ChildNodes)
            {
                INode node = CreateNode(item);
                if (node != null)
                {
                    IterateInformation(node, item);
                }
            }
        }

        static INode CreateNode(XmlNode node)
        {
            if (node.Name.Equals("node"))
            {
                long id = long.Parse(node.Attributes["id"].Value);
                float lat = float.Parse(node.Attributes["lat"].Value);
                float lon = float.Parse(node.Attributes["lon"].Value);
                return new Node(id, lat, lon);
            }

            if (node.Name.Equals("way"))
            {
                long id = long.Parse(node.Attributes["id"].Value);
                return new Way(id);
            }

            if (node.Name.Equals("relation"))
            {
                long id = long.Parse(node.Attributes["id"].Value);
                return new Relation(id);
            }

            return null;
        }

        /// <summary>
        /// Iterate over child of node and initialize information
        /// </summary>
        /// <param name="node"></param>
        static void IterateInformation(INode node, XmlNode xmlnode)
        {
            foreach (XmlNode item in xmlnode.ChildNodes)
            {
                Information info = new Information(item.Name);
                foreach (XmlAttribute att in item.Attributes)
                {
                    info.Add(att.Name, att.Value);
                }
                node.Informations.Add(info);
            }
        }

        static void Import()
        {
            foreach (KeyValuePair<long, Node> item in Node.Nodes)
            {
#if DEBUG
                Console.WriteLine(item.Value.GetInsertString());
#endif
                import.ImportNode(item.Value);
            }

            foreach (KeyValuePair<long, Way> item in Way.Ways)
            {
#if DEBUG
                Console.WriteLine(item.Value.GetInsertString());
#endif
                import.ImportWay(item.Value);
            }

            //foreach (KeyValuePair<int, Relation> item in relations)
            //{
            //    import.ImportRelation(item.Value);
            //}

            //QuitEvent.Set();
        }

    }
}
