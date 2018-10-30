using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Pos.Arquitetura.TrabalhoFinal.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class ServicoController : Controller
    {

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
            //Chamar WCF Rest
        }
    }
}
