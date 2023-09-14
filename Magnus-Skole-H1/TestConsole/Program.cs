using Microsoft.Data.SqlClient;
using SqlLib;
namespace TestConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            SQL _sql = new SQL();
            List<Vare> gg = _sql.GetVarer();
            List<Pluklister> pluklister = _sql.GetPluklister();

            //SqlInterface sql = new SqlInterface();
            //var test = sql.getTableData("vare");
            //using (var conn = new SqlConnection(""))
            //using (var cmd = new SqlCommand("", conn))
            //{
            //    var reader = cmd.ExecuteReader();
            //}
        }
    }
}