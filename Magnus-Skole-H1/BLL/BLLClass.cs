using DAL;
namespace BLL
{
    public class BllClass
    {
        private string databasePath;
        private DALClass _dal = new DALClass();
        public BllClass(string path = "Database.json")
        {
            this.databasePath = path;
        }
        public List<DAL.Kunde> HentAltData()
        {
            var data = _dal.HentData();
            return data.OrderBy(x => x.firstName).ToList();
        }
        public DAL.Kunde HentLinjeData(int id)
        {
            var data = _dal.HentData().Where(x => x.cprNummer == id).FirstOrDefault();
            return data;
        }
        public void RetLinje(DAL.Kunde data)
        {
            _dal.RetLinje(data);
        }
        public void SletLinje(DAL.Kunde data)
        {
            _dal.SletLinje(data);
        }
        public void SletAlt(List<DAL.Kunde> data)
        {
            _dal.SletAlt(data);
        }
        public void OpretLinje(DAL.Kunde data)
        {
            _dal.OpretLinje(data);
        }

    }
}