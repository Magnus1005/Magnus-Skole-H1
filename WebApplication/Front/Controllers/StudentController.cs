using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BLL;
using System.Linq;
using NuGet.Packaging.Signing;

namespace Front.Controllers
{
    public class StudentController : Controller
    {
        // GET: StudentController
        public ActionResult Index()
        {
            List<DAL.DalClass.DataList> data = BLL.BllClass.HentAltData();
            return View(data);
        }

        // GET: StudentController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: StudentController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: StudentController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(DAL.DalClass.DataList collection)
        {
            try
            {
                if (collection.id == 0)
                {
                    var altData = BLL.BllClass.HentAltData();
                    altData.OrderBy(n => n.id).ToArray();
                    bool idFound = false;
                    int idCount = 1;

                    while (idFound == false)
                    {
                        var linjerData = altData.Where(x => x.id == idCount).FirstOrDefault();
                        if (linjerData == null)
                        {
                            collection.id = idCount;
                            idFound = true;
                        }
                        else
                        {
                            idCount++;
                        }
                    }

                    BLL.BllClass.OpretLinje(collection);
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: StudentController/Edit/5
        public ActionResult Edit(int id)
        {
            var data = BLL.BllClass.HentLinjeData(id);
            return View(data);
        }

        // POST: StudentController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, DAL.DalClass.DataList collection)
        {
            try
            {                
                BLL.BllClass.RetLinje(collection);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: StudentController/Delete/5
        public ActionResult Delete(int id)
        {
            var data = BLL.BllClass.HentLinjeData(id);
            return View(data);
        }

        // POST: StudentController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, DAL.DalClass.DataList collection)
        {
            try
            {
                BLL.BllClass.SletLinje(collection);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
