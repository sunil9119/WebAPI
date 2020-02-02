using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Schedular.API.Data;
using Schedular.API.DTO;
using Schedular.API.Models;
using AutoMapper;

namespace Schedular.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _repo;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;
        public AuthController(IAuthRepository repo, IConfiguration config, IMapper mapper)
        {
            _mapper = mapper;
            _config = config;
            _repo = repo;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterUserDTO objDTO)
        {
            objDTO.Username = objDTO.Username.ToLower();

            if (await _repo.UserExists(objDTO.Username))
            {
                return BadRequest("User already registered.");
            }
            var userToCreate = _mapper.Map<User>(objDTO);

            var createdUser = await _repo.Register(userToCreate, objDTO.Password);

            var userToReturn = _mapper.Map<UserForDetailDTO>(createdUser);

            return CreatedAtRoute("GetUser", new{controller="Users", id=createdUser.Id},userToReturn);
        }

        //JWT  structure - Header Algo Type, Payload identifier Info, uname and other decodable info exp valit until
        //Sectet used to hash the payload and Header, stored on the server

        // client Sends Username pwd to server, server validates and sends back JWT
        //client  Sends JWT for further requests. server validates and sends back the JWT

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginUserDTO objDTO)
        {
            //throw new Exception("say no");

            var userFromRepo = await _repo.Login(objDTO.Username.ToLower(), objDTO.Password);

            if (userFromRepo == null)
            {
                return Unauthorized();
            }

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userFromRepo.Id.ToString()),
                new Claim(ClaimTypes.Name, userFromRepo.Username.ToString())
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8
            .GetBytes(_config.GetSection("AppSettings:Token").Value));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDesc = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1), // Should be UTC.Now
                SigningCredentials = credentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDesc);
            var user = _mapper.Map<UserForListDTO>(userFromRepo);

            return Ok(new
            {
                token = tokenHandler.WriteToken(token),
                user
            });
        }

    }
}
