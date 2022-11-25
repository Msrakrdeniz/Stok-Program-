using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mvc_Stok.Models.Entity;

namespace Mvc_Stok.Controllers
{
    public class UrunController : Controller
    {
        MvcDbStokEntities db = new MvcDbStokEntities();
        // GET: Urun
        public ActionResult Index()
        {
            var urun = db.TBL_URUNLER.ToList();
            return View(urun);
        }

        [HttpGet]
        public ActionResult Yeniurun()
        {
            List<SelectListItem> degerler = (from i in db.TBL_KATEGORİLER.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = i.KATEGORİAD,
                                                 Value = i.KATEGORID.ToString()
                                             }).ToList();

            ViewBag.dgr = degerler;
            return View();
        }


        [HttpPost]
        public ActionResult Yeniurun(TBL_URUNLER p3)
        {
            var ktg = db.TBL_KATEGORİLER.Where(m => m.KATEGORID == p3.TBL_KATEGORİLER.KATEGORID).FirstOrDefault();//yukarıda ki value'ya ulaşmak için yapılan eşleştirme işlemi
            p3.TBL_KATEGORİLER = ktg; //ktg'den gelen değeri p3.tbl.katg ata
            db.TBL_URUNLER.Add(p3);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        public ActionResult SIL(int id)
        {
            var urun = db.TBL_URUNLER.Find(id);
            db.TBL_URUNLER.Remove(urun);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult UrunGetir(int id)
        {
          var urun = db.TBL_URUNLER.Find(id);

            List<SelectListItem> degerler = (from i in db.TBL_KATEGORİLER.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = i.KATEGORİAD,
                                                 Value = i.KATEGORID.ToString()
                                             }).ToList();

            ViewBag.dgr = degerler;

            return View("UrunGetir", urun);
        }

        public ActionResult Guncelle(TBL_URUNLER p)
        {
            var urun = db.TBL_URUNLER.Find(p.URUNID);
            urun.URUNAD = p.URUNAD;
            urun.MARKA = p.MARKA;
            //urn.URUNKATEGORI = p.URUNKATEGORI;
            var ktg = db.TBL_KATEGORİLER.Where(m => m.KATEGORID == p.TBL_KATEGORİLER.KATEGORID).FirstOrDefault();
            urun.URUNKATEGORI = ktg.KATEGORID;
            urun.FİYAT = p.FİYAT;
            urun.STOK = p.STOK;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}