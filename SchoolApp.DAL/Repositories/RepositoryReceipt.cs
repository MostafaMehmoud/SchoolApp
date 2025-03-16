﻿using System;
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
    public class RepositoryReceipt : Repository<Receipt>, IRepositorySpecial<Receipt>
    {
        public RepositoryReceipt(ApplicationDBContext context) : base(context)
        {
        }

        public async Task<Receipt> GetMax()
        {
            var maxStage = await _context.receipts.Include(i => i.installmentReceipts).OrderByDescending(n => n.Code).FirstOrDefaultAsync();
            return maxStage;
        }

        public int GetMaxIdOfItem()
        {
            var id = _context.receipts.Max(i => i.Id);
            return id;
        }

        public async Task<Receipt> GetMin()
        {
            var minSatge = await _context.receipts.Include(i => i.installmentReceipts).OrderBy(n => n.Code).FirstOrDefaultAsync();
            return minSatge;
        }

        public async Task<int> GetNewCode()
        {
            try
            {
                // Use DefaultIfEmpty(0) to handle an empty table.
                int? maxCode = await _context.receipts.Include(i => i.installmentReceipts)
                    .Select(c => (int?)c.Code)

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

        public async Task<Receipt> GetNextOrPreviousItemByCode(int id, string direction)
        {  // Get the current record's code.
            var currentCode = await _context.receipts
                .Where(n => n.Id == id)
                .Select(n => n.Code)
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
                    var previousStage = await _context.receipts.Include(i => i.installmentReceipts)
                        .Where(n => n.Code < currentCode)
                        .OrderByDescending(n => n.Code)
                        .FirstOrDefaultAsync();
                    return previousStage;
                }
                else if (direction.ToLower() == "next")
                {
                    // Get the record with a code immediately greater than the current code.
                    var nextStage = await _context.receipts.Include(i => i.installmentReceipts)
                        .Where(n => n.Code > currentCode)
                        .OrderBy(n => n.Code)
                        .FirstOrDefaultAsync();
                    return nextStage;
                }
            }

            // If no direction was provided or direction is invalid, return null.
            return null;
        }
    }
}
