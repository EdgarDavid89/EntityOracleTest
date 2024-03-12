using System;

namespace Parkings.Secutiry
{
    public interface IAuthentication
    {
        string CreateToken(string user);
        string HasPassword(string password);
        bool VerifyPassword(string password, string passwordHash);
    }
}