using System.ComponentModel.DataAnnotations;
using Kolokwium_APBD.DTOs;
using Kolokwium_APBD.Exceptions;
using Kolokwium_APBD.Services;
using Microsoft.AspNetCore.Mvc;

namespace Kolokwium_APBD.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BatchesController : ControllerBase {
    private readonly IDbService _dbService;

    public BatchesController(IDbService dbService) {
        _dbService = dbService;
    }
    
    [HttpPost]
    public async Task<IActionResult> AddBatch([FromBody] NewBatchDto dto)
    {
        if (dto == null)
            return BadRequest("Batch data must be provided.");

        try {
            var batchId = await _dbService.AddBatchAsync(dto);
            return Created($"api/batches/{batchId}", new { batchId });
        }
        catch (ValidationException ex) {
            return BadRequest(ex.Message);
        }
        catch (NotFoundException ex) {
            return NotFound(ex.Message);
        }
        catch (InvalidOperationException ex) {
            return StatusCode(500, ex.Message);
        }
    }
}