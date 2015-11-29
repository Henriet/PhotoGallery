using System;
using System.Web;
using System.Web.Mvc;
using PhotoGalery.DAL;
using PhotoGalery.Models;
using PhotoGalery.Services;
using PhotoGallery.Domain;

namespace PhotoGalery.Controllers
{
    public class PhotosController : Controller
    {
        private readonly Repository<Photo> _repository = new Repository<Photo>();
        private readonly Repository<Gallery> _galleryRepository = new Repository<Gallery>();


        public ActionResult Details(int id)
        {
            var photo = _repository.Get(id);
            if (photo == null)
            {
                return HttpNotFound();
            }
            return View(photo);
        }


        [Authorize(Roles = "Admin")]
        public ActionResult Upload(int galleryId)
        {
            var model = new UploadPhotoModel(galleryId);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Upload(UploadPhotoModel model, HttpPostedFileBase image, int galleryId) //todo
        {
            if (!ModelState.IsValid) return View(model);
            var path = SavePhotoService.UploadPhoto(image);
            
            var photo = new Photo
            {
                Name = model.Name,
                Description = model.Description,
                Path = path,
                UploadDateTime = DateTime.Now
            };
            var gallery = _galleryRepository.Get(galleryId);
            gallery.Photos.Add(photo);

            _galleryRepository.Update(gallery); //todo

            return RedirectToAction("Edit", "Galleries", new { Id = galleryId });
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id)
        {
            Photo photo = _repository.Get(id);

            if (photo == null)
            {
                return HttpNotFound();
            }
            var model = new EditPhotoModel
            {
                Description = photo.Description,
                Name = photo.Name,
                Id = photo.Id
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(EditPhotoModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var photo = _repository.Get(model.Id);
            if (photo == null ||
                (model.Name == photo.Name && model.Description == photo.Description))
                return RedirectToAction("Index", "Galleries");//todo

            photo.Description = model.Description;
            photo.Name = model.Name;
            _repository.Update(photo);

            return RedirectToAction("Details", "Photos", model.Id);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            Photo photo = _repository.Get(id);
            var galleryId = photo.Gallery.Id;
            _repository.Delete(id);

            return RedirectToAction("Edit", "Galleries", galleryId);
        }
    }
}
