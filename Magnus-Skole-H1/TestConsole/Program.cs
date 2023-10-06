using Microsoft.Data.SqlClient;
using SqlLib;
namespace TestConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ExportFromSql exportFromSql = new ExportFromSql();
            exportFromSql.Export(3);

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