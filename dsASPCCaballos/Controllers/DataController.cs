using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using dsASPCCaballos.Models;
using Microsoft.Extensions.Configuration;
using dsASPCCaballos.DataAccess;
using dsASPCCaballos.Entidades;
using System.Net;

namespace dsASPCCaballos.Controllers
{
    [Produces("application/json")]
    public class DataController : Controller
    {
        private IConfiguration _configuration;
        public DataController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpPost]
        public IActionResult LecturasGenericasPaginadas([FromBody] BusquedaPaginada bs)
        {
            ObjectResult result;
            try
            {
                var res = new LecturasViewModel(_configuration, bs);
                result = new ObjectResult(res)
                {
                    StatusCode = (int)HttpStatusCode.OK
                };
            }
            catch (Exception ex)
            {
                result = new ObjectResult(ex)
                {
                    StatusCode = (int)HttpStatusCode.Conflict
                };
                Request.HttpContext.Response.Headers.Add("dsError", ex.Message);
            }
            return result;
        }
        [HttpGet]
        public IActionResult ParticipantesLeer()
        {
            ObjectResult result;
            try
            {
                var ad = new AdaptadorCaballos(_configuration);
                var res = ad.ParticipantesLeer();
                result = new ObjectResult(res)
                {
                    StatusCode = (int)HttpStatusCode.OK
                };
            }
            catch (Exception ex)
            {
                result = new ObjectResult(ex)
                {
                    StatusCode = (int)HttpStatusCode.Conflict
                };
                Request.HttpContext.Response.Headers.Add("dsError", ex.Message);
            }
            return result;
        }
        

    }
}
