using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exeption;

public class UnauthorizeException : Exception
{
    public UnauthorizeException() : base("La connexion a échouée") { }

}
