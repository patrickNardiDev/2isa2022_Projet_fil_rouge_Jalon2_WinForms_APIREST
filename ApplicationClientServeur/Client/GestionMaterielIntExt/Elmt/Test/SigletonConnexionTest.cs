using GestionMaterielIntExt.Controller;
using GestionMaterielIntExt.Model;

namespace GestionMaterielIntExt.Elmt.Test
{
    internal class SigletonConnexionTest
    {
        internal static bool TestInstance()
        {
            Console.WriteLine("Sigleton avec geter SingletonV2.Instance mais deux propriétés-------------------");
            SingletonConnexion myInstanceSigleton1 = SingletonConnexion.Instance;
            SingletonConnexion myInstanceSigleton2 = SingletonConnexion.Instance;

            Console.WriteLine($"myInstanceSigletonV21 : {myInstanceSigleton1.GetHashCode()}, myInstanceSigletonV22 : {myInstanceSigleton2.GetHashCode()}");
            return (myInstanceSigleton1.GetHashCode() == myInstanceSigleton2.GetHashCode()) ? true : false;
        }
    }


}
