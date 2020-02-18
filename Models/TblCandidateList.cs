using System;
using System.Collections.Generic;

namespace LSSCBackEnd.Models
{
    public partial class TblCandidateList
    {
        public TblCandidateList()
        {
            TblAssessmentProof = new HashSet<TblAssessmentProof>();
            TblFinalResult = new HashSet<TblFinalResult>();
            TblPracticalResult = new HashSet<TblPracticalResult>();
            TblTheoryResult = new HashSet<TblTheoryResult>();
            TblTotalPracticalMarks = new HashSet<TblTotalPracticalMarks>();
        }

        public string ClEnrollmentNo { get; set; }
        public string ClName { get; set; }
        public int? ClReqNo { get; set; }
        public bool? ClPracticalDone { get; set; }
        public bool? ClTheoryDeone { get; set; }

        public ICollection<TblAssessmentProof> TblAssessmentProof { get; set; }
        public ICollection<TblFinalResult> TblFinalResult { get; set; }
        public ICollection<TblPracticalResult> TblPracticalResult { get; set; }
        public ICollection<TblTheoryResult> TblTheoryResult { get; set; }
        public ICollection<TblTotalPracticalMarks> TblTotalPracticalMarks { get; set; }
    }
}
