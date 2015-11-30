using System;
using PhotoGallery.Domain;

namespace PhotoGalery.DAL
{
    public class PhotoService : Repository<Photo>
    {
        private readonly Repository<Gallery> _galleryRepository = new Repository<Gallery>(); 

        public Photo Insert(Photo photo, int galleryId)
        {
            try
            {
                var gallery = _galleryRepository.Get(galleryId);
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
    }
}
