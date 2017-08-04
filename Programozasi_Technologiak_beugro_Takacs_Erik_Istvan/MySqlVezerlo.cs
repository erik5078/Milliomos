using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using System.Data.SqlClient;

namespace wpf_beadando
{
    class MySqlVezerlo
    {
        private string connectionString;
        private MySqlConnection connect;
        private static MySqlVezerlo instance;
        private MySqlCommand command;

        public static MySqlVezerlo Instance()
        {
            if (instance == null)
            {
                instance = new MySqlVezerlo();
            }

            return instance;
        }

        private MySqlVezerlo()
        {
            connectionString = "datasource=localhost;port=3306;username=loim;password=loimloim";
        }

        private bool Connect()
        {
            connect = new MySqlConnection(connectionString);
            connect.Open();

            return true;
        }

        public void Insert(string parancs)
        {
            Connect();

            parancs = MySql.Data.MySqlClient.MySqlHelper.EscapeString(parancs);

            command = connect.CreateCommand();
            command.CommandText = parancs;
            command.ExecuteNonQuery();

            Close();
        }

        public void Update(string parancs)
        {
            Insert(parancs);
        }

        private int szamlalo = 0;

        public Results Select(string parancs)
        {
            Connect();

            try
            {
                parancs = MySql.Data.MySqlClient.MySqlHelper.EscapeString(parancs);

                command = connect.CreateCommand();
                command.CommandText = parancs;
                command.ExecuteNonQuery();

                MySqlDataAdapter adapter = new MySqlDataAdapter();
                adapter.SelectCommand = command;
                List<string> sor = new List<string>();

                using (MySqlDataReader dr = command.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        for (int j = 0; j < dr.FieldCount; j++)
                        {
                            sor.Add(dr.GetString(j));
                        }
                    }
                }

                Close();

                if (sor.Count > 0)
                {
                    return new Results(0, sor);
                }

                return new Results(-1, new List<string>());
            }
            catch(Exception e)
            {
                return new Results(-1, new List<string>());
            }
        }

        private bool Close()
        {
            connect.Close();
            return true;
        }
    }
}
