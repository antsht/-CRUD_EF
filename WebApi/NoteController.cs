using Microsoft.AspNetCore.Mvc;
using BusinessLogic;
using DataAccess;

namespace WebApi.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class NoteController(INoteService noteService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateAsync(string text)
    {
        await noteService.CreateAsync(text);
        return NoContent();
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetByIdAsync([FromRoute] int id)
    {
        var result = await noteService.GetByIdAsync(id);
        if (result == null || string.IsNullOrEmpty(result))
        {
            return NotFound();
        }
        return Ok(result);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateAsync([FromRoute] int id, [FromBody] string newText)
    {
        await noteService.UpdateAsync(id, newText);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteAsync([FromRoute] int id)
    {   
        try{
            await noteService.DeleteAsync(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
