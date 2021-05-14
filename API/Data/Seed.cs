using API.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace API.Data
{
    public class Seed
    {
        public static async Task SeedUsers(UserManager<AppUser> userManager)
        {
            // Check if we already have users in database and return if true
            if (await userManager.Users.AnyAsync()) return;
            
            // Get userdata from json file
            var userData = await System.IO.File.ReadAllTextAsync("Data/UserSeedData.json");

            // Create user objects from userData
            var users = JsonSerializer.Deserialize<List<AppUser>>(userData);

            foreach (var user in users)
            {
                // using var hmac = new HMACSHA512();

                user.UserName = user.UserName.ToLower(); // set username
                // user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("Pa$$w0rd")); // create passwordhash
                // user.PasswordSalt = hmac.Key; // create passwordsalt

                // add tracking to user through Entity framework
                await userManager.CreateAsync(user, "Pa$$w0rd");
            }
        } 
    }
}
