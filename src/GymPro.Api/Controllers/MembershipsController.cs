using GymPro.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GymPro.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MembershipsController(GymDbContext db) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Membership>>> Get() =>
        Ok(await db.Memberships.AsNoTracking().OrderBy(x => x.Price).ToListAsync());

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Membership>> GetById(int id)
    {
        var item = await db.Memberships.FindAsync(id);
        return item is null ? NotFound() : Ok(item);
    }

    [HttpPost]
    public async Task<ActionResult<Membership>> Create(Membership item)
    {
        item.Id = 0;
        item.CreatedDate = DateTime.UtcNow;
        db.Memberships.Add(item);
        await db.SaveChangesAsync();
        return CreatedAtAction(nameof(GetById), new { id = item.Id }, item);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, Membership input)
    {
        var item = await db.Memberships.FindAsync(id);
        if (item is null) return NotFound();
        item.PlanName = input.PlanName;
        item.DurationInMonths = input.DurationInMonths;
        item.Price = input.Price;
        item.Description = input.Description;
        item.UpdatedDate = DateTime.UtcNow;
        await db.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var item = await db.Memberships.FindAsync(id);
        if (item is null) return NotFound();
        db.Memberships.Remove(item);
        await db.SaveChangesAsync();
        return NoContent();
    }
}
