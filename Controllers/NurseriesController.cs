using Microsoft.AspNetCore.Mvc;
using Kolokwium_APBD.Services;
using Kolokwium_APBD.Exceptions;

namespace Kolokwium_APBD.Controllers;

[ApiController]
[Route("api/[controller]")]
public class NurseriesController : ControllerBase
{
    private readonly IDbService _dbService;

    public NurseriesController(IDbService dbService)
    {
        _dbService = dbService;
    }
    
    [HttpGet("{id}/batches")]
    public async Task<IActionResult> GetOrder(int id)
    {
        try {
            var order = await _dbService.GetNurseryBatchesAsync(id);
            return Ok(order);
        } 
        catch (NotFoundException) {
            return NotFound();
        }
    }
}