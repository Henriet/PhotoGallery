using PhotoGallery.Domain;

namespace PhotoGalery.DAL
{
    public class PhotoService : Repository<Photo>
    {
        private Repository<Gallery> _galleryRepository = new Repository<Gallery>();

    }
}
