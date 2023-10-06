using Microsoft.Data.SqlClient;

namespace SqlLib
{
    public class SQL
    {
       

        public SqlInterface _sqlinterface = new SqlInterface();
        public List<Vare> GetVarer()
        {
            List<Vare> vare = (List<Vare>)_sqlinterface.getTableData(new Vare(), "vare");
            List<ProduktTyper> produktTyper = (List<ProduktTyper>)_sqlinterface.getTableData(new ProduktTyper(), "produktTyper");

            foreach (var item in vare)
            {
                item.vare_type = produktTyper.Where(x => x.produktType_id == item.vare_type_id).FirstOrDefault().produktTyper_name;
            }
            return vare;
        }
        public Vare GetVare(string varenummer)
        {
            var allevare = GetVarer();
            var vare = allevare.Where(x => x.varenummer == varenummer).FirstOrDefault();
            return vare;
        }


        public List<Pluklister> GetPluklister()
        {
            List<Pluklister> pluklister = (List<Pluklister>)_sqlinterface.getTableData(new Pluklister(), "pluklister");
            List<Kunder> kunder = (List<Kunder>)_sqlinterface.getTableData(new Kunder(), "kunder");
            List<Forsendelse> forsendelser = (List<Forsendelse>)_sqlinterface.getTableData(new Forsendelse(), "forsendelse");
            List<PluklisteLinjer> pluklisteLinjer = (List<PluklisteLinjer>)_sqlinterface.getTableData(new PluklisteLinjer(), "pluklisteLinjer");
            foreach(var pluk in pluklister)
            {
                pluk.kunde_navn = kunder.Where(x => x.kunde_id == pluk.kunde_id).FirstOrDefault().full_name;
                pluk.kunde_adresse = kunder.Where(x => x.kunde_id == pluk.kunde_id).FirstOrDefault().adresse;
                pluk.forsendelse = forsendelser.Where(x => x.forsendelse_id == pluk.forsendelse_id).FirstOrDefault().forsendelses_type;
                pluk.antal_linjer = pluklisteLinjer.Where(x => x.master_id == pluk.plukliste_id).Count();
                pluk.linjer = GetPluklisteLinjer(pluk.plukliste_id);
            }
            return pluklister.Where(x => x.is_done == false).ToList();
        }
        public List<Pluklister> GetPluklisterAll()
        {
            List<Pluklister> pluklister = (List<Pluklister>)_sqlinterface.getTableData(new Pluklister(), "pluklister");
            List<Kunder> kunder = (List<Kunder>)_sqlinterface.getTableData(new Kunder(), "kunder");
            List<Forsendelse> forsendelser = (List<Forsendelse>)_sqlinterface.getTableData(new Forsendelse(), "forsendelse");
            List<PluklisteLinjer> pluklisteLinjer = (List<PluklisteLinjer>)_sqlinterface.getTableData(new PluklisteLinjer(), "pluklisteLinjer");
            foreach (var pluk in pluklister)
            {
                pluk.kunde_navn = kunder.Where(x => x.kunde_id == pluk.kunde_id).FirstOrDefault().full_name;
                pluk.kunde_adresse = kunder.Where(x => x.kunde_id == pluk.kunde_id).FirstOrDefault().adresse;
                pluk.forsendelse = forsendelser.Where(x => x.forsendelse_id == pluk.forsendelse_id).FirstOrDefault().forsendelses_type;
                pluk.antal_linjer = pluklisteLinjer.Where(x => x.master_id == pluk.plukliste_id).Count();
                pluk.linjer = GetPluklisteLinjer(pluk.plukliste_id);
            }
            return pluklister;
        }
        public Pluklister GetPlukliste(int id)
        {
            var allePluklister = GetPluklisterAll();
            var plukliste = allePluklister.Where(x => x.plukliste_id == id).FirstOrDefault();
            return plukliste;
        }

        public List<PluklisteLinjer> GetPluklisteLinjer(long id)
        {
            List<PluklisteLinjer> allePluklisteLinjer = (List<PluklisteLinjer>)_sqlinterface.getTableData(new PluklisteLinjer(), "pluklisteLinjer");
            List<PluklisteLinjer> linjerForDenne = allePluklisteLinjer.Where(x => x.master_id == id).ToList();
            List<Vare> varer = GetVarer();
            foreach(var linje in linjerForDenne)
            {
                linje.vare_type = varer.Where(x => x.varenummer == linje.vare).FirstOrDefault().vare_type;
                linje.vare_antal_paa_lager = varer.Where(x => x.varenummer == linje.vare).FirstOrDefault().paa_lager;
            }
            return linjerForDenne;
        }

        public void AfslutPlukseddel(int id)
        {
            _sqlinterface.AfslutPluksedel(id);
        }
        public void OpdaterLagerantal(string varenummer, int paa_lager)
        {
            _sqlinterface.OpdaterLager(varenummer, paa_lager);
        }



    }



}