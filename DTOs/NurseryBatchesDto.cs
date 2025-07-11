namespace Kolokwium_APBD.DTOs;

public class NurseryBatchesDto
{
    public int NurseryId { get; set; }
    public string Name { get; set; } = null!;
    public DateTime EstablishedDate { get; set; }
    public IEnumerable<BatchDto> Batches { get; set; } = null!;
}

public class BatchDto
{
    public int BatchId { get; set; }
    public int Quantity { get; set; }
    public DateTime SownDate { get; set; }
    public DateTime? ReadyDate { get; set; } = null!;
    public SpeciesDto Species { get; set; } = null!;
    public IEnumerable<ResponsibleDto> Responsible { get; set; } = null!;
}

public class SpeciesDto
{
    public string LatinName { get; set; } = null!;
    public int GrowthTimeInYears { get; set; }
}

public class ResponsibleDto
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Role { get; set; } = null!;
}