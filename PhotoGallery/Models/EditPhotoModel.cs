using System;
using PhotoGallery.Domain;

namespace PhotoGalery.Models
{
    public class EditPhotoModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid GalleryId { get; set; }
    }
}