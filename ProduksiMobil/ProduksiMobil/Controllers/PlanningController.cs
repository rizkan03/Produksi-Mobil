using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProduksiMobil.Models;
using ProduksiMobil.DAL;

namespace ProduksiMobil.Controllers
{
    public class PlanningController : Controller
    {
        ApplicationContext db = new ApplicationContext();
        public ActionResult Index()
        {
            // Ambil data history dari DB
            var list = db.Planning.OrderBy(x => x.Id).ToList();
            ViewBag.History = list;

            return View();
        }


        [HttpPost]
        public ActionResult Proses(int senin, int selasa, int rabu, int kamis, int jumat, int sabtu, int minggu)
        {
            var inputList = new List<int>
            {
                senin, selasa, rabu, kamis, jumat, sabtu, minggu
            };

            int total = inputList.Sum();
            var outputList = Ratarata(inputList);

            // Save DB
            Planning data = new Planning
            {
                InputData = string.Join(", ", inputList),
                OutputData = string.Join(", ", outputList),
                TotalProduksi = total,
                CreatedDate = DateTime.Now
            };
            db.Planning.Add(data);
            db.SaveChanges();

            return RedirectToAction("Index");
        }


        private List<int> Ratarata(List<int> input)
        {
            int total = 0;
            int hariKerja = 0;

            foreach (var x in input)
            {
                if (x > 0)
                {
                    total += x;
                    hariKerja++;
                }
            }

            if (hariKerja == 0)
                return new List<int>(input);

            int rata = total / hariKerja;
            int sisa = total % hariKerja;

            var result = new List<int>();

            var prioritas = new List<int>(input);
            prioritas.Sort((a, b) => b.CompareTo(a));

            int threshold = prioritas[Math.Max(0, sisa - 1)];

            foreach (var x in input)
            {
                if (x == 0)
                    result.Add(0);
                else if (sisa > 0 && x >= threshold)
                {
                    result.Add(rata + 1);
                    sisa--;
                }
                else
                {
                    result.Add(rata);
                }
            }

            return result;
        }
    }
}