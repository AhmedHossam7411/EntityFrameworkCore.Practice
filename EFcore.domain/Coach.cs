using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFcore.domain;  // following with EF core -full tour udemy course

public class Coach : BaseDomainModel  // Data Model Or table 
{
    public string Name { get; set; }
    public int? TeamId { get; set; }

}
