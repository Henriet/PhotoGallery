using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhotoGallery.Domain
{
    public class Gallery
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; protected set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual List<Photo> Photos { get; set; }

        public string CoverPhotoPath { get; set; }

        public Gallery()
        {
        }
    }
}
