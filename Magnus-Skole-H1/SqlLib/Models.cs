using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SqlLib
{

    public class Kunder
    {
        public int kunde_id { get; set; }
        public string full_name { get; set; }
        public string adresse { get; set; }
    }
    public class ProduktTyper
    {
        public int produktType_id { get; set; }
        public string produktTyper_name { get; set; }
    }
    public class Vare
    {
        public string varenummer { get; set; }
        public string vare_navn { get; set; }
        public int vare_type_id { get; set; }

        public int paa_lager { get; set; }

        public string vare_type { get; set; }
    }
    public class Forsendelse
    {
        public int forsendelse_id { get; set; }
        public string forsendelses_type { get; set; }
    }
    public class Pluklister
    {
        public long plukliste_id { get; set; }
        public int kunde_id { get; set; }
        public int forsendelse_id { get; set; }
        public bool is_done { get; set; }


        public string kunde_navn { get; set; }
        public string kunde_adresse { get; set; }
        public string forsendelse { get; set; }
        public int antal_linjer { get; set; }

        public List<PluklisteLinjer> linjer { get; set; }
    }
    public class PluklisteLinjer
    {
        public int plukliste_id { get; set; }
        public long master_id { get; set; }
        public string vare { get; set; }
        public string navn { get; set; }
        public int antal { get; set; }

        public string master { get; set; }
        public string vare_type { get; set; }
        public long vare_antal_paa_lager { get; set; }

    }


}
