using System;

namespace PhotoGalery.Models
{
    public class UploadPhotoModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid GalleryId { get; set; }

        public UploadPhotoModel(Guid galleryId)
        {
            GalleryId = galleryId;
        }

        public UploadPhotoModel()
        {
        }
    }
}