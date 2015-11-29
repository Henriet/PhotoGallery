using System;
using System.Data.Entity;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using PhotoGalery.DAL;
using PhotoGalery.Models;
using PhotoGalery.Services;
using PhotoGallery.Domain;

namespace PhotoGalery.Controllers
{
    public class GalleriesController : Controller
    {
        private readonly IRepository<Gallery> _repository = new Repository<Gallery>();

        public ActionResult Index()
        {
            SetAdminRights();
             return View(_repository.All());
        }
        
        public ActionResult Details(Guid? id)
        {
            SetAdminRights();
            if (!id.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gallery gallery = _repository.Get(id.Value);
            if (gallery == null)
            {
                return HttpNotFound();
            }
            var model = new GalleryModel(gallery);
            return View(model);
        }

        [Authorize(Roles = "Admin")] 
        public ActionResult Create()
        {
            return View();
        }

        // POST: Galleries/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(GalleryModel model, HttpPostedFileBase photo)
        {
            if (!ModelState.IsValid) return View(model);
            var path = String.Empty;
            if (photo != null)
            {
                path = SavePhotoService.UploadPhoto(photo);
            }
            var gallery = new Gallery
            {
                Name = model.Name,
                Description = model.Description,
                CoverPhotoPath = path
            };
                
            _repository.Insert(gallery);
            return RedirectToAction("Index");
        }

        // GET: Galleries/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gallery gallery = _repository.Get(id.Value);

            if (gallery == null)
            {
                return HttpNotFound();
            }
            return View(gallery); //todo view with editing images, description and title, cover photo
        }

        // POST: Galleries/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( Gallery model, Guid Id)
        {
            if (ModelState.IsValid)
            {

                _repository.Update(model);

                return RedirectToAction("Index");
            }
            
            return View(model);
        }


        // POST: Galleries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            _repository.Delete(id);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _repository.Dispose();
            }
            base.Dispose(disposing);
        }

        private void SetAdminRights()
        {
            var isAdmin = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>().IsInRole(User.Identity.GetUserId(), "Admin");
            ViewBag.IsAdmin = isAdmin;
        }
    }
}
