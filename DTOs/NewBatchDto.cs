namespace Kolokwium_APBD.DTOs;

public class NewBatchDto
{
    public int Quantity { get; set; }
    public string Species { get; set; } = null!;
    public string Nursery { get; set; } = null!;
    public DateTime SownDate { get; set; }
    public DateTime ReadyDate { get; set; }
    public List<NextResponsibleDto> Responsible { get; set; } = null!;
}

public class NextResponsibleDto
{
    public int EmployeeId { get; set; }
    public string Role { get; set; } = null!;
}