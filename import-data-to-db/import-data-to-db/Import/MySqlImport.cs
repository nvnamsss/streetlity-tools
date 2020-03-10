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
            _connection = new MySqlConnection(connectionString.ToString());
            _connection.Open();
        }

        public void ImportNode(Node node)
        {
            StringBuilder sCommand = new StringBuilder();
            sCommand.Append("INSERT INTO table VALUES");
            sCommand.Append("(");
            sCommand.Append(node.Id);
            sCommand.Append(",");
            sCommand.Append(node.Latitude);
            sCommand.Append(",");
            sCommand.Append(node.Longitude);
            sCommand.Append(")");
            MySqlCommand command = new MySqlCommand();
            command.Connection = _connection;
            command.CommandText = sCommand.ToString();

            command.ExecuteNonQuery();
        }

        public void ImportWay(Way way)
        {
            StringBuilder sCommand = new StringBuilder();
            sCommand.Append("INSERT INTO table VALUES");
            sCommand.Append("(");
            sCommand.Append(way.Id);
            sCommand.Append(")");

            MySqlCommand command = new MySqlCommand();
            command.Connection = _connection;
            command.CommandText = sCommand.ToString();

            command.ExecuteNonQuery();
        }

        public void ImportRelation(Relation relation)
        {
            StringBuilder sCommand = new StringBuilder();
            sCommand.Append("INSERT INTO table VALUES");
            sCommand.Append("(");
            sCommand.Append(relation.Id);
            sCommand.Append(")");

            MySqlCommand command = new MySqlCommand();
            command.Connection = _connection;
            command.CommandText = "INSERT INTO table VALUES()";

            command.ExecuteNonQuery();
        }
    }
}
