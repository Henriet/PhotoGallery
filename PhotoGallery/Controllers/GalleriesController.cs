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
        private readonly GalleryService _repository = new GalleryService();
        private const int PageSize = 9;

        public ActionResult Index()
        {
             return View(_repository.All());
        }
        

        public ActionResult Details(int id, int page = 1)
        {
            var gallery = _repository.Get(id);
            if (gallery == null)
            {
                return HttpNotFound();
            }
            var model = new GalleryDetailsModel(gallery, page, PageSize);

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
        public ActionResult Create(Gallery gallery, HttpPostedFileBase photo)
        {
            if (!ModelState.IsValid) return View(gallery);
            
            gallery.CoverPhotoPath = SavePhotoService.UploadPhoto(photo);
                
            gallery = _repository.Insert(gallery);
            return RedirectToAction("Details", new{gallery.Id});
        }


        public ActionResult Edit(int id)
        {
            var gallery = _repository.Get(id);

            return gallery != null ? (ActionResult) View(gallery) : HttpNotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")] 
        public ActionResult Edit( Gallery model, int Id)
        {
            if (!ModelState.IsValid) return View(model);
            _repository.UpdateGallery(model.Name, model.Description, Id);

            return RedirectToAction("Index");
        }
        
        [Authorize(Roles = "Admin")] 
        public ActionResult Delete(int id)
        {
            _repository.DeleteGalleryWithPhotos(id);
            return RedirectToAction("Index");
        }
    }
}
