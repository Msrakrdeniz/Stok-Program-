using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mvc_Stok.Models.Entity;

namespace Mvc_Stok.Controllers
{
    public class MusteriController : Controller
    {
        MvcDbStokEntities db = new MvcDbStokEntities();

        // GET: Musteri
        public ActionResult Index(string p)
        {
            var degerler = from d in db.TBL_MUSTERILER select d;
            if (!string.IsNullOrEmpty(p))
            {
                degerler = degerler.Where(m => m.MUSTERIAD.Contains(p));
            }
            return View(degerler.ToList());
          //  var musteri = db.TBL_MUSTERILER.ToList();
          //  return View(musteri);
        }

        [HttpGet]
        public ActionResult YeniMusteri()
        {
            return View();
        }

        [HttpPost]
        public ActionResult YeniMusteri(TBL_MUSTERILER p2)
        {
            if (!ModelState.IsValid)
            {
                return View("YeniMusteri");
            }
            db.TBL_MUSTERILER.Add(p2);
            db.SaveChanges();
            return View();
        }


        public ActionResult SIL(int id)
        {
            var musteri = db.TBL_MUSTERILER.Find(id);
            db.TBL_MUSTERILER.Remove(musteri);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult MusteriGetir(int id)
        {
            var mstr = db.TBL_MUSTERILER.Find(id);
            return View("MusteriGetir", mstr);
        }


        public ActionResult Guncelle(TBL_MUSTERILER p1)
        {
            var mstr = db.TBL_MUSTERILER.Find(p1.MUSTERİID);
            mstr.MUSTERIAD = p1.MUSTERIAD;
            mstr.MUSTERİSOYAD = p1.MUSTERİSOYAD;
            db.SaveChanges();
            return RedirectToAction("Index");

        }

    }
}               


            