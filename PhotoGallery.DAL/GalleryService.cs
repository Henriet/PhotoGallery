﻿using PhotoGallery.Domain;

namespace PhotoGalery.DAL
{
    public class GalleryService : Repository<Gallery>
    {
        private readonly PhotoService _photoService = new PhotoService();
        public void UpdateGallery(string name, string description, int id)
        {
            var gallery = Get(id);
            gallery.Name = name;
            gallery.Description = description;

            Update(gallery);
        }

        public void DeleteGalleryWithPhotos(int id)
        {
            Delete(id);
        }
    }
}
