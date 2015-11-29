using System;
using System.Collections.Generic;
using Antlr.Runtime.Tree;
using PhotoGallery.Domain;

namespace PhotoGalery.Models
{
    public class GalleryModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid Id { get; set; }

        public GalleryModel(Gallery gallery)
        {
            Id = gallery.Id;
            Name = gallery.Name;
            Description = gallery.Description;
        }

        public GalleryModel() { }
    }
}