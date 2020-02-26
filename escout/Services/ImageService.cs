﻿using escout.Models;
using System.Collections.Generic;
using System.Linq;

namespace escout.Services
{
    public class ImageService : BaseService
    {
        DataContext db;

        public ImageService()
        {
            db = new DataContext();
        }

        public List<Image> CreateImage(List<Image> image)
        {
            db.images.AddRange(image);
            db.SaveChanges();
            return image;
        }

        public bool UpdateImage(Image image)
        {
            try
            {
                db.images.Update(image);
                db.SaveChanges();
                return true;
            }
            catch { return false; }
        }

        public bool DeleteImage(int id)
        {
            try
            {
                var image = db.images.FirstOrDefault(i => i.id == id);
                db.images.Remove(image);
                db.SaveChanges();
                return true;
            }
            catch { return false; }
        }

        public Image GetImage(int id)
        {
            return db.images.FirstOrDefault(i => i.id == id);
        }
    }
}
