using System;
using System.Web;
using System.Web.Mvc;
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
             return View(_repository.All());
        }
        

        public ActionResult Details(int id, int page = 1)
        {
            Gallery gallery = _repository.Get(id);
            if (gallery == null)
            {
                return HttpNotFound();
            }
            var model = new GalleryDetailsModel(gallery, page, 9);

            return View(model);
        }

        [Authorize(Roles = "Admin")] 
        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")] 
        public ActionResult Create(GalleryModel model, HttpPostedFileBase photo)
        {
            if (!ModelState.IsValid) return View(model);

            var path = SavePhotoService.UploadPhoto(photo);

            var gallery = new Gallery
            {
                Name = model.Name,
                Description = model.Description,
                CoverPhotoPath = path
            };
                
            _repository.Insert(gallery);//todo
            return RedirectToAction("Index");
        }


        public ActionResult Edit(int id)
        {
            Gallery gallery = _repository.Get(id);

            if (gallery == null)
            {
                return HttpNotFound();
            }
            return View(gallery); 
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")] 
        public ActionResult Edit( Gallery model, int id)
        {
            if (!ModelState.IsValid) return View(model);

            var gallery = _repository.Get(id);
            if(gallery.Description != model.Description || gallery.Name != model.Name)
                _repository.Update(model);

            return RedirectToAction("Index");
        }
        
        [Authorize(Roles = "Admin")] 
        public ActionResult Delete(int id)
        {
            _repository.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
