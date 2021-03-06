﻿using System.ComponentModel.DataAnnotations;
using System.Linq;
using PagedList;
using PhotoGallery.Domain;

namespace PhotoGalery.Models
{
    public class GalleryDetailsModel
    {
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public int Id { get; set; }

        public IPagedList<Photo> Photos { get; set; }

        public GalleryDetailsModel(Gallery gallery, int pageNumber, int pageSize)
        {
            Id = gallery.Id;
            Name = gallery.Name;
            Description = gallery.Description;
            Photos = gallery.Photos.OrderBy(photo => photo.UploadDateTime).ToPagedList(pageNumber, pageSize);

        }
    }
}