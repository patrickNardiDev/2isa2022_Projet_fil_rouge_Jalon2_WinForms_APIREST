namespace GestionMaterielIntExt.ObjTransf.Consultation;

public class TrConsultationResquest
{
    public long Categorie { get; set; }
    public TrConsultationResquest() { }
    public TrConsultationResquest(long categorie) => Categorie = categorie;
}
