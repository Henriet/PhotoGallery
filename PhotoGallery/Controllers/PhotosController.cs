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
        private readonly PhotoService _photoService = new PhotoService();


        public ActionResult Details(int id)
        {
            var photo = _photoService.Get(id);
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
        public ActionResult Upload(UploadPhotoModel model, HttpPostedFileBase image)
        {
            if (!ModelState.IsValid) return View(model);
           
            var photo = UploadPhotoModel.GetPhotoFromModel(model);
            photo.Path = SavePhotoService.UploadPhoto(image);
            _photoService.Insert(photo, model.GalleryId);

            return RedirectToAction("Edit", "Galleries", new { Id = model.GalleryId });
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id)
        {
            var photo = _photoService.Get(id);

            if (photo == null)
            {
                return HttpNotFound();
            }

            var model = new EditPhotoModel(photo);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(EditPhotoModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var photo = _photoService.Get(model.Id);

            if (photo == null)
                return RedirectToAction("Details", "Photos", new {model.Id });

            model.UpdatePhotoFromModel(photo);
            _photoService.Update(photo);

            return RedirectToAction("Details", "Photos", new{model.Id});
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            Photo photo = _photoService.Get(id);
            var galleryId = photo.Gallery.Id;
            _photoService.Delete(id);

            return RedirectToAction("Edit", "Galleries", galleryId);
        }
    }
}
