using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interfaces;
using Domain;

namespace DAL.Repositories
{
    public class PersonRepository : EFRepository<Person>, IPersonRepository
    {
        public PersonRepository(IDbContext dbContext) : base(dbContext)
        {
        }

        public List<Person> GetAllForUser(int userId)
        {
            return DbSet.Where(p => p.UserId == userId).OrderBy(o => o.Lastname).ThenBy(o => o.Firstname).Include(c => c.Contacts).ToList();
        }

        public Person GetForUser(int personId, int userId)
        {
            return DbSet.FirstOrDefault(a => a.PersonId == personId && a.UserId == userId);
        }

        public List<Person> GetAllForUser(int userId, string filter, string sortProperty, int pageNumber, int pageSize, out int totalUserCount, out string realSortProperty)
        {
            return GetAllForUser( userId,  filter,  null,  null, sortProperty, pageNumber,  pageSize, out  totalUserCount, out realSortProperty);
        }

        public List<Person> GetAllForUser(int userId, string filter, DateTime? filterFromDT, DateTime? filterToDt, string sortProperty,
            int pageNumber, int pageSize, out int totalUserCount, out string realSortProperty)
        {
            sortProperty = sortProperty?.ToLower() ?? "";
            realSortProperty = sortProperty;
            filter = filter?.ToLower() ?? "";


            //start building up the query
            var res = DbSet
                .Where(p => p.UserId == userId);

            // filter records
            if (!string.IsNullOrWhiteSpace(filter))
            {
                res = res.Where(p => p.Firstname.ToLower().Contains(filter) || p.Lastname.ToLower().Contains(filter));
            }

            if (filterFromDT != null && filterToDt != null)
            {
                res = res.Where(p => p.DateTime2 >= filterFromDT.Value && p.DateTime2 <= filterToDt.Value);
            }

            // set up sorting
            switch (sortProperty)
            {
                case "_firstname":
                    res = res
                        .OrderByDescending(o => o.Firstname).ThenBy(o => o.Lastname);
                    break;
                case "firstname":
                    res = res
                        .OrderBy(o => o.Firstname).ThenBy(o => o.Lastname);
                    break;

                case "_lastname":
                    res = res
                        .OrderByDescending(o => o.Lastname).ThenBy(o => o.Firstname);
                    break;

                default:
                case "lastname":
                    res = res
                        .OrderBy(o => o.Lastname).ThenBy(o => o.Firstname);
                    realSortProperty = "lastname";
                    break;
            }

            // join entities to avoid 1+n
            res = res
                .Include(c => c.Contacts);

            // get the count before any skip and take - this will generate sql like: SELECT COUNT(*) FROM Person WHERE UserId=xxx
            totalUserCount = res.Count();

            // skip x first records, then take y records - this will generate full sql, with joins
            var reslist = res
                .Skip(pageNumber * pageSize).Take(pageSize)
                .ToList();

            return reslist;
        }
    }
}
