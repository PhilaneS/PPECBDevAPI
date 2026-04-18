using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class ProdCodeGeneratorSevice : IProductCodeGenerator
    {
        private readonly AppDbContext _context;

        public ProdCodeGeneratorSevice(AppDbContext context)
        {
            _context = context;
        }
        public async Task<string> GenerateProductCodeAsync()
        {
           var monthKey = DateTime.UtcNow.ToString("yyyyMM");

            using var transaction = await _context.Database.BeginTransactionAsync();

            var sequence = await _context.ProductCodeSequences.FirstOrDefaultAsync(s => s.Id == monthKey);

            if (sequence == null)
            {
                sequence = new ProductCodeSequence
                {
                    Id = monthKey,
                    LastNumber = 1
                };
                _context.ProductCodeSequences.Add(sequence);
            }
            else
            {
                sequence.LastNumber++;
                _context.ProductCodeSequences.Update(sequence);
            }

            await _context.SaveChangesAsync();
            await transaction.CommitAsync();

            return $"{monthKey}-{sequence.LastNumber:D4}";
        }
    }
}
