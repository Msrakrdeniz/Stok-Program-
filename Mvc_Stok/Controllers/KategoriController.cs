using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mvc_Stok.Models.Entity;
using PagedList;
using PagedList.Mvc;

namespace Mvc_Stok.Controllers
{
    public class KategoriController : Controller
    {
        // GET: Kategori
        MvcDbStokEntities db = new MvcDbStokEntities();
        public ActionResult Index(int sayfa=1)
        {
            // var degerler = db.TBL_KATEGORİLER.ToList();

            var degerler = db.TBL_KATEGORİLER.ToList().ToPagedList(sayfa, 4);
            return View(degerler);
        }

        [HttpGet]
        public ActionResult Yenikategori()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Yenikategori(TBL_KATEGORİLER p1)
        {
            if(!ModelState.IsValid)
            {
                return View("YeniKategori");
            }
            db.TBL_KATEGORİLER.Add(p1);
            db.SaveChanges();
            return View();
        }


        public ActionResult SIL(int id)
        {
            var kategori = db.TBL_KATEGORİLER.Find(id);
            db.TBL_KATEGORİLER.Remove(kategori);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult KategoriGetir(int id)
        {
            var ktgr = db.TBL_KATEGORİLER.Find(id);
            return View("KategoriGetir",ktgr);

        }

        public ActionResult Guncelle(TBL_KATEGORİLER p1)
        {
            var ktg = db.TBL_KATEGORİLER.Find(p1.KATEGORID);
            ktg.KATEGORİAD = p1.KATEGORİAD;
            db.SaveChanges();
            return RedirectToAction("Index");

        }
        


    }
}