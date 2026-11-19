namespace Application.Interfaces.Reposatoriese
{
    public interface IRefreshTokenRepo
    {
        public Task<RefreshToken> GetRefreshToken(string token, string userId);
        public Task<RefreshToken> AddRefreshToken(RefreshToken refreshToken);
        public Task<bool> UpdateRefreshToken(RefreshToken refreshToken);
        public Task<bool> DeleteRefreshToken(RefreshToken refreshToken);
        public Task SaveChange();
    }
}
