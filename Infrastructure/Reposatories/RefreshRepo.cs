using Application.Interfaces.Reposatoriese;
using Domain.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Reposatories
{
    public class RefreshRepo : IRefreshTokenRepo
    {
        private readonly E_COmmerceDbContext db;

        public RefreshRepo(E_COmmerceDbContext db)
        {
            this.db = db;
        }
        public async Task<RefreshToken> AddRefreshToken(RefreshToken refreshToken)
        {
            try
            {
                var refreshtoken = await db.RefreshTokens.AddAsync(refreshToken);
                //db.SaveChanges();
                return refreshToken;
            }
            catch
            {
                return null!;
            }
        }

        public async Task<bool> DeleteRefreshToken(RefreshToken refreshToken)
        {
            try
            {
                db.RefreshTokens.Remove(refreshToken);
                return true;

            }
            catch
            {
                return false;
            }
        }

        public async Task<RefreshToken> GetRefreshToken(string token, string userId)
        {
            //throw new NotImplementedException();
            try
            {

                RefreshToken? refreshtoken = await db.RefreshTokens.Where(r => r.UserId == userId && r.Refreshtoken == token && r.IsActive == true).FirstOrDefaultAsync();

                return refreshtoken;
            }
            catch
            {
                return null!;
            }
        }

        public async Task<bool> UpdateRefreshToken(RefreshToken refreshToken)
        {
            try
            {
                var update = db.RefreshTokens.Update(refreshToken);
                return true;
            }catch
            {
                return false;
            }
           
        }
        public async Task SaveChange()
        {
            db.SaveChanges();
        }
    }
}
