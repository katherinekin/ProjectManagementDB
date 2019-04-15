using DataLibrary.Identities.IdentitiesModel;
using System;
using System.Linq;
using Microsoft.AspNet.Identity;

namespace DataLibrary.Identities
{
    public class UserRepository<T> where T : IdentityUser
    {
        private readonly DatabaseContext _databaseContext;

        public UserRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        internal T GeTByName(string userName)
        {
            var user = _databaseContext.LOGIN.SingleOrDefault(u => u.Username == userName);
            if (user != null)
            {
                T result = (T)Activator.CreateInstance(typeof(T));
                result.Employee_ID = user.LEmployee_ID;
                result.UserName = user.Username;
                result.Password = user.Password;
                result.PasswordHash = user.Password;
                result.Role = user.Role;
                result.Id = result.Employee_ID.ToString();
                return result;
            }
            return null; 
        }

        /// <summary>
        /// Returns an T given the user's id
        /// </summary>
        /// <param name="userId">The user's id</param>
        /// <returns></returns>
        public T GeTById(string userId)
        {
            var user = _databaseContext.LOGIN.SingleOrDefault(u => u.LEmployee_ID.ToString() == userId);
            if (user != null)
            {
                T result = (T)Activator.CreateInstance(typeof(T));
                result.Employee_ID = user.LEmployee_ID;
                result.UserName = user.Username;
                result.Password = user.Password;
                result.PasswordHash = user.Password;
                result.Role = user.Role;
                result.Id = result.Employee_ID.ToString();
                return result;
            }
            return null; 
        }

        /// <summary>
        /// Updates a user in the Users table
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public int Update(T user)
        {
            var result = _databaseContext.LOGIN.FirstOrDefault(u => u.LEmployee_ID == user.Employee_ID);
            if (result != null)
            {
                result.LEmployee_ID = user.Employee_ID;
                result.Username = user.UserName;
                result.Password = user.Password;
                result.Role = user.Role;
                return _databaseContext.SaveChanges();
            }
            return 0;
        }

        internal int Insert(T user)
        {
            _databaseContext.LOGIN.Add(new LOGIN
            {
                /*
                Id = user.Id,
                UserName = user.UserName,
                PasswordHash = user.PasswordHash,
                SecurityStamp = user.SecurityStamp,
                Email = user.Email,
                EmailConfirmed = user.EmailConfirmed,
                PhoneNumber = user.PhoneNumber,
                PhoneNumberConfirmed = user.PhoneNumberConfirmed,
                LockoutEnabled = user.LockoutEnabled,
                LockoutEndDateUtc = user.LockoutEndDateUtc,
                AccessFailedCount = user.AccessFailedCount
                */
            });

            return _databaseContext.SaveChanges();
        }

        public string GetPasswordHash(string userId)
        {
            var user = _databaseContext.LOGIN.FirstOrDefault(u => u.LEmployee_ID.ToString() == userId);
            var passHash = user != null ? user.Password : null;
            return passHash;
        }

        internal T GeTByEmail(string email)
        {
            var user = _databaseContext.LOGIN.SingleOrDefault(u => u.Username == email);
            if (user != null)
            {
                T result = (T)Activator.CreateInstance(typeof(T));
                result.Employee_ID = user.LEmployee_ID;
                result.UserName = user.Username;
                result.Password = user.Password;
                result.PasswordHash = user.Password;
                result.Role = user.Role;
                result.Id = result.Employee_ID.ToString();
                return result;
            }
            return null;
        }
    }
}