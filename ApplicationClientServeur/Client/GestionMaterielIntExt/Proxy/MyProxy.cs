using Domain.Exeption;
using GestionActifsMateriels.ObjTransf;

namespace GestionActifsMateriels.Proxy;

public class MyProxy
{
    public SysException MadeSysException(EProblemDetails problemDetails)
    {
        return new SysException($"Titre : {problemDetails.title}\r\nHttp Statut : {problemDetails.status}\r\n{problemDetails.type} \r\n\nDétails : {problemDetails.detail}");
    }
    
}
