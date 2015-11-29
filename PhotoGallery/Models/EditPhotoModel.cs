using System;
using PhotoGallery.Domain;

namespace PhotoGalery.Models
{
    public class EditPhotoModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int GalleryId { get; set; }
    }
}