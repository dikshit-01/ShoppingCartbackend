using Buisness_Logic_Layer.Services;
using Data_Access_Layer.Models;
using Data_Access_Layer.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Shopping_cart_Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly IGenericRepository<User> _genericRepository;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserServices _createUser;

        private readonly IConfiguration _config;
        private readonly ITokenService _tokenService;

        private string _generatedToken = null;


        public HomeController(IGenericRepository<User> genericRepository, UserManager<User> userManager,
            SignInManager<User> signInManager,RoleManager<IdentityRole> roleManager, UserServices createUser,
            IConfiguration config, ITokenService tokenService)
        {
            _genericRepository = genericRepository;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _createUser = createUser;
            _config = config;
            _tokenService = tokenService;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> Index()
        {
            var list = await _genericRepository.GetAll();
            return Ok(await _genericRepository.GetAll());
        }

        [HttpPost]
        [Route("Register")]
        public async Task<ActionResult> Register(UserModel userModel)
        {
            var IsAlreadyExists = await _userManager.FindByEmailAsync(userModel.Email);
            if (IsAlreadyExists != null) { return Conflict(); }
            var result = await _createUser.RegisterUser(userModel);
            if (result.Succeeded)
            {
                return Ok(userModel);
            }
            return BadRequest(result);
        }

        [HttpPost]
        [Route("Login")]
        public async Task<ActionResult> Login(UserLoginDto user)
        {
            if (string.IsNullOrEmpty(user.UserName) || string.IsNullOrEmpty(user.Password))
            {
                return (RedirectToAction("Error"));
            }

            var validUser = await _userManager.FindByEmailAsync(user.UserName);
            if (validUser == null) return NotFound();
            if (await _userManager.CheckPasswordAsync(validUser,user.Password))
            {
                
                _generatedToken = await _tokenService.BuildToken(_config["Jwt:Key"].ToString(), _config["Jwt:Issuer"].ToString(), validUser);
                if (_generatedToken != null)
                {
                    HttpContext.Session.SetString("Token", _generatedToken);
                    Token tk = new Token();
                    tk.token= _generatedToken;
                    string json = JsonConvert.SerializeObject(tk);
                    return Ok(json);
                }
                else
                {
                    return BadRequest();
                }
            }
            else
            {
                return NotFound();
            }
        }
    }
}
