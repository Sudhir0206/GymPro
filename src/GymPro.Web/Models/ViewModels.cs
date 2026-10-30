using System.ComponentModel.DataAnnotations;

namespace GymPro.Web.Models;

public class MemberVm
{
    public int Id { get; set; }
    [Required, StringLength(120)] public string FullName { get; set; } = "";
    [Required, EmailAddress] public string Email { get; set; } = "";
    [Required, StringLength(20)] public string MobileNumber { get; set; } = "";
    [Required] public string Gender { get; set; } = "Male";
    [Range(1, 100)] public int Age { get; set; } = 25;
    public string Address { get; set; } = "";
    public DateTime JoiningDate { get; set; } = DateTime.Today;
    public bool IsActive { get; set; } = true;
}
public class TrainerVm
{
    public int Id { get; set; }
    [Required] public string TrainerName { get; set; } = "";
    [Required, EmailAddress] public string Email { get; set; } = "";
    [Required] public string MobileNumber { get; set; } = "";
    [Required] public string Specialization { get; set; } = "";
    [Range(0, 60)] public int Experience { get; set; }
}
public class MembershipVm
{
    public int Id { get; set; }
    [Required] public string PlanName { get; set; } = "";
    [Range(1, 60)] public int DurationInMonths { get; set; } = 1;
    [Range(typeof(decimal), "0.01", "1000000")] public decimal Price { get; set; }
    public string Description { get; set; } = "";
}
public class PaymentApiVm
{
    public int Id { get; set; }

    public int MemberId { get; set; }

    public MemberVm? Member { get; set; }

    public decimal Amount { get; set; }

    public DateTime PaymentDate { get; set; }

    public string PaymentMode { get; set; } = "";

    public string PaymentStatus { get; set; } = "";
}
public class PaymentVm
{
    public int Id { get; set; }
    public int MemberId { get; set; }
    public string? MemberName { get; set; }
    [Range(typeof(decimal), "0.01", "10000000")] public decimal Amount { get; set; }
    public DateTime PaymentDate { get; set; } = DateTime.Today;
    public string PaymentMode { get; set; } = "UPI";
    public string PaymentStatus { get; set; } = "Paid";
}
public class DashboardVm
{
    public int TotalMembers { get; set; }
    public int ActiveMembers { get; set; }
    public int TotalTrainers { get; set; }
    public int TotalMemberships { get; set; }
    public int TotalPayments { get; set; }
    public decimal TotalRevenue { get; set; }
    public List<RecentMemberVm> RecentMembers { get; set; } = [];
}
public class RecentMemberVm
{
    public int Id { get; set; }
    public string FullName { get; set; } = "";
    public string Email { get; set; } = "";
    public DateTime JoiningDate { get; set; }
    public bool IsActive { get; set; }
}
