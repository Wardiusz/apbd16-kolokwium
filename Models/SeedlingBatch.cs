using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Kolokwium_APBD.Models;


[Table("SeedlingBatch")]
public class SeedlingBatch
{
    [Key] public int BatchId { get; set; }
    [ForeignKey(nameof(Nursery))] public int NurseryId { get; set; }
    [ForeignKey(nameof(TreeSpecies))] public int SpeciesId { get; set; }
    public int Quantity { get; set; }
    public DateTime SownDate { get; set; }
    public DateTime? ReadyDate { get; set; }

    public TreeSpecies TreeSpecies { get; set; } = null!;
    public Nursery Nursery { get; set; } = null!;

    public ICollection<Responsible> Responsibles { get; set; } = null!;
}