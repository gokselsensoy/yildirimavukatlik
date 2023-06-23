using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ILawyerService
    {
        IDataResult<List<Lawyer>> GetAll();
        IDataResult<List<Lawyer>> GetLawyersBySortId(int sortId);
        IDataResult<LawyerDetailDto> GetLawyerDetails(int lawyerId);
        IDataResult<int> Add(Lawyer lawyer);
        IResult Update(Lawyer lawyer);
        IResult Delete(Lawyer lawyer);
    }
}
