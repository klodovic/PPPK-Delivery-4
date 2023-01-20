using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace PersonManager.Controllers
{
    public class ShoeController : Controller
    {

        private readonly ModelContainer db = new ModelContainer();
        ~ShoeController()
        {
            db?.Dispose();
        }




        // GET: Shoe
        public ActionResult Index(int id)
        {
            ViewBag.id = id;
            return View(db.Shoes);
        }

        // GET: Shoe/Details/5
        public ActionResult Details(int? id)
        {
            return CommonAction(id);
        }

        // GET: Shoe/Create
        public ActionResult Create(int id)
        {
            ViewBag.IDPerson = id;
            return View();
        }

        // POST: Shoe/Create
        [HttpPost]
        public ActionResult Create(Shoe shoe, IEnumerable<HttpPostedFileBase> files)
        {
            if (ModelState.IsValid)
            {
                shoe.UploadedFiles = new List<UploadedFile>();
                AddFiles(shoe, files);
                db.Shoes.Add(shoe);
                db.SaveChanges();
            }

            return RedirectToAction("Index", "Person"); 
        }

        // GET: Shoe/Edit/5
        public ActionResult Edit(int? id)
        {
            return CommonAction(id);
        }

        // POST: Shoe/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, IEnumerable<HttpPostedFileBase> files)
        {
            Shoe shoe = db.Shoes.Find(id);
            if (TryUpdateModel(shoe, "", new string[]
            {
                nameof(shoe.Name),   
                nameof(shoe.Size),   
                nameof(shoe.IDPerson)
            }))
            {
                AddFiles(shoe, files);
                db.Entry(shoe).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "Person");
            }
            
            return View(shoe);
        }

        // GET: Shoe/Delete/5
        public ActionResult Delete(int? id)
        {
            return CommonAction(id);
        }

        // POST: Shoe/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            db.UploadedFiles.RemoveRange(db.UploadedFiles.Where(f => f.ShoeIShoe == id));
            db.Shoes.Remove(db.Shoes.Find(id));
            db.SaveChanges();
            return RedirectToAction("Index", "Person");
        }



        //Metode
        private ActionResult CommonAction(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }

            Shoe shoe = db.Shoes
                .Include(s => s.UploadedFiles)
                .SingleOrDefault(s => s.IShoe == id);

            if (shoe == null)
            {
                return HttpNotFound();
            }

            return View(shoe);
        }

        private void AddFiles(Shoe shoe, IEnumerable<HttpPostedFileBase> files)
        {
            foreach (var file in files)
            {
                if (file != null && file.ContentLength > 0)
                {
                    var picture = new UploadedFile
                    {
                        Name = file.FileName,
                        ContentType = file.ContentType
                    };
                    using (var reader = new System.IO.BinaryReader(file.InputStream))
                    {
                        picture.Content = reader.ReadBytes(file.ContentLength);
                    }
                    shoe.UploadedFiles.Add(picture);
                }
            }
        }
    }
}
