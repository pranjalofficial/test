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
    public class CenterAssesorInfoController : ControllerBase
    {
        lsscPortalContext dataOut = new lsscPortalContext();
        // GET: api/CenterAssesorInfo/5
        [HttpGet("{id}")]
        public CenterAssesor Get(int id)
        {
            CenterAssesor ca;
            try
            {
                ca = dataOut.CenterAssesors.FromSql("exec spCenterAssesor " + id).Single();
            }
            catch (Exception)
            {
                return null;
            }
            return ca;
        }

    }
}
