using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Plukliste;

namespace PluklisteWeb.Controllers
{
    public class PluklisterController : Controller
    {
        // GET: PluklisterController
        public ActionResult Index()
        {
            BLL _bll = new BLL("C:\\PluklisteOpgave\\import", "C:\\PluklisteOpgave\\export", "C:\\PluklisteOpgave\\print", "C:\\PluklisteOpgave\\letterTemplates");
            var data = _bll.readListOfFiles();
            List<Pluklist> dataToSend = new List<Pluklist>();

            foreach (var item in data)
            {
                dataToSend.Add(_bll.readFile(item));
            }
            return View(dataToSend);
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
        public ActionResult Edit(string filename)
        {
            BLL _bll = new BLL("C:\\PluklisteOpgave\\import", "C:\\PluklisteOpgave\\export", "C:\\PluklisteOpgave\\print", "C:\\PluklisteOpgave\\letterTemplates");

            var data = _bll.readFile(filename);
            List<Pluklist> dataToSend = new List<Pluklist>();
            dataToSend.Add(data);
            return View(dataToSend);
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
