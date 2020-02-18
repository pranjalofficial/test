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
    public class QuestionPaperController : ControllerBase
    {
        lsscPortalContext dataOut = new lsscPortalContext();
        // GET: api/QuestionPaper
        [HttpGet("{id}")]
        public IEnumerable<TblTheoryQuestions> Get(int id)
        {
            List<TblTheoryQuestions> theoryQue;
            theoryQue = dataOut.TblTheoryQuestions.FromSql("exec spQuestionPaperTheory "+id).ToList();
            return theoryQue;
        }

    }
}
