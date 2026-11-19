namespace Application.Interfaces.IServices
{
    public interface IsendEmail
    {
        public Task<bool> sendemail(string to, string subject, string body);

    }
}
