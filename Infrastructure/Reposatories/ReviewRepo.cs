using Application.Interfaces.Reposatoriese;
using Domain.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Reposatories
{
    public class ReviewRepo : IReviewRepo
    {
        private readonly E_COmmerceDbContext db;

        public ReviewRepo(E_COmmerceDbContext db)
        {
            this.db = db;
        }
        public async Task CreateReview(Reviews review)
        {
            await db.Reviews.AddAsync(review);
        }
        public async Task<Reviews> GetReviewById(string reviewId)
        {
            return await db.Reviews.Include(r=>r.User).FirstOrDefaultAsync(r => r.Id == reviewId);
        }

        public async Task<bool> DeleteReview(Reviews review)
        {
            db.Reviews.Remove(review);
            await db.SaveChangesAsync();
            return true;
        }

        public async Task<List<Reviews>> GetAllReviewByUser(string UserId)
        {
            return await db.Reviews.Include(r=>r.User).Where(r => r.UserId == UserId).ToListAsync();
        }
        public async Task<List<Reviews>> GetAllReview(string productId)
        {
            return await db.Reviews.Include(r=>r.User).Where(r => r.ProductId == productId).ToListAsync();
        }

        public async Task Save()
        {
            await db.SaveChangesAsync();
        }

        public async Task<bool> UpdateReview(Reviews review)
        {
            db.Reviews.Update(review);
            await db.SaveChangesAsync();
            return true;
        }
        
    }
}
