namespace Domain.Exeption
{
    public class SysException : Exception
    {
        public string Message { get; private set; }
        // constructeur d'exeption internet affichées sur la vue connextion 
        public SysException(string message)
        {
            Message = message;
        }
    }
}
