using GymPro.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GymPro.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DashboardController(GymDbContext db) : ControllerBase
{
    [HttpGet("summary")]
    public async Task<IActionResult> Summary()
    {
        var result = new
        {
            totalMembers = await db.Members.CountAsync(),
            activeMembers = await db.Members.CountAsync(x => x.IsActive),
            totalTrainers = await db.Trainers.CountAsync(),
            totalMemberships = await db.Memberships.CountAsync(),
            totalPayments = await db.Payments.CountAsync(),
            totalRevenue = await db.Payments.Where(x => x.PaymentStatus == "Paid").SumAsync(x => (decimal?)x.Amount) ?? 0m,
            recentMembers = await db.Members.AsNoTracking().OrderByDescending(x => x.Id).Take(5)
                .Select(x => new { x.Id, x.FullName, x.Email, x.JoiningDate, x.IsActive }).ToListAsync()
        };
        return Ok(result);
    }
}
