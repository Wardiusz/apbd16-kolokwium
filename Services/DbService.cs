using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Kolokwium_APBD.Data;
using Kolokwium_APBD.Exceptions;
using Kolokwium_APBD.DTOs;
using Kolokwium_APBD.Models;

namespace Kolokwium_APBD.Services;

public class DbService : IDbService {
    private readonly DatabaseContext _context;

    public DbService(DatabaseContext context) {
        _context = context;
    }

    public async Task<NurseryBatchesDto> GetNurseryBatchesAsync(int nurseryId) {
        await using var transaction = await _context.Database.BeginTransactionAsync();
        
        try {
            var nursery = await _context.Nurseries
                .Include(a => a.SeedlingBatches)
                .ThenInclude(b => b.Responsibles)
                .ThenInclude(c => c.Employee)
                .Include(d => d.SeedlingBatches)
                .ThenInclude(e => e.TreeSpecies)
                .FirstOrDefaultAsync(f => f.NurseryId == nurseryId);
            
            if (nursery == null)
                throw new NotFoundException("Error");

            var dto = new NurseryBatchesDto {
                NurseryId = nursery.NurseryId,
                Name = nursery.Name,
                EstablishedDate = nursery.EstablishedDate,
                Batches = nursery.SeedlingBatches.Select(a => new BatchDto {
                    BatchId = a.BatchId,
                    Quantity = a.Quantity,
                    SownDate = a.SownDate,
                    ReadyDate = a.ReadyDate,
                    Species = new SpeciesDto {
                        LatinName = a.TreeSpecies.LatinName,
                        GrowthTimeInYears = a.TreeSpecies.GrowthTimeInYears
                    },
                    Responsible = a.Responsibles.Select(b => new ResponsibleDto {
                        FirstName = b.Employee.FirstName,
                        LastName = b.Employee.LastName,
                        Role = b.Role
                    }).ToList()
                }).ToList()
            };

            await transaction.CommitAsync();
            return dto;
        }
        catch {
            await transaction.RollbackAsync();
            throw;
        }
    }

    public async Task<int> AddBatchAsync(NewBatchDto dto) {
        if (dto.Quantity <= 0)
            throw new ValidationException("Quantity must be greater than zero");

        await using var transaction = await _context.Database.BeginTransactionAsync();

        try {
            var species = await _context.TreeSpecies
                .FirstOrDefaultAsync(s => s.LatinName == dto.Species);

            if (species == null)
                throw new NotFoundException($"Species '{dto.Species}' not found");

            var nursery = await _context.Nurseries
                .FirstOrDefaultAsync(n => n.Name == dto.Nursery);

            if (nursery == null)
                throw new NotFoundException($"Nursery '{dto.Nursery}' not found");

            var batch = new SeedlingBatch {
                Quantity = dto.Quantity,
                SpeciesId = species.SpeciesId,
                NurseryId = nursery.NurseryId,
                SownDate = dto.SownDate,
                ReadyDate = dto.ReadyDate
            };

            await _context.SeedlingBatches.AddAsync(batch);
            await _context.SaveChangesAsync();

            var batchEmployees = new List<Responsible>();
            foreach (var resp in dto.Responsible) {
                var emp = await _context.Employees.FindAsync(resp.EmployeeId);
                
                if (emp == null)
                    throw new NotFoundException($"Employee with id {resp.EmployeeId} not found");

                batchEmployees.Add(new Responsible {
                    BatchId = batch.BatchId,
                    EmployeeId = emp.EmployeeId,
                    Role = resp.Role
                });
            }

            await _context.Responsibles.AddRangeAsync(batchEmployees);
            await _context.SaveChangesAsync();

            await transaction.CommitAsync();
            return batch.BatchId;
        }
        catch {
            await transaction.RollbackAsync();
            throw new InvalidOperationException("Batch not added.");
        }
    }
}