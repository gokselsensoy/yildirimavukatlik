using Core.Entities;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class LawyerDetailDto : IDto
    {
        public int Id { get; set; }
        public List<LawyerImage> LawyerImages { get; set; }
    }
}
