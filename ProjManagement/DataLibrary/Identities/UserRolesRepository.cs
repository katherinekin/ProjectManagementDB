using DataLibrary.Identities.IdentitiesModel;
using System.Collections.Generic;
using System.Linq;

namespace DataLibrary.Identities
{
    internal class UserRolesRepository
    {
        private readonly DatabaseContext _databaseContext;

        public UserRolesRepository(DatabaseContext database)
        {
            _databaseContext = database;
        }

        /// <summary>
        /// Returns a list of user's roles
        /// </summary>
        /// <param name="userId">The user's id</param>
        /// <returns></returns>
        public IList<string> FindByUserId(string userId)
        {
            var roles = _databaseContext.LOGIN.
                Where(u => u.LEmployee_ID.ToString() == userId);
            return roles.Select(r => r.Role).ToList();
        }
    }
}