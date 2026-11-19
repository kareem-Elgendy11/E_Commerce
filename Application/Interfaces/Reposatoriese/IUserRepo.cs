using Application.Dtos.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces.Reposatoriese
{
    public interface IUserRepo
    {
        public Task<List<ApplicationUser>> GetAll();
        public Task<bool> DeleteAccount(string id);
        public Task <ApplicationUser> GetUserById(string Id);
        public Task Save();


    }
}
