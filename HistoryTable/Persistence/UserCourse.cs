using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HistoryTable.Persistence;

public class UserCourse
{
    public int UserId { get; set; }
    public int CourseId { get; set; }
    public DateTime DateEnrolled { get; set; }
}
