using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using LearnerDictionary.Models;

namespace LearnerDictionary.Repository
{
    public class LDRepository : IUserStore<IUser>
    {
        public Task CreateAsync(IUser user)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(IUser user)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Task<IUser> FindByIdAsync(string userId)
        {
            if (userId == "1234")
            {
                return Task.FromResult((IUser)new LDUser()
                {
                    UserName = "1234"
                });
            }

            return null;
        }

        public Task<IUser> FindByNameAsync(string userName)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(IUser user)
        {
            throw new NotImplementedException();
        }
    }
}