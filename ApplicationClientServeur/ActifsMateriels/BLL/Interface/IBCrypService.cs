using Domain.Entities;

namespace BLL.Interface;

public interface IBCrypService
{
    public bool Verify(string expected, string tobechecked);
}
