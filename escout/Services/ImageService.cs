using escout.Helpers;
using escout.Models;
using System.Collections.Generic;
using System.Linq;

namespace escout.Services
{
    public class ImageService : BaseService
    {
        private readonly DataContext context;
        public ImageService(DataContext context) => this.context = context;

        public List<Image> CreateImage(List<Image> image)
        {
            image.ToList().ForEach(i => i.created = Utils.GetDateTime());
            image.ToList().ForEach(i => i.updated = Utils.GetDateTime());
            context.images.AddRange(image);
            context.SaveChanges();
            return image;
        }

        public bool UpdateImage(Image image)
        {
            try
            {
                image.updated = Utils.GetDateTime();
                context.images.Update(image);
                context.SaveChanges();
                return true;
            }
            catch { return false; }
        }

        public bool DeleteImage(int id)
        {
            try
            {
                var image = context.images.FirstOrDefault(i => i.id == id);
                context.images.Remove(image);
                context.SaveChanges();
                return true;
            }
            catch { return false; }
        }

        public Image GetImage(int id)
        {
            return context.images.FirstOrDefault(i => i.id == id);
        }
    }
}
