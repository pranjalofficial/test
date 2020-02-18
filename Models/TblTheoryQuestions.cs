using System;
using System.Collections.Generic;

namespace LSSCBackEnd.Models
{
    public partial class TblTheoryQuestions
    {
        public int TqCode { get; set; }
        public string TqQuestion { get; set; }
        public string TqOption1 { get; set; }
        public string TqOption2 { get; set; }
        public string TqOption3 { get; set; }
        public string TqOption4 { get; set; }
        public int TqCorrectAnswer { get; set; }
        public int TqMarks { get; set; }
        public string TqVersionOfQb { get; set; }
        public string TqLanguage { get; set; }

        public TblQuestionPaperVersion TqVersionOfQbNavigation { get; set; }
    }
}
