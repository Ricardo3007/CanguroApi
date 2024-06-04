using CanguroApi.Aplicattions.Contracts;
using CanguroApi.DTO;
using CanguroApi.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CanguroApi.Controllers
{
    //[Route("[controller]")]
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CanguroController : ControllerBase
    {

        private readonly ICanguroAppService _appService;
        private readonly IConfiguration _configuration;

        public CanguroController(ICanguroAppService appService, IConfiguration configuration) {
            _appService = appService;
            _configuration = configuration;
        }

        /// <summary>
        /// Consultar registro Canguro
        /// </summary>
        //[HttpGet]
        //[Route(nameof(CanguroController.GetCanguro))]
        [HttpGet("[action]")]
        //[Authorize]
        //[Authorize(Roles = "admin")]
        public Request<List<MovCanguroDTO>> GetCanguro()
        {
            Request<List<MovCanguroDTO>> aa = _appService.GetCanguro();
            
            return aa; 
        }

        /// <summary>
        /// Actualizar registro Canguro
        /// </summary>
        [HttpPut("[action]")]
        //[Route(nameof(CanguroController.UpdateCanguro))]
        public Request<MovCanguroDTO> UpdateCanguro(MovCanguroDTO movCanguroDTO)
        {
            return _appService.UpdateCanguro(movCanguroDTO);
        }

        /// <summary>
        /// Eliminar Registro Canguro
        /// </summary>
        [HttpDelete("[action]/{codigo}")]
        //[Route(nameof(CanguroController.DeleteCanguro))]
        public Request<string> DeleteCanguro(int codigo)
        {
            return _appService.DeleteCanguro(codigo);
        }

        /// <summary>
        /// agregar registro Canguro
        /// </summary>
        [HttpPost("[action]")]
        //[Route(nameof(CanguroController.UpdateCanguro))]
        public Request<MovCanguroDTO> AddCanguro(MovCanguroDTO movCanguroDTO)
        {
            return _appService.AddCanguro(movCanguroDTO);
        }



        [AllowAnonymous]
        [HttpPost("Login")]
        public IActionResult Login([FromBody] LoginModel loginModel)
        {
            Request<bool> existUsu = _appService.GetUsuario(loginModel.Nombre, loginModel.Password);
            if (existUsu.Result == true)
            {
                var token = GenerateJwtToken(loginModel.Nombre);
                //return Request<IActionResult>.Succes(Request < IActionResult >(token));
                return Ok(new { Token = token });
            }
            return Unauthorized();
        }

        private string GenerateJwtToken(string username)
        {
            var jwtSettings = _configuration.GetSection("Jwt").Get<JwtSettings>();
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.NameId, username)
            };

            var token = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = System.DateTime.Now.AddDays(1),
                SigningCredentials = creds
             };

            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenf = tokenHandler.CreateToken(token);

            return tokenHandler.WriteToken(tokenf);
        }


    }
}
