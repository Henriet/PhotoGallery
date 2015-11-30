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

            return photo != null ? (ActionResult)View(photo) : HttpNotFound();
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
           
            var path = SavePhotoService.UploadPhoto(image);
            _photoService.AddPhotoToGallery(model.Name, model.Description, path, model.GalleryId);

            return RedirectToAction("Details", "Galleries", new { Id = model.GalleryId });
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

            _photoService.UpdatePhoto(model.Id, model.Name, model.Description);

            return RedirectToAction("Details", "Photos", new { model.Id });
        }


        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            Photo photo = _photoService.Get(id);
            var galleryId = photo.Gallery.Id;
            _photoService.Delete(id);

            return RedirectToAction("Details", "Galleries", new { Id = galleryId });
        }
    }
}
