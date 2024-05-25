using Domain.DTO.Connexion;
using GestionMaterielIntExt.ObjTransf.Connexion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionMaterielIntExt.Proxy.Connexion.Interface
{
    public interface IProxiConnexion
    {

        internal bool IsConnected { get; }

        //public Task<ConnexionResponseDto> ConnexionUserAsync(ConnexionRequestDto requestDto);
        public Task<trConnexionResponse> ConnexionUserAsync(TrConnexionRequest trConnexionRequest);
        public bool DeConnexionUserAsync();

        public HttpClient GetClient();


    }
}
