using System;
using System.Collections.Generic;

namespace LSSCBackEnd.Models
{
    public partial class TblTrainingPartner
    {
        public int TpCode { get; set; }
        public string TpName { get; set; }
        public bool TpActive { get; set; }
    }
}
