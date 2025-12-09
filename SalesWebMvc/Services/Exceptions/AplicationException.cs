namespace SalesWebMvc.Services.Exceptions
{
    public class AplicationException
    {
        private string message;

        public AplicationException(string message)
        {
            this.message = message;
        }
    }
}