using Domain.Entities;

namespace GestionMaterielIntExt.Elmt.Factory
{
    internal class FactoMateriel : Materiel
    {
        private static DateTime _DateMiseEnService;

        public enum categorie
        {
            laptop = 1,
            écranPC = 2,
            chaiseBureau = 3,
            uniteCentrale = 4,
            souris = 6,
            clavier = 7,
            CPU = 8,
            GPU = 9
        }

        //internal static Materiel MackFactM()
        //{
        //    FactoMateriel factoM = new();
        //    return new Materiel(factoM.GetNom(), factoM.GetDateMiseEnService(), factoM.GetDateFinGarantie(), null, null, false);
        //    //public Materiel(long? id, string nom, DateTime? dateMiseEnService, DateTime? dateFinGarantie, long? idUser, long? idMContrat, bool archive)

        //}

        internal DateTime GetDateFinGarantie()
        {
            return _DateMiseEnService.AddYears(2);
        }

        internal DateTime GetDateMiseEnService()
        {
            _DateMiseEnService = DateTimeRandom();
            return _DateMiseEnService;

        }

        internal int? GetIdMContrat()
        {
            var list = new List<int> { 1, 2, 3, 4, 5 };
            return RandomValue(list);
        }

        internal int GetIdUser()
        {
            var list = new List<int> { 82001, 96103, 82005, 96107, 96111, 96117, 96118 };
            return RandomValue(list);
        }

        internal string GetNom()
        {
            var list = new List<string> { "Martin", "Petit", " Schmitt ", "Bommet", "Faure", "Blanc", "Barre" };
            return RandomValue(list);
        }

        internal static T RandomValue<T>(List<T> t)
        {
            var random = new Random();
            var list = t;
            int index = random.Next(t.Count);
            return list[index];
        }

        internal static DateTime DateTimeRandom()
        {
            var year = new Random().Next(2020, 2023);
            var month = new Random().Next(1, 12);
            var day = new Random().Next(1, 28);
            return new DateTime(year, month, day);
        }

        int GetId()
        {
            throw new NotImplementedException();
        }
    }
}
