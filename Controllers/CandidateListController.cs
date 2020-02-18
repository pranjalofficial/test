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
    public class CandidateListController : ControllerBase
    {
        lsscPortalContext dataOut = new lsscPortalContext();
        // GET: api/CandidateList
        [HttpGet("{id}")]
        public IEnumerable<TblCandidateList> Get(int id)
        {
            List<TblCandidateList> candidateList;
            candidateList = dataOut.TblCandidateList.FromSql("exec spCandidateList " + id).ToList();
            return candidateList;
        }

       
    }
}
