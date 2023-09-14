using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Microsoft.Data.SqlClient;
using SqlLib;
namespace SqlLib
{
    public class SqlInterface
    {
        public string connectionString = "Data Source=MN-Laptop\\MAGNUSH1SKOLESQL;Initial Catalog=PluklisteDB;Persist Security Info=True;User ID=sa;Password=101005;TrustServerCertificate=true";
        public void AfslutPluksedel(int id)
        {
            using (SqlConnection myConnection = new SqlConnection(connectionString))
            {
                string oString = $"UPDATE lager.pluklister SET is_done = 1 WHERE plukliste_id = {id};";

                try
                {
                    myConnection.Open();
                    SqlCommand oCmd = new SqlCommand(oString, myConnection);
                    using (SqlDataReader oReader = oCmd.ExecuteReader())
                    {

                        while (oReader.Read())
                        {
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Handle any exceptions here
                    Console.WriteLine("Error: " + ex.Message);
                }
                finally
                {
                    myConnection.Close(); // Close the connection, whether an exception occurred or not
                }
            }
        }

        public void OpdaterLager(string varenummer, int paa_lager)
        {
            using (SqlConnection myConnection = new SqlConnection(connectionString))
            {
                string oString = $"UPDATE lager.vare SET paa_lager = {paa_lager} WHERE varenummer = '{varenummer}';";

                try
                {
                    myConnection.Open();
                    SqlCommand oCmd = new SqlCommand(oString, myConnection);
                    using (SqlDataReader oReader = oCmd.ExecuteReader())
                    {

                        while (oReader.Read())
                        {
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Handle any exceptions here
                    Console.WriteLine("Error: " + ex.Message);
                }
                finally
                {
                    myConnection.Close(); // Close the connection, whether an exception occurred or not
                }
            }
        }
        public IList<T> getTableData<T>(T entryType, string tableName) where T : class // produktTyper
        {
            string fullTableName = "lager." + tableName;
            List<T> list = new List<T>();

            using (SqlConnection myConnection = new SqlConnection(connectionString))
            {
                string oString = $"SELECT * FROM {fullTableName}";

                try
                {
                    myConnection.Open();
                    SqlCommand oCmd = new SqlCommand(oString, myConnection);
                    using (SqlDataReader oReader = oCmd.ExecuteReader())
                    {
                        
                        while (oReader.Read())
                        {
                            list.Add((T)getDecoder(tableName, oReader));
                            //// Read and process data here
                            //string productName = oReader["produktTyper_name"].ToString();
                            //int productID = Convert.ToInt32(oReader["produktType_id"]);
                            //// You can process more columns as needed
                            //Console.WriteLine($"Product Name: {productName}, Product ID: {productID}");
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Handle any exceptions here
                    Console.WriteLine("Error: " + ex.Message);
                }
                finally
                {
                    myConnection.Close(); // Close the connection, whether an exception occurred or not
                }
            }
            return list;
        }
        public static object getDecoder(string tableName, SqlDataReader oReader)
        {
            if (tableName == "kunder")
            {
                return getKunder(oReader);
            }
            else if (tableName == "produktTyper")
            {
                return getProduktTyper(oReader);
            }
            else if (tableName == "vare")
            {
                return getVare(oReader);
            }
            else if (tableName == "forsendelse")
            {
                return getForsendelse(oReader);
            }
            else if (tableName == "pluklister")
            {
                return getPluklister(oReader);
            }
            else if (tableName == "pluklisteLinjer")
            {
                return getPluklisteLinjer(oReader);
            }
            else { throw new Exception("Table name not found: " + tableName); }
        }

        public static Kunder getKunder(SqlDataReader oReader)
        {
            Kunder list = new Kunder();
            list.adresse = oReader["adresse"].ToString();
            list.kunde_id = Convert.ToInt32(oReader["kunde_id"]);
            list.full_name = oReader["full_name"].ToString();
            return list;
        }

        public static ProduktTyper getProduktTyper(SqlDataReader oReader)
        {
            ProduktTyper list = new ProduktTyper();
            list.produktTyper_name = oReader["produktTyper_name"].ToString();
            list.produktType_id = Convert.ToInt32(oReader["produktType_id"]);
            return list;
        }
        public static Vare getVare(SqlDataReader oReader)
        {
            Vare list = new Vare();
            list.varenummer = oReader["varenummer"].ToString();
            list.vare_navn = oReader["vare_navn"].ToString();
            list.vare_type_id = Convert.ToInt32(oReader["vare_type_id"]);
            list.paa_lager = Convert.ToInt32(oReader["paa_lager"]);
            return list;
        }
        public static Forsendelse getForsendelse(SqlDataReader oReader)
        {
            Forsendelse list = new Forsendelse();
            list.forsendelses_type = oReader["forsendelses_type"].ToString();
            list.forsendelse_id = Convert.ToInt32(oReader["forsendelse_id"]);
            return list;
        }
        public static Pluklister getPluklister(SqlDataReader oReader)
        {
            Pluklister list = new Pluklister();
            list.forsendelse_id = Convert.ToInt32(oReader["forsendelse_id"]);
            list.is_done = Convert.ToBoolean(oReader["is_done"]);
            list.kunde_id = Convert.ToInt32(oReader["kunde_id"]);
            list.plukliste_id = Convert.ToInt64(oReader["plukliste_id"]);
            return list;
        }
        public static PluklisteLinjer getPluklisteLinjer(SqlDataReader oReader)
        {
            PluklisteLinjer list = new PluklisteLinjer();
            list.antal = Convert.ToInt32(oReader["antal"]);
            list.master_id = Convert.ToInt64(oReader["master_id"]);
            list.navn = oReader["navn"].ToString(); ;
            list.plukliste_id = Convert.ToInt32(oReader["pluklisteLinjer_id"]); ;
            list.vare = oReader["vare"].ToString();
            return list;
        }
    }
}
