using System.Data.Entity;
using PhotoGallery.Domain;

namespace PhotoGalery.DAL
{
    public class Context : DbContext
    {

        public Context()
            : base("DefaultConnection")
        {
        }


        public DbSet<Photo> Photos { get; set; }
        public DbSet<Gallery> Galleries { get; set; }
        
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Gallery>()
                .HasMany<Photo>(c => c.Photos)
                .WithOptional(x => x.Gallery)
                .WillCascadeOnDelete(true);
        }
    }
}
