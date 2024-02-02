using Handlers;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using Objects;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;
using Google.Protobuf.Collections;
using Microsoft.IdentityModel.Tokens;

namespace Vista_iCT_BegrippenLijst.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;

        public UserController(ILogger<UserController> logger)
        {
            _logger = logger;
        }

        [HttpPost("Login")]
        public Task<string> Login(LoginData login)
        {
            if (login.Password == "" || login.Email == "")
            {
                return Task.FromResult<string>("Empty String detected code 500");
            }

            using DatabaseHandler database = new();

            MySqlCommand SelectUserData = new MySqlCommand();

            SelectUserData.CommandText = "SELECT Email, Password FROM Administrator WHERE Email=@Email;";
            SelectUserData.Parameters.AddWithValue("@Email", login.Email);

            string JsonDataReturn = database.Select(SelectUserData);


            LoginData[]? ReturnedAdminData = JsonConvert.DeserializeObject<LoginData[]>(JsonDataReturn);

            if (ReturnedAdminData == null || ReturnedAdminData.Length == 0)
            {
                return Task.FromResult<string>("Error No user found with this email adress.");
            }

            if (!BCryptHandler.VerifyPassword(login.Password, ReturnedAdminData[0].Password))
            {
                return Task.FromResult<string>("Password Not Valid");
            }


            return Task.FromResult<string>(JWTtokenHandler.GenerateToken());
        }

        [Authorize]
        [HttpPut("ChangeEmail")]
        public Task<int> ChangeEmail(ChangeEmailObject email)
        {
            if (string.IsNullOrEmpty(email.Email) && string.IsNullOrEmpty(email.EmailVerify))
                return Task.FromResult<int>(500);

            if (email.Email != email.EmailVerify)
                return Task.FromResult<int>(500);


            using DatabaseHandler database = new DatabaseHandler();

            MySqlCommand changePasswordSql = new MySqlCommand();

            changePasswordSql.CommandText = "UPDATE Administrator SET Email=@Email WHERE Id=@Id;";
            changePasswordSql.Parameters.AddWithValue("@Email", email.Email);
            changePasswordSql.Parameters.AddWithValue("@Id", 1);

            if (database.Update(changePasswordSql).isSuccesfull)
                return Task.FromResult<int>(200);
            return Task.FromResult<int>(500);
        }

        [Authorize]
        [HttpPut("ChangePassword")]
        public Task<int> ChangePassword(ChangePasswordObject password)
        {
            if(string.IsNullOrEmpty(password.Password) || string.IsNullOrEmpty(password.PasswordVerify))
                return Task.FromResult<int>(503);

            if(password.Password != password.PasswordVerify)
                return Task.FromResult<int>(500);

            using DatabaseHandler database = new DatabaseHandler();

            MySqlCommand changePasswordSql = new MySqlCommand();

            changePasswordSql.CommandText = "UPDATE Administrator SET Password=@password WHERE Id=@Id;";
            changePasswordSql.Parameters.AddWithValue("@password", BCryptHandler.BcrypyBasicEncryption(password.Password));
            changePasswordSql.Parameters.AddWithValue("@Id", 1);

            if (database.Update(changePasswordSql).isSuccesfull)
                return Task.FromResult<int>(200);
            return Task.FromResult<int>(500);
        }

    }
}