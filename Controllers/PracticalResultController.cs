using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LSSCBackEnd.Models;
using Microsoft.EntityFrameworkCore;

namespace LSSCBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PracticalResultController : ControllerBase
    {
        lsscPortalContext dataOut = new lsscPortalContext();

        // POST: api/PracticalResult
        [HttpPost]
        public void Post([FromBody] IEnumerable<TblPracticalResult> prResult)
        {
            foreach(TblPracticalResult item in prResult)
            {
                dataOut.TblPracticalResult.Add(item);
                dataOut.SaveChanges();
            } 
            dataOut.Database.ExecuteSqlCommand("exec spPracticalBitChange '"+prResult.First().PrCandidate+"'");
        }
    }
}
