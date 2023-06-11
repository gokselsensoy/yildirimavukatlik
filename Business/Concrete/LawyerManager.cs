using Business.Abstract;
using Business.BusinessAspects;
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Core.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class LawyerManager : ILawyerService
    {
        private readonly ILawyerDal _lawyerDal;
        private readonly ILawyerImageService _lawyerImageService;
        public LawyerManager(ILawyerDal lawyerDal, ILawyerImageService lawyerImageService)
        {
            _lawyerDal = lawyerDal;
            _lawyerImageService = lawyerImageService;
        }

        //[SecuredOperation("admin")]
        [CacheRemoveAspect("ILawyerService.Get")]
        public IDataResult<int> Add(Lawyer lawyer)
        {
            _lawyerDal.Add(lawyer);
            var result = _lawyerDal.Get(l =>
            l.Name == lawyer.Name &&
            l.Position == lawyer.Position &&
            l.Description == lawyer.Description);
            if (result != null)
            {
                return new SuccessDataResult<int>(result.Id, Messages.LawyerAdded);
            }
            return new ErrorDataResult<int>(-1, "Avukat eklenirken bir sorun olutşu.");
        }

        //[SecuredOperation("admin")]
        [CacheRemoveAspect("ILawyerService.Get")]
        public IResult Delete(Lawyer lawyer)
        {
            var result = BusinessRules.Run(CheckIfLawyerIdExist(lawyer.Id));
            if (result != null)
            {
                return result;
            }

            var deletedLawyer = _lawyerDal.Get(l => l.Id == lawyer.Id);
            _lawyerImageService.DeleteImageOfLawyerByLawyerId(deletedLawyer.Id);
            _lawyerDal.Delete(deletedLawyer);
            return new SuccessResult(Messages.LawyerDeleted);
        }

        [CacheAspect]
        public IDataResult<List<Lawyer>> GetAll()
        {
            return new SuccessDataResult<List<Lawyer>>(_lawyerDal.GetAll());
        }

        //[SecuredOperation("admin")]
        [CacheRemoveAspect("ILawyerService.Get")]
        public IResult Update(Lawyer lawyer)
        {
            var result = BusinessRules.Run(CheckIfLawyerIdExist(lawyer.Id));
            if (result != null)
            {
                return result;
            }
            _lawyerDal.Update(lawyer);
            return new SuccessResult(Messages.LawyerUpdated);
        }

        public IDataResult<LawyerDetailDto> GetLawyerDetails(int lawyerId)
        {
            return new SuccessDataResult<LawyerDetailDto>(_lawyerDal.GetLawyerDetails().SingleOrDefault((l => l.Id == lawyerId)), Messages.LawyerUpdated);
        }

        private IResult CheckIfLawyerIdExist(int lawyerId)
        {
            var result = _lawyerDal.GetAll(l => l.Id == lawyerId).Any();
            if (!result)
            {
                return new ErrorResult(Messages.LawyerNotExist);
            }
            return new SuccessResult();
        }

    }
}
