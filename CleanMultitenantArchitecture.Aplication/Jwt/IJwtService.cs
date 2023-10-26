using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CleanMultitenantArchitecture.Aplication.Jwt
{
    public interface IJwtService
    {
        string GenerateToken(IEnumerable<Claim> claims);
    }
}
