using System;
using PhotoGallery.Domain;

namespace PhotoGalery.DAL
{
    public class PhotoService : Repository<Photo>
    {
        private readonly Repository<Gallery> _galleryRepository = new Repository<Gallery>(); 

        public Photo AddPhotoToGallery(string path, Photo photo)
        {
            try
            {
                if (photo.GalleryId == null) return photo;
                var gallery = _galleryRepository.Get(photo.GalleryId.Value);
                photo.UploadDateTime = DateTime.Now;
                photo.Path = path;
                photo.Gallery = gallery;

                gallery.Photos.Add(photo);
                _galleryRepository.Update(gallery);
                return photo;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public void UpdatePhoto(int id, Photo photo)
        {
            try
            {
                var photoFromDb = Get(id);
                if (photoFromDb == null) return;

                photoFromDb.Name = photo.Name;
                photoFromDb.Description = photo.Description;
                Update(photoFromDb);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            
        }

        public void DeletePhoto(Photo photo)
        {
            Delete(photo.Id);
        }
    }
}
