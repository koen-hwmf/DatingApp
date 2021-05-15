using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace API.Controllers
{
  public class AccountController : BaseApiController
  {
    private readonly ITokenService _tokenService;
    private readonly IMapper _mapper;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly UserManager<AppUser> _userManager;

    public AccountController(UserManager<AppUser> userManager, 
        SignInManager<AppUser> signInManager, ITokenService tokenService, IMapper mapper)
    {
      _userManager = userManager;
      _signInManager = signInManager;
      _tokenService = tokenService;
      _mapper = mapper;
    }

    // REGISTER METHOD
    [HttpPost("register")]
    public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
    {
      // check if username is already taken
      if (await UserExists(registerDto.Username)) return BadRequest("Username is already taken");

      var user = _mapper.Map<AppUser>(registerDto);

      // using var hmac = new HMACSHA512();

      user.UserName = registerDto.Username.ToLower();
      // user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)); // GetBytes creates bytesArray which is expected by hmac.ComputeHash()
      // user.PasswordSalt = hmac.Key;

      //   _context.Users.Add(user); // only tracks data in Entity framework
      //   await _context.SaveChangesAsync(); // call database and save data to users table

      var result = await _userManager.CreateAsync(user, registerDto.Password);

      if (!result.Succeeded)  return BadRequest(result.Errors);

      var roleResult = await _userManager.AddToRoleAsync(user, "Member");

      if (!roleResult.Succeeded) return BadRequest(result.Errors);

      return new UserDto
      {
        Username = user.UserName,
        Token = await _tokenService.CreateToken(user),
        KnownAs = user.KnownAs,
        Gender = user.Gender
      };
    }

    // LOGIN METHOD
    [HttpPost("login")]
    public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
    {
      // get username from database
      var user = await _userManager.Users
          .Include(p => p.Photos)
          .SingleOrDefaultAsync(x => x.UserName == loginDto.Username.ToLower());

      if (user == null) return Unauthorized("Invalid username");

      var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
      if (!result.Succeeded) return Unauthorized();

      // get stored salted password and encrypt
      // using var hmac = new HMACSHA512(user.PasswordSalt);

      // salt and encrypt entered password 
      // var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));

      // compare stored and entered encrypted passwords and return invalid if not equal
      // for (int i = 0; i < computedHash.Length; i++) 
      // {
      //     if (computedHash[i] != user.PasswordHash[i]) return Unauthorized("Invalid password");
      // }

      return new UserDto
      {
        Username = user.UserName,
        Token = await _tokenService.CreateToken(user),
        PhotoUrl = user.Photos.FirstOrDefault(x => x.IsMain)?.Url,
        KnownAs = user.KnownAs,
        Gender = user.Gender
      };
    }

    private async Task<bool> UserExists(string username)
    {
      return await _userManager.Users.AnyAsync(x => x.UserName == username.ToLower());
    }
  }
}
