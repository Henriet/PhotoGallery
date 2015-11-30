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
            var photo = new Photo
            {
                GalleryId = galleryId
            };
            return View(photo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Upload(Photo model, HttpPostedFileBase image)
        {
            if (!ModelState.IsValid) return View(model);
           
            var path = SavePhotoService.UploadPhoto(image);
            _photoService.AddPhotoToGallery(path, model);

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
            return View(photo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(Photo model, int id)
        {
            if (!ModelState.IsValid) return View(model);

            _photoService.UpdatePhoto(id, model);

            return RedirectToAction("Details", "Galleries", new { Id =  model.GalleryId });
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
