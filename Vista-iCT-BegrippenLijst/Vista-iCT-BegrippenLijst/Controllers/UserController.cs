using Handlers;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using Objects;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;

namespace Vista_iCT_BegrippenLijst.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;

        /// <summary>
        /// Inits the User controller and injects the ILogger 
        /// </summary>
        public UserController(ILogger<UserController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// The api call to login to a users account
        /// </summary>
        /// <param name="login"></param>
        /// <returns>JWT Token string</returns>
        [HttpPost("Login")]
        public Task<string> Login(LoginData login)
        {
            //Checks if the password or email are empty if one of them is empty Return string with status code 500
            if (login.Password == "" || login.Email == "")
            {
                return Task.FromResult<string>("Empty String detected code 500");
            }

            //Creates a new MySqlCommand and gives it a select query using a parameter for the @email
            // The select query is used the check out if there is a user with the given email adress.
            MySqlCommand SelectUserData = new MySqlCommand();

            SelectUserData.CommandText = "SELECT Email, Password FROM Administrator WHERE Email=@Email;";
            
            //Sets the @Email parameter to the value of login.Email
            SelectUserData.Parameters.AddWithValue("@Email", login.Email);

            //Creates a new database handler object and does the select statement after it.
            using DatabaseHandler database = new DatabaseHandler();

            string JsonDataReturn = database.Select(SelectUserData);

            // changes the json it returned to an LoginData array after that is checked if the LoginData array is null or the length is 0
            LoginData[]? ReturnedAdminData = JsonConvert.DeserializeObject<LoginData[]>(JsonDataReturn);

            if (ReturnedAdminData == null || ReturnedAdminData.Length == 0)
            {
                return Task.FromResult<string>("Error No user found with this email adress.");
            }

            //Checks if the password is the same as the first accounts password. if not it returns the error code Password not Valid
            if (!BCryptHandler.VerifyPassword(login.Password, ReturnedAdminData[0].Password))
            {
                return Task.FromResult<string>("Password Not Valid");
            }

            // If the password was valid it returns the JWT token.
            return Task.FromResult<string>(JWTtokenHandler.GenerateToken());
        }

        /// <summary>
        /// Changes the email adres of the user.
        /// an autorization code is needed to use this api call
        /// </summary>
        /// <param name="email"></param>
        /// <returns>int status code</returns>
        [Authorize]
        [HttpPut("ChangeEmail")]
        public Task<int> ChangeEmail(ChangeEmailObject email)
        {
            // Checks if the both the email adress and verify email adress are not null or an empty string
            // After that it checks if the the email adresses are not the same. 
            // if one of these if statements are true status code 500 is returned.
            if (string.IsNullOrEmpty(email.Email) && string.IsNullOrEmpty(email.EmailVerify))
                return Task.FromResult<int>(500);

            if (email.Email != email.EmailVerify)
                return Task.FromResult<int>(500);


            //Creates a new database handler object
            using DatabaseHandler database = new DatabaseHandler();

            // Creates a new MySqlCommand object and gives it an update query with the parameters @Email and @Id
            MySqlCommand changePasswordSql = new MySqlCommand();
            changePasswordSql.CommandText = "UPDATE Administrator SET Email=@Email WHERE Id=@Id;";

            //Replaces the parameter in the update query with the email adress and the id of the user.
            //Note the id of the user is 1 by default.
            changePasswordSql.Parameters.AddWithValue("@Email", email.Email);
            changePasswordSql.Parameters.AddWithValue("@Id", 1);

            //Changes the password and checks if the was a successfull atempt
            if (database.Update(changePasswordSql).isSuccesfull)
                return Task.FromResult<int>(200);
            return Task.FromResult<int>(500);
        }

        /// <summary>
        /// Changes the password of the user
        /// /// an autorization code is needed to use this api call
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPut("ChangePassword")]
        public Task<int> ChangePassword(ChangePasswordObject password)
        {
            //Checks if the password given and verify password are not empty or equal to 0 
            //After that check if the password and password verify are not equal to each other. 
            //If one if these if statements is true return status code 500.
            if(string.IsNullOrEmpty(password.Password) || string.IsNullOrEmpty(password.PasswordVerify))
                return Task.FromResult<int>(503);

            if(password.Password != password.PasswordVerify)
                return Task.FromResult<int>(500);

            //Creates a new MysqlCommand object and gives it the Update query with the parameters @password and @Id
            MySqlCommand changePasswordSql = new MySqlCommand();
            changePasswordSql.CommandText = "UPDATE Administrator SET Password=@password WHERE Id=@Id;";

            //Changes the parameters new password and id to the values 
            changePasswordSql.Parameters.AddWithValue("@password", BCryptHandler.BcrypyBasicEncryption(password.Password));
            changePasswordSql.Parameters.AddWithValue("@Id", 1);

            //Creates a new database handler object
            using DatabaseHandler database = new DatabaseHandler();

            //Sends the sql query and checks if the query was succesfull
            if (database.Update(changePasswordSql).isSuccesfull)
                return Task.FromResult<int>(200);
            return Task.FromResult<int>(500);
        }

    }
}