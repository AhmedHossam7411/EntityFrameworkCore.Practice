using EFcore.domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFcore.domain
{
    public class league : BaseDomainModel
    {
        public string Name { get; set; }
        public List<Team>? Teams { get; set; } // for one to many relationship
    }
}