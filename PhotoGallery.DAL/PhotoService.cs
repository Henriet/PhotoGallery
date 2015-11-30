using System;
using PhotoGallery.Domain;

namespace PhotoGalery.DAL
{
    public class PhotoService : Repository<Photo>
    {
        private readonly Repository<Gallery> _galleryRepository = new Repository<Gallery>(); 

        public Photo AddPhotoToGallery(string name, string description, string path, int galleryId)
        {
            try
            {
                var gallery = _galleryRepository.Get(galleryId);
                var photo = new Photo
                {
                    Gallery = gallery,
                    Path = path,
                    Name = name,
                    Description = description
                };

                gallery.Photos.Add(photo);
                _galleryRepository.Update(gallery);
                return photo;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public void UpdatePhoto(int id, string name, string description)
        {
            try
            {
                var photo = Get(id);
                if (photo == null) return;

                photo.Name = name;
                photo.Description = description;
                Update(photo);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            
        }
    }
}
