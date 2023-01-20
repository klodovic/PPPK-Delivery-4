using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PersonManager.Controllers
{
    public class PersonController : Controller
    {
        private readonly ModelContainer db = new ModelContainer();
        ~PersonController()
        {
            db?.Dispose();
        }




        // GET: Person
        public ActionResult Index()
        {
            return View(db.People);
        }

        // GET: Person/Details/5
        public ActionResult Details(int? id)
        {
            return CommonAction(id);
        }

        // GET: Person/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Person/Create
        [HttpPost]
        public ActionResult Create(Person person)
        {
            if (ModelState.IsValid)
            {
                db.People.Add(person);
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        // GET: Person/Edit/5
        public ActionResult Edit(int? id)
        {
            return CommonAction(id);
        }

        // POST: Person/Edit/5
        [HttpPost]
        public ActionResult Edit(int id)
        {
            Person person = db.People.Find(id);
            if (TryUpdateModel(person, "", new string[]
            {
                nameof(person.FirstName),
                nameof(person.LastName),
                nameof(person.Age),
                nameof(person.Contact)
            }))
            {
                db.Entry(person).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(person);
        }

        // GET: Person/Delete/5
        public ActionResult Delete(int? id)
        {
            return CommonAction(id);
        }

        // POST: Person/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            IEnumerable<Shoe> shoesForDelete = db.Shoes.Where(s => s.IShoe == id);

            foreach (Shoe item in shoesForDelete)
            {
                db.UploadedFiles.RemoveRange(db.UploadedFiles.Where(f => f.ShoeIShoe == id));
            }
            db.Shoes.Remove(db.Shoes.Find(id));
            db.People.Remove(db.People.Find(id));
            db.SaveChanges();
            return RedirectToAction("Index");
        }




        //Metode
        private ActionResult CommonAction(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }

            Person person = db.People.SingleOrDefault(p => p.IDPerson == id);

            if (person == null)
            {
                return HttpNotFound();
            }

            return View(person);
        }
    }
}
