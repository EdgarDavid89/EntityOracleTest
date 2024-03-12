using System;

namespace Parkings.Secutiry
{
    public interface IAuthentication
    {
        string CreateToken(string user);
    }
}