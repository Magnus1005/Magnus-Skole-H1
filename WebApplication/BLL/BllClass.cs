using DAL;
namespace BLL
{
    public class BllClass
    {
        public static List<DAL.DalClass.DataList> HentAltData()
        {
            var data = DAL.DalClass.HentData();
            return data.OrderBy(x => x.id).ToList();
        }
        public static DAL.DalClass.DataList HentLinjeData(int id)
        {
            var data = DAL.DalClass.HentData().Where(x => x.id == id).FirstOrDefault();
            return data;
        }
        public static void RetLinje(DAL.DalClass.DataList data)
        {
            DAL.DalClass.RetLinjeJson(data);
        }
        public static void RetAlt(List<DAL.DalClass.DataList> data)
        {
            DAL.DalClass.RetAlt(data);
        }
        public static void SletLinje(DAL.DalClass.DataList data)
        {
            DAL.DalClass.SletLinje(data);
        }
        public static void SletAlt(List<DAL.DalClass.DataList> data)
        {
            DAL.DalClass.SletAlt(data);
        }
        public static void OpretLinje(DAL.DalClass.DataList data)
        {
            DAL.DalClass.OpretLinje(data);
        }

    }
}