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
    public class RepositoryNationalSpecial : Repository<National>, IRepositoryNationalSpecial
    {
        public RepositoryNationalSpecial(ApplicationDBContext context) : base(context)
        {
        }
        public async Task<National> GetMin()
        {
            var minNational = await _context.nationals.OrderBy(n => n.code).FirstOrDefaultAsync();
            return minNational;
        }
        public async Task<int> GetNewCode()
        {
            try
            {
                // Use DefaultIfEmpty(0) to handle an empty table.
                int? maxCode = await _context.nationals
                    .Select(c => (int?)c.code)

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
        public async Task<National> GetMax()
        {
            var maxNational = await _context.nationals.OrderByDescending(n => n.code).FirstOrDefaultAsync();
            return maxNational;
        }

        public async Task<National> GetNextOrPreviousNationalByCode(int id, string direction)
        {
            // Get the current record's code.
            var currentCode = await _context.nationals
                .Where(n => n.ID == id)
                .Select(n => n.code)
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
                    var previousNational = await _context.nationals
                        .Where(n => n.code < currentCode)
                        .OrderByDescending(n => n.code)
                        .FirstOrDefaultAsync();
                    return previousNational;
                }
                else if (direction.ToLower() == "next")
                {
                    // Get the record with a code immediately greater than the current code.
                    var nextNational = await _context.nationals
                        .Where(n => n.code > currentCode)
                        .OrderBy(n => n.code)
                        .FirstOrDefaultAsync();
                    return nextNational;
                }
            }

            // If no direction was provided or direction is invalid, return null.
            return null;
        }

        public int GetMaxIdOfNational()
        {
            var id = _context.nationals.Max(i => i.ID);
            return id;
        }
    }
}
