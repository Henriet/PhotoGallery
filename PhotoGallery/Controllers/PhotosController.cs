using System;
using System.Net;
using System.Web.Mvc;
using PhotoGalery.DAL;
using PhotoGalery.Models;
using PhotoGallery.Domain;

namespace PhotoGalery.Controllers
{
    public class PhotosController : Controller
    {
        private readonly Repository<Photo> _repository = new Repository<Photo>();
        private readonly Repository<Gallery> _galleryRepository = new Repository<Gallery>();

        private const int PageSize = 9;

        // GET: Photos/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var photo = _repository.Get(id.Value);
            if (photo == null)
            {
                return HttpNotFound();
            }
            return View(photo);
        }

        public ActionResult GetPhotosForPage(Guid galleryId, int pageNumber)
        {
            var gallery = _galleryRepository.Get(galleryId);
            var photos = gallery.Photos.GetRange(pageNumber*PageSize - 1, PageSize);

            return PartialView(photos);
        }

        // GET: Photos/Create
        public ActionResult Upload()
        {
            return View();
        }

        // POST: Photos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Upload(Photo photo)
        {
            if (ModelState.IsValid)
            {
              //todo
            }

            return View(photo);
        }

        // GET: Photos/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Photo photo = _repository.Get(id.Value);
            
            if (photo == null)
            {
                return HttpNotFound();
            }
            var model = new EditPhotoModel
            {
                Description = photo.Description,
                Name = photo.Name,
                GalleryId = photo.Gallery.Id,
                Id = photo.Id
            };
            return View(model);
        }

        // POST: Photos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditPhotoModel model)
        {
            if (ModelState.IsValid)
            {
                var photo = _repository.Get(model.Id);
                if (photo != null)
                {
                    photo.Gallery = _galleryRepository.Get(model.GalleryId);
                    photo.Description = model.Description;
                    photo.Name = model.Name;
                    _repository.Update(photo);
                    return RedirectToAction("Edit", "Galleries", model.GalleryId);
                }
                return RedirectToAction("Index", "Galleries");
            }
            return View(model);
        }


        // POST: Photos/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Guid id)
        {
            Photo photo = _repository.Get(id);
            var galleryId = photo.Gallery.Id;
            _repository.Delete(id);
            return RedirectToAction("Edit",  "Galleries", galleryId);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _repository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
