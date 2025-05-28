using CW09_s30527.models.DTO;
using CW09_s30527.Services;


namespace CW09_s30527.Controllers;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]

public class TemplateController(IDbService dbService) : ControllerBase

{
    [HttpPost("prescription")]
    public async Task<IActionResult> CreatePrescription(CreatePrescriptionDto dto)
    {
        try
        {
            await dbService.CreatePrescription(dto);
            return Ok("Prescription created successfully");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("patient/{id}")]
    public async Task<IActionResult> GetPatient(int id)
    {
        try
        {
            var result = await dbService.GetPatient(id);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }

}