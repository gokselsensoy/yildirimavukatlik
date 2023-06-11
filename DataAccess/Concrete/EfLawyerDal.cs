using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete
{
    public class EfLawyerDal : EfEntityRepositoryBase<Lawyer, YildirimDatabaseContext>, ILawyerDal
    {
        public List<LawyerDetailDto> GetLawyerDetails(Expression<Func<LawyerDetailDto, bool>> filter = null)
        {
            using (YildirimDatabaseContext context = new YildirimDatabaseContext())
            {

                var primaryResult = from l in context.Lawyers
                                    join b in context.LawyerImages
                                    on l.Id equals b.LawyerId into lawyerImages
                                    from lawyerImage in lawyerImages.DefaultIfEmpty()
                                    select new
                                    {
                                        Lawyer = l,
                                        LawyerImage = lawyerImage
                                    };

                var result = primaryResult.ToList().Select(x => new LawyerDetailDto
                {
                    Id = x.Lawyer.Id,
                    LawyerImages = x.LawyerImage != null ? new List<LawyerImage> { x.LawyerImage } : new List<LawyerImage> { new LawyerImage { Id = -1, LawyerId = x.Lawyer.Id, Date = DateTime.Now, ImagePath = "/images/default.jpg" } },
                }).ToList();

                return result;
            }
        }
    }
}
