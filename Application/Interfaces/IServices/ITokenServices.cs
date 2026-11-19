namespace Application.Interfaces.IServices
{
    public interface ITokenServices
    {
        public Task<(string token, DateTime expireAt)> GenerateAccessToken(ApplicationUser User);
        public string GenerateRefreshToken();


    }
}
