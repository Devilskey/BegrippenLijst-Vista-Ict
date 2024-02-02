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
            if(newConcept == null) 
            { 
                return 500; 
            }

            MySqlCommand addNewConcept = new();
            addNewConcept.CommandText = "INSERT INTO Concept (Title, English_Version, Dutch_Version, Created_At) VALUES (@Title, @English_Ver, @Dutch_Ver, @Created_At);";

            addNewConcept.Parameters.AddWithValue("@Title", newConcept.title);
            addNewConcept.Parameters.AddWithValue("@English_Ver", newConcept.englishVersion);
            addNewConcept.Parameters.AddWithValue("@Dutch_Ver", newConcept.dutchVersion);
            addNewConcept.Parameters.AddWithValue("@Created_At", DateTime.Now);


            using DatabaseHandler database = new DatabaseHandler();

            if (database.Insert(addNewConcept).isSuccesfull)
            {
                return 200;
            }

            return 500;
        }

        [Authorize]
        [HttpPut("EditConcept")]
        public int EditConcept(ConceptListItem newConcept)
        {
            if (newConcept == null)
            {
                return 500;
            }

            MySqlCommand addNewConcept = new MySqlCommand();
            addNewConcept.CommandText = "UPDATE Concept SET Title=@Title, English_Version=@English_Ver, Dutch_Version=@Dutch_Ver WHERE Id=@Id;";

            addNewConcept.Parameters.AddWithValue("@Title", newConcept.Title);
            addNewConcept.Parameters.AddWithValue("@English_Ver", newConcept.EnglishVersion );
            addNewConcept.Parameters.AddWithValue("@Dutch_Ver", newConcept.DutchVersion);
            addNewConcept.Parameters.AddWithValue("@Id", newConcept.Id);


            using DatabaseHandler database = new DatabaseHandler();

            if (database.Insert(addNewConcept).isSuccesfull)
            {
                return 200;
            }

            return 500;
        }

        [Authorize]
        [HttpDelete("DeleteConcept")]
        public int DeleteConcept(int DeleteId)
        {
            if (DeleteId == 0)
            {
                return 500;
            }

            MySqlCommand addNewConcept = new MySqlCommand();
            addNewConcept.CommandText = "DELETE FROM Concept WHERE Id=@Id;";

            addNewConcept.Parameters.AddWithValue("@Id", DeleteId);

            using DatabaseHandler database = new DatabaseHandler();

            if (database.Delete(addNewConcept).isSuccesfull)
            {
                return 200;
            }

            return 500;
        }

        [HttpGet("GetConcepts")]
        public ApiResponse GetConcepts()
        {
            MySqlCommand addNewConcept = new MySqlCommand();
            addNewConcept.CommandText = "SELECT * FROM Concept;";

            using DatabaseHandler database = new DatabaseHandler();

            string json = database.Select(addNewConcept);
            _logger.LogInformation(json);


            if (json == "[]" || json == "[{}]")
                return new ApiResponse(500, "");

            return new ApiResponse(200, json);
        }
    }
}
