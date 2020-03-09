using System;
using System.Xml;

namespace import_data_to_db
{
    class Program
    {
        static void Main(string[] args)
        {
            XmlDocument xml = new XmlDocument();
            xml.Load("district-1.osm");
            for (int loop = 0; loop < 5; ++loop)
            {
                Console.WriteLine(loop);
            }
            Console.WriteLine("Hello World!");
        }
    }
}
