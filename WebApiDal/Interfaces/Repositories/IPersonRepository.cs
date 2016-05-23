using System;
using System.Collections.Generic;
using Domain;

namespace Interfaces.Repositories
{
    public interface IPersonRepository : IBaseRepository<Person>
    {
        /// <summary>
        /// Get all Persons for this user id
        /// </summary>
        /// <param name="userId">user ID to filter by</param>
        /// <returns>List of persons, belonging to this user id</returns>
        List<Person> GetAllForUser(int userId);

        Person GetForUser(int personId, int userId);

        List<Person> GetAllForUser(int userId, string filter, string sortProperty, int pageNumber, int pageSize, out int totalUserCount, out string realSortProperty);
        List<Person> GetAllForUser(int userId, string filter, DateTime? filterFromDT, DateTime? filterToDt, string sortProperty, int pageNumber, int pageSize, out int totalUserCount, out string realSortProperty);
    }
}
