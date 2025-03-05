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
    public class RepositoryStudent : Repository<Student>, IRepositorySpecial<Student>
    {
        public RepositoryStudent(ApplicationDBContext context) : base(context)
        {
        }

        public async Task<Student> GetMax()
        {
            var minStudent = await _context.students.
                Include(i => i.installmentCostBeforeDiscounts).
                Include(i => i.installmentCostAfterDiscounts)
                .OrderByDescending(n => n.StudentNumber)
                .FirstOrDefaultAsync();
            return minStudent;
        }

        public int GetMaxIdOfItem()
        {
            
            var id = _context.students.Max(i => i.Id);
            return id;
        }

        public async Task<Student> GetMin()
        {
            var minSatge = await _context.students.
                Include(i => i.installmentCostBeforeDiscounts).
                Include(i=>i.installmentCostAfterDiscounts).
                OrderBy(n => n.StudentNumber)
                .FirstOrDefaultAsync();
            return minSatge;
        }

        public async Task<int> GetNewCode()
        {
            try
            {
                // Use DefaultIfEmpty(0) to handle an empty table.
                int? maxCode = await _context.students
                    .Select(c => (int?)c.StudentNumber)

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

        public async Task<Student> GetNextOrPreviousItemByCode(int id, string direction)
        {
            // Get the current record's code.
            var currentCode = await _context.students
                .Where(n => n.Id == id)
                .Select(n => n.StudentNumber)
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
                    var previousStage = await _context.students.
                        Include(i => i.installmentCostBeforeDiscounts).
                        Include(i => i.installmentCostAfterDiscounts)
                        .Where(n => n.StudentNumber < currentCode)
                        .OrderByDescending(n => n.StudentNumber)
                        .FirstOrDefaultAsync();
                    return previousStage;
                }
                else if (direction.ToLower() == "next")
                {
                    // Get the record with a code immediately greater than the current code.
                    var nextStage = await _context.students.
                        Include(i => i.installmentCostBeforeDiscounts).
                        Include(i => i.installmentCostAfterDiscounts)
                        .Where(n => n.StudentNumber > currentCode)
                        .OrderBy(n => n.StudentNumber)
                        .FirstOrDefaultAsync();
                    return nextStage;
                }
            }

            // If no direction was provided or direction is invalid, return null.
            return null;
        }
    }
}
