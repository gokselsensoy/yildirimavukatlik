using Business.Abstract;
using Business.Constants;
using Core.Business;
using Core.Utilities.Helpers.FileHelper;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class LawyerImageManager : ILawyerImageService
    {
        ILawyerImageDal _lawyerImageDal;
        ILawyerDal _lawyerDal;

        public LawyerImageManager(ILawyerImageDal lawyerImageDal, ILawyerDal lawyerDal)
        {
            _lawyerImageDal = lawyerImageDal;
            _lawyerDal = lawyerDal;
        }
        public IResult Add(IFormFile file, int lawyerId)
        {
            IResult result = BusinessRules.Run(CheckIfLawyerImageLimitExceeded(lawyerId));
            if (result != null)
            {
                return result;
            }

            var imageResult = FileHelperManager.Upload(file);
            if (!imageResult.Success)
            {
                return new ErrorResult(imageResult.Message);
            }

            LawyerImage lawyerImage = new LawyerImage
            {
                ImagePath = imageResult.Message,
                LawyerId = lawyerId,
                Date = DateTime.Now
            };
            _lawyerImageDal.Add(lawyerImage);
            return new SuccessResult(Messages.LawyerImageAdded);
        }

        public IResult Delete(LawyerImage lawyerImage)
        {
            IResult rulesResult = BusinessRules.Run(CheckIfLawyerImageIdExist(lawyerImage.Id));
            if (rulesResult != null)
            {
                return rulesResult;
            }

            var deletedImage = _lawyerImageDal.Get(l => l.Id == lawyerImage.Id);
            var result = FileHelperManager.Delete(deletedImage.ImagePath);
            if (!result.Success)
            {
                return new ErrorResult(Messages.ErrorDeletingImage);
            }
            _lawyerImageDal.Delete(deletedImage);
            return new SuccessResult(Messages.LawyerImageDeleted);
        }
        public IResult Update(IFormFile file, LawyerImage lawyerImage)
        {
            IResult rulesResult = BusinessRules.Run(CheckIfLawyerImageIdExist(lawyerImage.Id),
                CheckIfLawyerImageLimitExceeded(lawyerImage.Id));
            if (rulesResult != null)
            {
                return rulesResult;
            }

            var updatedImage = _lawyerImageDal.Get(l => l.Id == lawyerImage.Id);
            var result = FileHelperManager.Update(file, updatedImage.ImagePath);
            if (!result.Success)
            {
                return new ErrorResult(Messages.ErrorUpdatingImage);
            }
            lawyerImage.ImagePath = result.Message;
            lawyerImage.Date = DateTime.Now;
            _lawyerImageDal.Update(lawyerImage);
            return new SuccessResult(Messages.LawyerImageUpdated);
        }

        private IResult CheckIfLawyerImageLimit(int lawyerId)
        {
            var result = _lawyerImageDal.GetAll(c => c.Id == lawyerId).Count;
            if (result >= 5)
            {
                return new ErrorResult();
            }
            return new SuccessResult();
        }

        public IDataResult<List<LawyerImage>> GetByLawyerId(int lawyerId)
        {
            var result = BusinessRules.Run(CheckLawyerImage(lawyerId));
            if (result != null)
            {
                return new ErrorDataResult<List<LawyerImage>>(GetDefaultImage(lawyerId).Data);
            }
            return new SuccessDataResult<List<LawyerImage>>(_lawyerImageDal.GetAll(c => c.LawyerId == lawyerId));
        }

        public IDataResult<LawyerImage> GetByImageId(int imageId)
        {
            return new SuccessDataResult<LawyerImage>(_lawyerImageDal.Get(c => c.Id == imageId));
        }

        public IDataResult<List<LawyerImage>> GetAll()
        {
            return new SuccessDataResult<List<LawyerImage>>(_lawyerImageDal.GetAll());
        }

        public IResult DeleteImageOfLawyerByLawyerId(int lawyerId)
        {
            var deletedImages = _lawyerImageDal.GetAll(l => l.LawyerId == lawyerId);
            if (deletedImages == null)
            {
                return new ErrorResult(Messages.NoPictureOfTheLawyer);
            }
            foreach (var deletedImage in deletedImages)
            {
                _lawyerImageDal.Delete(deletedImage);
                FileHelperManager.Delete(deletedImage.ImagePath);
            }
            return new SuccessResult(Messages.LawyerImageDeleted);
        }

        private IDataResult<List<LawyerImage>> GetDefaultImage(int lawyerId)
        {

            List<LawyerImage> lawyerImage = new List<LawyerImage>();
            lawyerImage.Add(new LawyerImage { Id = lawyerId, Date = DateTime.Now, ImagePath = "DefaultImage.jpg" });
            return new SuccessDataResult<List<LawyerImage>>(lawyerImage);
        }
        private IResult CheckLawyerImage(int lawyerId)
        {
            var result = _lawyerImageDal.GetAll(c => c.Id == lawyerId).Count;
            if (result > 0)
            {
                return new SuccessResult();
            }
            return new ErrorResult();
        }

        private IResult CheckIfLawyerImageIdExist(int imageId)
        {
            var result = _lawyerImageDal.GetAll(l => l.Id == imageId).Any();
            if (!result)
            {
                return new ErrorResult(Messages.LawyerImageIdNotExist);
            }
            return new SuccessResult();
        }

        private IResult CheckIfLawyerImageLimitExceeded(int lawyerId)
        {
            int result = _lawyerImageDal.GetAll(l => l.LawyerId == lawyerId).Count;
            if (result >= 5)
            {
                return new ErrorResult(Messages.LawyerImageLimitExceeded);
            }
            return new SuccessResult();
        }
    }
}
