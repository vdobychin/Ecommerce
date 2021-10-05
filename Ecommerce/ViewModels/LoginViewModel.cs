using Ecommerce.Models;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.ViewModels
{
    public class LoginViewModel
    {
        public Login login { get; set; }

        [Required]
        public string Password { get; set; }
        [Required]
        public string ReturnUrl { get; set; }

        public bool IsPasswordValid(string password, string salt, string hash)
        {
            byte[] saltBytes = Convert.FromBase64String(salt);
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: password,
            salt: saltBytes,
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: 100000,
            numBytesRequested: 256 / 8));
            return hashed.SequenceEqual(hash);
        }
    }
}
