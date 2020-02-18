using System;
using System.Collections.Generic;

namespace LSSCBackEnd.Models
{
    public partial class TblNos
    {
        public TblNos()
        {
            TblPracticalQuestion = new HashSet<TblPracticalQuestion>();
        }

        public string NosCode { get; set; }
        public string NosName { get; set; }
        public bool NosActive { get; set; }
        public string NosQpCode { get; set; }

        public TblQp NosQpCodeNavigation { get; set; }
        public ICollection<TblPracticalQuestion> TblPracticalQuestion { get; set; }
    }
}
