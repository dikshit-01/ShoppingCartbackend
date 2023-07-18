using Data_Access_Layer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Repository
{
    public interface ITokenService
    {
        Task<string> BuildToken(string key, string issuer, User user);
        bool IsTokenValid(string key, string issuer, string token);
    }
}
