namespace Domain.ValueObjects
{
    public class CMaintenanceGestion : ElmntGestion
    {
        public long Id { get; set; }
        public string Nom { get; set; }
        public string Info { get; set; }
        public int NbMat { get; set; }
        public DateTime DateDebut { get; set; }
        public DateTime DateFin { get; set; }
        public DateTime? DateDerniereIntervention { get; set; }
        public DateTime? DateProchaineIntervention { get; set; }
        public long IdEntreprise { get; set; }
        public string NomEntreprise { get; set; }
        public bool Archive { get; set; } = false;
        public DateTime LastModif { get; set; }



        public CMaintenanceGestion() { }
    }
}
