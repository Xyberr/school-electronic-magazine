using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using school_electronic_magazine.Models;

namespace school_electronic_magazine.Infrastructure.Auth;

public interface ITokenValidationParametersBuilder
{
    TokenValidationParameters Build();
}