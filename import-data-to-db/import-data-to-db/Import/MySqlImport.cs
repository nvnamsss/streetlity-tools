using import_data_to_db.MapGrapth;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace import_data_to_db.Import
{
    public class MySqlImport
    {
        private MySqlConnection _connection;

        public MySqlImport(string server, string username, string password, string database)
        {
            StringBuilder connectionString = new StringBuilder();
            connectionString.Append("server=" + server + ";");
            connectionString.Append("uid=" + username + ";");
            connectionString.Append("pwd=" + password + ";");
            connectionString.Append("database=" + database);
            Console.WriteLine("[MySqlImport] - Connection string: " + connectionString.ToString());

            _connection = new MySqlConnection(connectionString.ToString());
            _connection.Open();
        }

        public void ImportNode(Node node)
        {
            string sCommand = node.GetInsertString();

            MySqlCommand command = new MySqlCommand();
            command.Connection = _connection;
            command.CommandText = sCommand;

            command.ExecuteNonQuery();
        }

        public void ImportWay(Way way)
        {
            string sCommand = way.GetInsertString();

            MySqlCommand command = new MySqlCommand();
            command.Connection = _connection;
            command.CommandText = sCommand;

            command.ExecuteNonQuery();
        }

        public void ImportRelation(Relation relation)
        {
            StringBuilder sCommand = new StringBuilder();
            sCommand.Append("INSERT INTO street");
            sCommand.Append("(");
            sCommand.Append("id,");
            sCommand.Append("name,");
            sCommand.Append("nodes,");
            sCommand.Append("coo,");
            sCommand.Append("oneway,");
            sCommand.Append("lanenum,");
            sCommand.Append("maxspeed,");
            sCommand.Append("maxspeedperlane,");
            sCommand.Append(")");
            sCommand.Append("VALUES");
            sCommand.Append("(");
            sCommand.Append(")");

            MySqlCommand command = new MySqlCommand();
            command.Connection = _connection;
            command.CommandText = "INSERT INTO table VALUES()";

            command.ExecuteNonQuery();
        }
    }
}
