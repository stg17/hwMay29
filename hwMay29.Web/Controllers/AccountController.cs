using hwMay29.Data;
using hwMay29.Web.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace hwMay29.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly string _connectionString;
        public AccountController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ConStr");
        }

        [HttpPost("signup")]
        public void Signup(SignupViewModel signupViewModel)
        {
            var repo = new UserRepository(_connectionString);
            repo.AddUser(signupViewModel, signupViewModel.Password);
        }

        [HttpPost("login")]
        public User Login(LoginViewModel loginViewModel)
        {
            var repo = new UserRepository(_connectionString);
            var user = repo.Login(loginViewModel.Email, loginViewModel.Password);
            if (user == null)
            {
                return null;
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, loginViewModel.Email)
            };

            HttpContext.SignInAsync(new ClaimsPrincipal(
                new ClaimsIdentity(claims, "Cookies", ClaimTypes.Email, "role"))).Wait();

            return user;
        }

        [HttpPost("logout")]
        public void Logout()
        {
            HttpContext.SignOutAsync().Wait();
        }

        [HttpGet("getuser")]
        public User GetCurrentUser()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return null;
            }

            var repo = new UserRepository(_connectionString);
            return repo.GetByEmail(User.Identity.Name);
        }
    }
}
