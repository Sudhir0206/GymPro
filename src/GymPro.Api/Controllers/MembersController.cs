using GymPro.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GymPro.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MembersController(GymDbContext db) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Member>>> Get(string? search)
    {
        var query = db.Members.AsNoTracking();
        if (!string.IsNullOrWhiteSpace(search))
            query = query.Where(x => x.FullName.Contains(search) || x.Email.Contains(search) || x.MobileNumber.Contains(search));
        return Ok(await query.OrderByDescending(x => x.Id).ToListAsync());
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Member>> GetById(int id)
    {
        var item = await db.Members.FindAsync(id);
        return item is null ? NotFound() : Ok(item);
    }

    [HttpPost]
    public async Task<ActionResult<Member>> Create(Member item)
    {
        if (await db.Members.AnyAsync(x => x.Email == item.Email))
            return Conflict(new { message = "A member with this email already exists." });
        item.Id = 0;
        item.CreatedDate = DateTime.UtcNow;
        db.Members.Add(item);
        await db.SaveChangesAsync();
        return CreatedAtAction(nameof(GetById), new { id = item.Id }, item);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, Member input)
    {
        var item = await db.Members.FindAsync(id);
        if (item is null) return NotFound();
        if (await db.Members.AnyAsync(x => x.Id != id && x.Email == input.Email))
            return Conflict(new { message = "Another member already uses this email." });
        item.FullName = input.FullName;
        item.Email = input.Email;
        item.MobileNumber = input.MobileNumber;
        item.Gender = input.Gender;
        item.Age = input.Age;
        item.Address = input.Address;
        item.JoiningDate = input.JoiningDate;
        item.IsActive = input.IsActive;
        item.UpdatedDate = DateTime.UtcNow;
        await db.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var item = await db.Members.FindAsync(id);
        if (item is null) return NotFound();
        if (await db.Payments.AnyAsync(x => x.MemberId == id))
            return Conflict(new { message = "This member has payment history and cannot be deleted." });
        db.Members.Remove(item);
        await db.SaveChangesAsync();
        return NoContent();
    }
}
