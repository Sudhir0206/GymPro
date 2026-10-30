using GymPro.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GymPro.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PaymentsController(GymDbContext db) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Payment>>> Get() =>
        Ok(await db.Payments.AsNoTracking().Include(x => x.Member).OrderByDescending(x => x.PaymentDate).ToListAsync());

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Payment>> GetById(int id)
    {
        var item = await db.Payments.AsNoTracking().Include(x => x.Member).FirstOrDefaultAsync(x => x.Id == id);
        return item is null ? NotFound() : Ok(item);
    }

    [HttpPost]
    public async Task<ActionResult<Payment>> Create(Payment item)
    {
        if (!await db.Members.AnyAsync(x => x.Id == item.MemberId))
            return BadRequest(new { message = "Select a valid member." });
        if (item.Amount <= 0)
            return BadRequest(new { message = "Payment amount must be greater than zero." });
        item.Id = 0;
        item.Member = null;
        item.CreatedDate = DateTime.UtcNow;
        db.Payments.Add(item);
        await db.SaveChangesAsync();
        return CreatedAtAction(nameof(GetById), new { id = item.Id }, item);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var item = await db.Payments.FindAsync(id);
        if (item is null) return NotFound();
        db.Payments.Remove(item);
        await db.SaveChangesAsync();
        return NoContent();
    }
}
