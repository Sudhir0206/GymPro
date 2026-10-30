using GymPro.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GymPro.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TrainersController(GymDbContext db) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Trainer>>> Get() =>
        Ok(await db.Trainers.AsNoTracking().OrderByDescending(x => x.Id).ToListAsync());

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Trainer>> GetById(int id)
    {
        var item = await db.Trainers.FindAsync(id);
        return item is null ? NotFound() : Ok(item);
    }

    [HttpPost]
    public async Task<ActionResult<Trainer>> Create(Trainer item)
    {
        item.Id = 0;
        item.CreatedDate = DateTime.UtcNow;
        db.Trainers.Add(item);
        await db.SaveChangesAsync();
        return CreatedAtAction(nameof(GetById), new { id = item.Id }, item);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, Trainer input)
    {
        var item = await db.Trainers.FindAsync(id);
        if (item is null) return NotFound();
        item.TrainerName = input.TrainerName;
        item.Email = input.Email;
        item.MobileNumber = input.MobileNumber;
        item.Specialization = input.Specialization;
        item.Experience = input.Experience;
        item.UpdatedDate = DateTime.UtcNow;
        await db.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var item = await db.Trainers.FindAsync(id);
        if (item is null) return NotFound();
        db.Trainers.Remove(item);
        await db.SaveChangesAsync();
        return NoContent();
    }
}
