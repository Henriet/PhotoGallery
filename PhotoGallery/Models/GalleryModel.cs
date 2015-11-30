using System.ComponentModel.DataAnnotations;
using PhotoGallery.Domain;

namespace PhotoGalery.Models
{
    public class GalleryModel
    {
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public int Id { get; set; }

        public GalleryModel(Gallery gallery)
        {
            Id = gallery.Id;
            Name = gallery.Name;
            Description = gallery.Description;
        }

        public Gallery GetGalleryFromModel()
        {
            var gallery = new Gallery
            {
                Name = Name,
                Description = Description
            };

            return gallery;
        }

        public GalleryModel() { }
    }
}