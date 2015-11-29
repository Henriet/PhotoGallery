using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhotoGallery.Domain
{
    public class Photo
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; protected set; }//todo
        public string Path { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual Gallery Gallery { get; set; }//todo

        public DateTime UploadDateTime { get; set; }
        public Photo()
        {
            
        }
    }
}
