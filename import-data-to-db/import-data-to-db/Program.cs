﻿using import_data_to_db.MapGrapth;
using System;
using System.Collections.Generic;
using System.Xml;
using System.Linq;
using import_data_to_db.Import;
using System.Threading;
using System.Configuration;
using import_data_to_db.Services;
using import_data_to_db.Export;
using System.Threading.Tasks;

namespace import_data_to_db
{
    public class Program
    {
        static MySqlImport import;
        static string file = "td3";
        static ManualResetEvent QuitEvent = new ManualResetEvent(false);
        static string server = ConfigurationManager.AppSettings["Server"];
        static string database = ConfigurationManager.AppSettings["Database"];
        static string username = ConfigurationManager.AppSettings["Username"];
        static string password = ConfigurationManager.AppSettings["Password"];

        static void Main(string[] args)
        {
            //import = new MySqlImport(server, username, password, database);
            XmlDocument xml = new XmlDocument();
            xml.Load(file + ".osm");
            XmlNode osm = null;
            foreach (XmlNode item in xml?.ChildNodes)
            {
                if (item.Name.Equals("osm")) osm = item;
            }

            if (osm != null) IterateNode(osm);

            //Import();
            Task t = ExportServices();
            t.Wait();

            Console.WriteLine("Completed, press Ctrl + C to quit");
            QuitEvent.WaitOne();
        }

        static async Task ExportServices()
        {
            List<Atm> @as = Atm.Atms.Values.Cast<Atm>().ToList();
            //foreach (Atm a in @as)
            //{
            //    await a.ReverseGeocoding();
            //}

            List<Fuel> fs = Fuel.Fuels.Values.Cast<Fuel>().ToList();
            //foreach (Fuel f in fs)
            //{
            //    await f.ReverseGeocoding();
            //}

            List<Maintenance> ms = Maintenance.Maintenances.Values.Cast<Maintenance>().ToList();
            //foreach (Maintenance m in ms)
            //{
            //    await m.ReverseGeocoding();
            //}

            List<Toilet> ts = Toilet.Toilets.Values.Cast<Toilet>().ToList();
            //foreach (Toilet t in ts)
            //{
            //    await t.ReverseGeocoding();
            //}

            RawTextExporter exporter = new RawTextExporter();
            exporter.ExportFile(file + "-atm.txt", @as);
            exporter.ExportFile(file + "-fuel.txt", fs);
            exporter.ExportFile(file + "-maintenance.txt", ms);
            exporter.ExportFile(file + "-toilet.txt", ts);
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

                if (node is Node nd)
                {
                    foreach (Information info in node.Informations)
                    {
                        if (!info.Contains("amenity")) continue;

                        switch (info["amenity"])
                        {
                            case Atm.Amenity:
                                Atm.Create(nd);
                                break;
                            case Fuel.Amenity:
                                Fuel.Create(nd);
                                break;
                            case Toilet.Amenity:
                                Toilet.Create(nd);
                                break;
                            case Maintenance.Amenity:
                                Maintenance.Create(nd);
                                break;
                            default:
                                break;
                        }
                    }
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
                XmlAttributeCollection collection = item.Attributes;
                switch (collection.Count)
                {
                    case 1:
                        info.Add(collection[0].Name, collection[0].Value);
                        break;
                    case 2:
                        info.Add(collection[0].Value, collection[1].Value);
                        break;
                }
                //foreach (XmlAttribute att in item.Attributes)
                //{
                //    info.Add(att.Name, att.Value);
                //}
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

            foreach (KeyValuePair<long, Atm> item in Atm.Atms)
            {
                import.ImportService(item.Value);
            }

            foreach (KeyValuePair<long, Fuel> item in Fuel.Fuels)
            {
                import.ImportService(item.Value);
            }

            foreach (KeyValuePair<long, Toilet> item in Toilet.Toilets)
            {
                import.ImportService(item.Value);
            }

            foreach (KeyValuePair<long, Maintenance> item in Maintenance.Maintenances)
            {
                import.ImportService(item.Value);
            }
            //foreach (KeyValuePair<int, Relation> item in relations)
            //{
            //    import.ImportRelation(item.Value);
            //}

            //QuitEvent.Set();
        }

    }
}
