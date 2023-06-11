using Core.DataAccess;
using Core.DataAccess.EntityFramework;
using DataAccess.Concrete;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface ILawyerDal : IEntityRepository<Lawyer>
    {
        List<LawyerDetailDto> GetLawyerDetails(Expression<Func<LawyerDetailDto, bool>> filter = null);
    }
}
