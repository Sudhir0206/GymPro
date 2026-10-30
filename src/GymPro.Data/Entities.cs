namespace GymPro.Data;

public class Member
{
    public int Id { get; set; }
    public string FullName { get; set; } = "";
    public string Email { get; set; } = "";
    public string MobileNumber { get; set; } = "";
    public string Gender { get; set; } = "";
    public int Age { get; set; }
    public string Address { get; set; } = "";
    public DateTime JoiningDate { get; set; } = DateTime.Today;
    public bool IsActive { get; set; } = true;
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedDate { get; set; }
}

public class Trainer
{
    public int Id { get; set; }
    public string TrainerName { get; set; } = "";
    public string Email { get; set; } = "";
    public string MobileNumber { get; set; } = "";
    public string Specialization { get; set; } = "";
    public int Experience { get; set; }
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedDate { get; set; }
}

public class Membership
{
    public int Id { get; set; }
    public string PlanName { get; set; } = "";
    public int DurationInMonths { get; set; }
    public decimal Price { get; set; }
    public string Description { get; set; } = "";
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedDate { get; set; }
}

public class Payment
{
    public int Id { get; set; }
    public int MemberId { get; set; }
    public Member? Member { get; set; }
    public decimal Amount { get; set; }
    public DateTime PaymentDate { get; set; } = DateTime.Today;
    public string PaymentMode { get; set; } = "UPI";
    public string PaymentStatus { get; set; } = "Paid";
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
}
