using System;
using System.ComponentModel.DataAnnotations;
using PhotoGallery.Domain;

namespace PhotoGalery.Models
{
    public class UploadPhotoModel
    {
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public int GalleryId { get; set; }

        public UploadPhotoModel(int galleryId)
        {
            GalleryId = galleryId;
        }

        public UploadPhotoModel()
        {
        }
    }
}