using Core.Utilities.Results;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ILawyerImageService
    {
        IResult Add(IFormFile file, int lawyerId);
        IResult Delete(LawyerImage lawyerImage);
        IResult Update(IFormFile file, LawyerImage lawyerImage);

        IDataResult<List<LawyerImage>> GetAll();
        IDataResult<List<LawyerImage>> GetByLawyerId(int lawyerId);
        IDataResult<LawyerImage> GetByImageId(int imageId);
        IResult DeleteImageOfLawyerByLawyerId(int lawyerId);
    }
}
