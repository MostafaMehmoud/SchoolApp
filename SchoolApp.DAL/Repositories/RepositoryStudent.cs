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
                // 🔹 1️⃣ استخراج السنتين الأخيرتين من السنة الحالية
                string yearPrefix = DateTime.Now.Year.ToString().Substring(2, 2); // "25" للسنة 2025
                int yearNumber = int.Parse(yearPrefix) * 100000; // 25 → 2500000

                // 🔹 2️⃣ إيجاد أكبر رقم طالب للسنة الحالية
                int? maxStudentNumber = await _context.students
                    .Where(s => s.StudentNumber >= yearNumber && s.StudentNumber < (yearNumber + 100000)) // طلاب نفس السنة
                    .Select(s => (int?)s.StudentNumber)
                    .MaxAsync();

                // 🔹 3️⃣ تحديد الرقم الجديد
                int newStudentNumber = (maxStudentNumber ?? yearNumber) + 1;

                return newStudentNumber;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in Repository.GetNewCode: " + ex.ToString());
                throw;
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
