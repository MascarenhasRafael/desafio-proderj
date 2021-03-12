using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace desafio_proderj.Models
{
  public class New
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ID { get; set; }

    [Required]
    [StringLength(100)]
    public string Title { get; set; }

    [StringLength(500)]
    public string Content { get; set; }
  }
}
