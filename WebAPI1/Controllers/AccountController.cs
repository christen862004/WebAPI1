using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

//using Microsoft.IdentityModel.JsonWebTokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebAPI1.DTO;
using WebAPI1.Models;

namespace WebAPI1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IConfiguration config;

        public AccountController(UserManager<ApplicationUser> userManager,IConfiguration config)
        {
            this.userManager = userManager;
            this.config = config;
        }

        [HttpPost("register")]//api/Account/register [username ,password,email]
        public async Task<IActionResult> Register(RegisterUserDto userFromRequest)//
        {
            if (ModelState.IsValid)
            {
                ApplicationUser userModel = new ApplicationUser();
                userModel.UserName = userFromRequest.UserName;
                userModel.Email = userFromRequest.Email;
                IdentityResult result=await userManager.CreateAsync(userModel, userFromRequest.Password);        
                if (result.Succeeded)
                {
                    return Ok("Account Created Success");
                }
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
            }
            return BadRequest(ModelState);
        }

        [HttpPost("login")]//api/account/login?username=Ahmed&Passwor=Ahem123*
        public async Task<IActionResult> Login(LoginUserDto userFromRequest)
        {
            if (ModelState.IsValid)
            {
                //check Account Valid
                ApplicationUser userModel=
                    await  userManager.FindByNameAsync(userFromRequest.UserName);
                if(userModel != null)
                {
                    bool found=await userManager.CheckPasswordAsync(userModel,userFromRequest.Password);
                    if (found == true)
                    {
                        #region Create Token

                        
                        List<Claim> claims = new List<Claim>();

                        claims.Add(new Claim(ClaimTypes.NameIdentifier, userModel.Id));
                        claims.Add(new Claim(ClaimTypes.Name, userModel.UserName));
                        claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));

                        var Roles = await userManager.GetRolesAsync(userModel);
                        if (Roles != null)
                        {
                            foreach (var RoleName in Roles)
                            {
                                claims.Add(new Claim(ClaimTypes.Role, RoleName));
                            }
                        }
                        //-------------------------------------
                        string key = config["JWT:Key"];
                        var Secritkey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));

                        SigningCredentials signingCredentials =
                            new SigningCredentials(Secritkey, SecurityAlgorithms.HmacSha256);


                        JwtSecurityToken mytoken = new JwtSecurityToken(
                            issuer: config["JWT:Issuer"],  //Provider IP Current ip to this project
                            audience: config["JWT:Audience"], //consumer IP Anagler
                            expires: DateTime.Now.AddHours(1),
                            claims: claims,                     //payload
                            signingCredentials: signingCredentials //Signiture

                            );

                        return Ok(new
                        {
                            token=new JwtSecurityTokenHandler().WriteToken(mytoken),
                            expires= DateTime.Now.AddHours(1)
                        });

                        #endregion
                    }
                }
                return Unauthorized();
            }
            return BadRequest(ModelState);
        }
    }
}
