using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Model;
using DAL;
using DAL.Infrastructure;
using Web.Core;

namespace BLL
{
    public interface IPhotoService
    {
        ProductPhoto GetProductPhotoById(int id);
        CategoryPhoto GetCategoryPhotoById(int id);
        ProductPhoto DefaultProductPhoto();
        CategoryPhoto DefaultCategoryPhoto();

        byte[] GetThumbnailPhotoFile(Photo photo, int width, int height);
            
    }
    public class PhotoService : IPhotoService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IProductPhotoRepository productPhotoRepository;
        private readonly ICategoryPhotoRepository categoryPhotoRepository;
        public PhotoService(IUnitOfWork unitOfWork, IProductPhotoRepository productPhotoRepository, ICategoryPhotoRepository categoryPhotoRepository)
        {
            var context = unitOfWork.Context;
            productPhotoRepository.Context = context;
            categoryPhotoRepository.Context = context;

            this.unitOfWork = unitOfWork;
            this.productPhotoRepository = productPhotoRepository;
            this.categoryPhotoRepository = categoryPhotoRepository;
        }
        public ProductPhoto GetProductPhotoById(int id)
        {
            return productPhotoRepository.GetById(id);
        }
        public CategoryPhoto GetCategoryPhotoById(int id)
        {
            return categoryPhotoRepository.GetById(id);
        }
        public ProductPhoto DefaultProductPhoto()
        {
            return productPhotoRepository.GetAll().FirstOrDefault();
        }
        public CategoryPhoto DefaultCategoryPhoto()
        {
            return categoryPhotoRepository.GetAll().FirstOrDefault();
        }
        public byte[] GetThumbnailPhotoFile(Photo photo, int width, int height)
        {
            byte[] photoByteFile = photo.FileBytes;
            System.Drawing.Image sourceImage = ImageHelper.GetImageFromBytes(photoByteFile);

            System.Drawing.Image thumbnailImage;
            if (width > 0 && height > 0)
            {
                thumbnailImage = ImageHelper.GetFixedSizeThumbnail(sourceImage, width, height);
            }
            else
            {
                thumbnailImage = ImageHelper.GetThumbnail(sourceImage, width, height);
            }

            byte[] thumbnailFile = ImageHelper.ConvertImageToByteArray(thumbnailImage, "jpeg");
            return thumbnailFile;
        }

        void Save()
        {
            unitOfWork.Commit();
        }
    }
}
