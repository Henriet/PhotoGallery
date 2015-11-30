using System;
using System.ComponentModel.DataAnnotations;
using PhotoGallery.Domain;

namespace PhotoGalery.Models
{
    public class EditPhotoModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public int GalleryId { get; set; }

        public EditPhotoModel(Photo photo)
        {
            Description = photo.Description;
            Name = photo.Name;
            Id = photo.Id;
        }

        public void UpdatePhotoFromModel(Photo photo)
        {
            photo.Name = Name;
            photo.Description = Description;
        }

        public EditPhotoModel()
        {}
    }
}