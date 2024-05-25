using BLL.Interface;
using Domain.Entities;

namespace BLL.Implementation;

public class BCrypService : IBCrypService
{
    public bool Verify(string expected, string tobechecked)
    {
        return BCrypt.Net.BCrypt.Verify(expected, tobechecked);
    }
}
