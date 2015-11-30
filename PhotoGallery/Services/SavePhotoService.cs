using System.IO;
using System.Web;

namespace PhotoGalery.Services
{
    public static class SavePhotoService
    {
        private const string BasePath = "/Content/Photo/";
        private static readonly string DefaultImagePath = Path.Combine(BasePath, "no-image.png");
        public static string UploadPhoto(HttpPostedFileBase imagesfiles)
        {
            if (imagesfiles == null)
            {
                return DefaultImagePath;
            }

            var fileName = imagesfiles.FileName;
            var path = Path.Combine("~", HttpContext.Current.Server.MapPath(BasePath), fileName);

            var data = new byte[imagesfiles.ContentLength];
            imagesfiles.InputStream.Read(data, 0, imagesfiles.ContentLength);

            using (var sw = new FileStream(path, FileMode.Create))
            {
                sw.Write(data, 0, data.Length);
            }

            return BasePath + fileName;
        }
    }
}