using DAL;
namespace BLL
{
    public class BllClass
    {
        public static List<DAL.Kunde> HentAltData()
        {
            var data = DAL.DALClass.HentData();
            return data.OrderBy(x => x.firstName).ToList();
        }
        public static DAL.Kunde HentLinjeData(int id)
        {
            var data = DAL.DALClass.HentData().Where(x => x.cprNummer == id).FirstOrDefault();
            return data;
        }
        public static void RetLinje(DAL.Kunde data)
        {
            DAL.DALClass.RetLinje(data);
        }
        public static void SletLinje(DAL.Kunde data)
        {
            DAL.DALClass.SletLinje(data);
        }
        public static void SletAlt(List<DAL.Kunde> data)
        {
            DAL.DALClass.SletAlt(data);
        }
        public static void OpretLinje(DAL.Kunde data)
        {
            DAL.DALClass.OpretLinje(data);
        }

    }
}