using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class LawyerImage : IEntity
    {
        public int Id { get; set; }
        public int LawyerId { get; set; }
        public string ImagePath { get; set; }
        public DateTime Date { get; set; }
    }
}