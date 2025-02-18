using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SchoolApp.DAL.Models;
using SchoolApp.DAL.Repositories.IRepository;
using SchoolApp.DAL.Repository;

namespace SchoolApp.DAL.Repositories
{
    public class RepositoryFileBusSpecial : Repository<FileBus>, IRepositorySpecial<FileBus> 
    {
        private readonly DbSet<FileBus> _dbSet;
        public RepositoryFileBusSpecial(ApplicationDBContext context)
            : base(context)
        {
            _dbSet = context.Set<FileBus>();
        }
        public async Task<int> GetNewCode()
        {
            try
            {
                // Use DefaultIfEmpty(0) to handle an empty table.
                int? maxCode = await _context.FileBuses
                    .Select(c => (int?)c.BusCode)

                    .MaxAsync();


                return (maxCode ?? 0) + 1;
            }
            catch (Exception ex)
            {
                // Log the full exception details for debugging.
                Console.WriteLine("Exception in Repository.GetNewCode: " + ex.ToString());
                throw;  // Rethrow to let the upper layers catch it.
            }
        }
        public async Task<FileBus> GetMax()
        {
            var maxNational = await _dbSet.OrderByDescending(n => n.BusCode).FirstOrDefaultAsync();
            return maxNational;
        }

        public int GetMaxIdOfItem()
        {
            var id = _dbSet.Max(i => i.BusId);
            return id;
        }

        public async Task<FileBus> GetMin()
        {
            var minNational = await _dbSet.OrderBy(n => n.BusCode).FirstOrDefaultAsync();
            return minNational;
        }

        public async Task<FileBus> GetNextOrPreviousItemByCode(int id, string direction)
        {
            // Get the current record's code.
            var currentCode = await _dbSet
                .Where(n => n.BusId == id)
                .Select(n => n.BusCode)
                .FirstOrDefaultAsync();

            // If currentCode is 0, you may interpret it as "not found" or invalid.
            if (currentCode == 0)
            {
                return null;
            }

            if (!string.IsNullOrEmpty(direction))
            {
                if (direction.ToLower() == "previous")
                {
                    // Get the record with a code immediately less than the current code.
                    var previousNational = await _dbSet
                        .Where(n => n.BusCode < currentCode)
                        .OrderByDescending(n => n.BusCode)
                        .FirstOrDefaultAsync();
                    return previousNational;
                }
                else if (direction.ToLower() == "next")
                {
                    // Get the record with a code immediately greater than the current code.
                    var nextNational = await _dbSet
                        .Where(n => n.BusCode > currentCode)
                        .OrderBy(n => n.BusCode)
                        .FirstOrDefaultAsync();
                    return nextNational;
                }
            }

            // If no direction was provided or direction is invalid, return null.
            return null;
        }
    }
}

