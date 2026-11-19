using Application.Dtos.User;
using Application.Interfaces.Reposatoriese;
using Domain.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Reposatories
{
    public class UserRepo : IUserRepo
    {
        private readonly E_COmmerceDbContext db;

        public UserRepo(E_COmmerceDbContext db)
        {
            this.db = db;
        }
        public async Task<List<ApplicationUser>> GetAll()
        {
            return await db.Users.AsNoTracking().ToListAsync();
        }
        public async Task<bool> DeleteAccount(string Id)
        {
            var user = db.Users.Where(u => u.Id == Id);
            if (user != null)
            {
                db.Users.RemoveRange(user);
                return true;
            }
            return false;
        }


        public async Task<ApplicationUser> GetUserById(string Id)
        {
            ApplicationUser? user =  await db.Users.FirstOrDefaultAsync(u => u.Id == Id);
            if(user != null) 
               return user;
            return null!;
        }

        public async Task Save()
        {
           await  db.SaveChangesAsync();
        }
    }
}
