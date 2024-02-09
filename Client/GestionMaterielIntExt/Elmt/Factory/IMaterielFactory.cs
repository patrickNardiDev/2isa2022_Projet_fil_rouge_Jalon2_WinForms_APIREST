using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionMaterielIntExt.Elmt.Factory
{
    public interface IMaterielFactory
    {
        public int GetId();
        public string GetNom();
        public DateTime GetDateMiseEnService();
        public DateTime GetDateFinGarantie();
        public int GetIdUser();
        public int? GetIdMContrat();
    }
}
