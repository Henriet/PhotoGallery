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
        public int Id { get; protected set; }

        [StringLength(60, MinimumLength = 3)]
        public string Name { get; set; }//todo valid datatags

        public string Description { get; set; }

        public virtual List<Photo> Photos { get; set; }

          [StringLength(60, MinimumLength = 10)]

        public string CoverPhotoPath { get; set; }

        public Gallery()
        {}
    }
}
