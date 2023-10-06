using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Plukliste;
using SqlLib;
using System.Reflection;

namespace PluklisteWeb.Controllers
{
    public class PluklisterController : Controller
    {
        // GET: PluklisterController
        public ActionResult Index()
        {
            //BLL _bll = new BLL("C:\\PluklisteOpgave\\import", "C:\\PluklisteOpgave\\export", "C:\\PluklisteOpgave\\print", "C:\\PluklisteOpgave\\letterTemplates");
            //var data = _bll.readListOfFiles();
            //List<Pluklist> dataToSend = new List<Pluklist>();

            //foreach (var item in data)
            //{
            //    dataToSend.Add(_bll.readFile(item));
            //}

            SQL _sql = new SQL();
            var data = _sql.GetPluklister();
            return View(data);
        }

        // GET: PluklisterController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: PluklisterController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PluklisterController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PluklisterController/Edit/5
        public ActionResult Edit(int id)
        {
            //BLL _bll = new BLL("C:\\PluklisteOpgave\\import", "C:\\PluklisteOpgave\\export", "C:\\PluklisteOpgave\\print", "C:\\PluklisteOpgave\\letterTemplates");

            //var data = _bll.readFile(filename);
            //List<Pluklist> dataToSend = new List<Pluklist>();
            //dataToSend.Add(data);
            SQL _sql = new SQL();
            var data = _sql.GetPlukliste(id);
            List<Pluklister> dataToSend = new List<Pluklister>();
            dataToSend.Add(data);
            return View(dataToSend);
        }

        public ActionResult EditStorage(string varenummer)
        {
            SQL _sql = new SQL();
            var data = _sql.GetVare(varenummer);
            return View(data);
        }

        public IActionResult OnPostAfslutPluksedel(int id)
        {
            SQL _sql = new SQL();
            _sql.AfslutPlukseddel(id);

            ExportFromSql exportFromSql = new ExportFromSql();
            exportFromSql.Export(id);

            return RedirectToAction("Index");
            // Your method logic goes here
        }

        [HttpPost]
        public IActionResult EditStorage(Vare model) // You can bind to your model class here.
        {

            SQL _sql = new SQL();
            _sql.OpdaterLagerantal(model.varenummer, model.paa_lager);
            // Redirect to a success page or another action.
            return RedirectToAction("Storage");
        }


        public IActionResult OnPostUpdateStorage(int vare)
        {
            Console.WriteLine("");
            return RedirectToAction("EditStorage");
        }

        public ActionResult Storage()
        {
            SQL _sql = new SQL();
            List<Vare> alleVare = _sql.GetVarer();
            return View(alleVare);
        }

        // POST: PluklisterController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PluklisterController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PluklisterController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
