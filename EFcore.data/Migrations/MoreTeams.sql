using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFcore.data.Migrations
{
    class MoreTeams
    {
       insert into teams(name , CreatedDate)
       values
       ('alAhaly' , date() ),
       ('elmazareta' , date() ),
       ('benfica' , date() ),
       ('roma' , date() ),
       ('barcelona' , date() ),
    }
}
