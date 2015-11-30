using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhotoGallery.Domain
{
    public class Photo
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; protected set; }

        [Required]
        public string Path { get; set; }

        [Required]
        [StringLength(60, MinimumLength = 1)]
        public string Name { get; set; }

        public string Description { get; set; }

        public virtual Gallery Gallery { get; set; }
        
        public DateTime UploadDateTime { get; set; }

        public Photo()
        { }
    }
}
