using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFaculty.Domain.Enums
{
    public enum TaskSubmissionStatus
    {
        PendingSubmission = 1,
        SentForEvaluation = 2,
        SentBack = 3,
        Evaluated = 4
    }
}
