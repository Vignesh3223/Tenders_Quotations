using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Tenders_Quotations.Models;

namespace Tenders_Quotations.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : Controller
    {
        private readonly TenderQuotationsContext _context;

        public LoginController(TenderQuotationsContext context)
        {
            _context = context;
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult EmployeeLogin([FromBody] LoginModel login)
        {
            string storedProcedureQuery = "EXEC dbo.Validate_User @Email, @Password";
            var loguser = _context.Currentusers.FromSqlRaw(storedProcedureQuery, new SqlParameter("Email", login.Email),
                new SqlParameter("Password", login.Password)).ToList();
            var user = _context.Users.FirstOrDefault(x => x.UserId == loguser[0].UserId);
            switch (loguser[0].UserId)
            {
                case -1:
                    var response = new Response
                    {
                        Status = "Error",
                        Message = "Invalid User",
                    };
                    return BadRequest(response);

                default:
                    GetToken(loguser);
                    var response1 = new Response
                    {
                        Status = "Success",
                        Message = "Login Successfully",
                        Userid = user.UserId,
                        Companyname = user.CompanyName,
                        Proprieator = user.Proprieator,
                        Email = user.Email,
                        Address = user.Address,
                        ContactNumber = user.ContactNumber,
                        Companysector = user.CompanySector,
                        EstablishedDate = user.EstablishedDate,
                        Gstin = user.Gstin,
                        Crn = user.Crn,
                        RoleId = user.RoleId,
                        Token = user.Token,
                        ProfilePic = user.ProfilePic
                    };
                    return Ok(response1);
            }
        }

        [HttpPost("gettoken")]
        public IActionResult GetToken(List<Currentuser> login)
        {
            var user = _context.Users.FirstOrDefault(x => x.UserId == login[0].UserId);

            if (user == null)
            {
                return BadRequest("No user found");
            }

            int? roleid = user.RoleId;

            var userRole = "no role";

            if (roleid == 1)
            {
                userRole = "Admin";
            }
            else if (roleid == 2)
            {
                userRole = "Users";
            }
            else
            {
                return BadRequest("User does not have a role.");
            }

            var key = "Yh2k7QSu4l8CZg5p6X3Pna9L0Miy4D3Bvt0JVr87UcOj69Kqw5R2Nmf4FWs03Hdx";

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.CompanyName),
                new Claim(ClaimTypes.Role, userRole),
            };

            var creds = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)), SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: "JWTAuthenticationServer",
                audience: "JWTServicePostmanClient",
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds
            );

            var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);
            user.Token=jwtToken;
            _context.Entry(user).State = EntityState.Modified;  
            _context.SaveChanges();
            return Ok(jwtToken);
        }
    }
}
