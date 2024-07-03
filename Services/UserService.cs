using FormBackend.Model;
using FormBackend.Model.DTOS;
using FormBackend.Services.Context;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace FormBackend.Services{
    public class UserService : ControllerBase{
        private readonly DataContext _context;
        public UserService(DataContext context){_context = context;}
        public bool AddUser(CreateAccountDTO user){
            if(!DoesUserExist(user.Email)){
                PassDTO pass = HashPassword(user.Password);
                UserModel newUser = new(){
                    ID = user.ID,
                    Email = user.Email,
                    Salt = pass.Salt,
                    Hash = pass.Hash,
                    IsAdmin = user.IsAdmin,
                };
                _context.UserInfo.Add(newUser);
            }
            return _context.SaveChanges() != 0;
        }
        public bool DoesUserExist(string email){
            return _context.UserInfo.SingleOrDefault(u => u.Email == email) != null;
        }

        public UserDTO Converter(UserModel user){
            return new UserDTO{
                ID = user.ID,
                Email = user.Email,
                IsAdmin = user.IsAdmin 
            };
        }
        public bool DeleteUser(int id){
            UserModel user = _context.UserInfo.Find(id);
            if(user != null){
                _context.UserInfo.Remove(user);
            }
            return _context.SaveChanges() != 0;
        }

        public IEnumerable<UserDTO> GetUsers(){
            IEnumerable<UserModel> users = _context.UserInfo;
            return users.Select(user => Converter(user)).ToList();
        }
        public PassDTO HashPassword(string passowrd){
            PassDTO newHashPassword = new();
            byte[] SaltByte = new byte[64];
            RNGCryptoServiceProvider provider = new();
            provider.GetNonZeroBytes(SaltByte);
            string salt = Convert.ToBase64String(SaltByte);
            Rfc2898DeriveBytes rfc2898DeriveBytes = new(passowrd, SaltByte, 10000);
            string hash = Convert.ToBase64String(rfc2898DeriveBytes.GetBytes(256));
            newHashPassword.Salt = salt;
            newHashPassword.Hash = hash;
            return newHashPassword;
        }

        public UserModel GetUserByUsername(string email){
            return _context.UserInfo.SingleOrDefault(u => u.Email == email);
        }

        public bool VerifyUsersPassword(string passowrd, string storedHash, string storedSalt){
            byte[] SaltBytes = Convert.FromBase64String(storedSalt);
            Rfc2898DeriveBytes rfc2898DeriveBytes = new(passowrd, SaltBytes, 10000);
            string newHash = Convert.ToBase64String(rfc2898DeriveBytes.GetBytes(256));
            return newHash == storedHash;
        }


        // DO NOT TOUCH YOU IDIOT!!!!
        public IActionResult Login(LoginDTO user){
            Console.WriteLine("Ran Function");
            IActionResult Result = Unauthorized();
            if(DoesUserExist(user.Username)){
                Console.WriteLine("Ran User Check");
                UserModel userModel = GetUserByUsername(user.Username);
                if(VerifyUsersPassword(user.Password, userModel.Hash, userModel.Salt)){
                    Console.WriteLine("Ran Password Verif");
                    var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("supersecretkeythatisextended@345"));
                    var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256Signature);
                    var tokenOptions = new JwtSecurityToken(
                        issuer: "http://localhost:5000",
                        audience: "http://localhost:5000",
                        claims: new List<Claim>(),
                        expires: DateTime.Now.AddMinutes(30),
                        signingCredentials: signingCredentials
                    );
                    Console.WriteLine("Ran Token Creation");
                    var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
                    Result = Ok(new {token = tokenString});
                } 
            }
            return Result;
        }
        //I TOLD YOU NOT TO TOUCH IT!

        public bool ResetPassword(ResetPassDTO NewPass){
            UserModel foundUser = GetUserByUsername(NewPass.Email);
            if(foundUser != null){ 
                var UpdatedPass = HashPassword(NewPass.NewPassword);
                if(foundUser.Salt == UpdatedPass.Salt || foundUser.Hash == UpdatedPass.Hash){
                    return false;
                }
                foundUser.Salt = UpdatedPass.Salt;
                foundUser.Hash = UpdatedPass.Hash;
                return true;
            }else return false;
        }
        public bool EditUser(UpdateUserDTO user){
            UserModel foundUser = GetUserByUsername(user.Username);
            if(foundUser != null){
                foundUser.ID = foundUser.ID;
                foundUser.Email = user.Username;
                foundUser.IsAdmin = user.IsAdmin;
                foundUser.Address = user.Address ;
                foundUser.PhoneNumber = user.PhoneNumber;
                _context.Update(foundUser);
            }
            return _context.SaveChanges() != 0;
        }
    }
}
