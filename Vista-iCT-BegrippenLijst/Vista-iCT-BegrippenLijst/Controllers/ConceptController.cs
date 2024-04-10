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

        /// <summary>
        /// Inits the Concept controller and injects the ILogger 
        /// </summary>
        /// <param name="logger"></param>
        public ConceptController(ILogger<ConceptController> logger) 
        {
            _logger = logger;
        }


        /// <summary>
        /// Api call to add a new concept needs the new concept object as a parameter.
        /// To use this api call you need an authorization token.
        /// </summary>
        /// <param name="newConcept"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost("AddConcept")]
        public int AddConcept(NewConcept newConcept)
        {
            // checks if the newConcept object is empty
            if(newConcept == null) 
            { 
                return 500; 
            }

            // Creates a new sql command and gives and makes the command text an insert query.
            // A parameter is used for every item so that sql injection is not possible.
            MySqlCommand addNewConcept = new MySqlCommand();
            addNewConcept.CommandText = "INSERT INTO Concept (Title, English_Version, Dutch_Version, Created_At) VALUES (@Title, @English_Ver, @Dutch_Ver, @Created_At);";

            // Inserts replaces the parameter with the real value. 
            addNewConcept.Parameters.AddWithValue("@Title", newConcept.title);
            addNewConcept.Parameters.AddWithValue("@English_Ver", newConcept.englishVersion);
            addNewConcept.Parameters.AddWithValue("@Dutch_Ver", newConcept.dutchVersion);
            addNewConcept.Parameters.AddWithValue("@Created_At", DateTime.Now);

            // Creates a new database handeler object.
            using DatabaseHandler database = new DatabaseHandler();

            // Sends the insert query to the database and checks if it is succesfull or not.
            if (database.Insert(addNewConcept).isSuccesfull)
            {
                return 200;
            }

            return 500;
        }

        /// <summary>
        /// Edits a concept in the database it needs the newConcept object as a parameter.
        /// To use this api call you need an authorization token.
        /// </summary>
        /// <param name="newConcept"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPut("EditConcept")]
        public int EditConcept(ConceptListItem newConcept)
        {
            // checks if the newConcept object is empty
            if (newConcept == null)
            {
                return 500;
            }

            // Creates a new sql command and gives and makes the command text an Update query.
            // A parameter is used for every item so that sql injection is not possible.
            MySqlCommand addNewConcept = new MySqlCommand();
            addNewConcept.CommandText = "UPDATE Concept SET Title=@Title, English_Version=@English_Ver, Dutch_Version=@Dutch_Ver WHERE Id=@Id;";

            // Inserts replaces the parameter with the real value. 
            addNewConcept.Parameters.AddWithValue("@Title", newConcept.Title);
            addNewConcept.Parameters.AddWithValue("@English_Ver", newConcept.EnglishVersion );
            addNewConcept.Parameters.AddWithValue("@Dutch_Ver", newConcept.DutchVersion);
            addNewConcept.Parameters.AddWithValue("@Id", newConcept.Id);

            // Creates a new database handler object.
            using DatabaseHandler database = new DatabaseHandler();

            // Sends the Update query to the database and checks if it is succesfull or not.
            if (database.Update(addNewConcept).isSuccesfull)
            {
                return 200;
            }

            return 500;
        }

        /// <summary>
        /// DeleteConcept
        /// </summary>
        /// <param name="DeleteId"></param>
        /// <returns></returns>
        [Authorize]
        [HttpDelete("DeleteConcept")]
        public int DeleteConcept(int DeleteId)
        {
            //Checks if the id is not equal to 0 cuz that id would not exist in the database
            if (DeleteId == 0)
            {
                return 500;
            }

            // Creates a new sql command and gives and makes the command text an Delete query.
            // A parameter is used for every item so that sql injection is not possible.
            MySqlCommand addNewConcept = new MySqlCommand();
            addNewConcept.CommandText = "DELETE FROM Concept WHERE Id=@Id;";

            // adds the value of the @Id parameter 
            addNewConcept.Parameters.AddWithValue("@Id", DeleteId);

            //Creates a new Datbase handler object. 
            using DatabaseHandler database = new DatabaseHandler();

            //Sends the query to the database and checks if it was succesfull
            if (database.Delete(addNewConcept).isSuccesfull)
            {
                return 200;
            }

            return 500;
        }

        /// <summary>
        /// returns all the concepts that are stored in the database.
        /// </summary>
        /// <returns>ApiResponse object it has a status code and a json</returns>
        [HttpGet("GetConcepts")]
        public ApiResponse GetConcepts()
        {
            // Creates a new MySqlCommand object and gives it a select * 
            MySqlCommand addNewConcept = new MySqlCommand();
            addNewConcept.CommandText = "SELECT * FROM Concept;";

            //Creates a new Datbase handler object. 
            using DatabaseHandler database = new DatabaseHandler();

            // Sends the select statement to the database and returns the data in json format.
            string json = database.Select(addNewConcept);

            // Checks if the json is empty if its empty then send a 500 status code
            if (json == "[]" || json == "[{}]")
                return new ApiResponse(500, "");

            // returns the json together with a 200 status code
            return new ApiResponse(200, json);
        }
    }
}
