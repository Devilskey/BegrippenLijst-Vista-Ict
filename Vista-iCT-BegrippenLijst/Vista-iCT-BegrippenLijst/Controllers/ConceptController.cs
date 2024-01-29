using Handlers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using Objects;

namespace Vista_iCT_BegrippenLijst.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ConceptController : Controller
    {
        public ILogger<ConceptController> _logger { get; set; }
        public ConceptController(ILogger<ConceptController> logger) 
        {
            _logger = logger;
        }

        [Authorize]
        [HttpPost("AddConcept")]
        public int AddConcept(NewConcept newConcept)
        {
        }

        [Authorize]
        [HttpPut("EditConcept")]
        public int EditConcept(NewConcept newConcept)
        {
        }

        [Authorize]
        [HttpDelete("DeleteConcept")]
        public int DeleteConcept(int DeleteId)
        {
        }

        [Authorize]
        [HttpGet("GetConcepts")]
        public ApiResponse GetConcepts()
        {
        }
    }
}
