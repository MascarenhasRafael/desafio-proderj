using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace desafio_proderj.Models
{
  public class New
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ID { get; set; }

    [Required(ErrorMessage = "Campo Obrigatório")]
    [StringLength(100)]
    public string Title { get; set; }

    [Required(ErrorMessage = "Campo Obrigatório")]
    [StringLength(500)]
    public string Content { get; set; }

    [DisplayName("Selecione um Arquivo de Imagem")]
    [NotMapped]
    public IFormFile ImageFile { get; set; }

    [DisplayName("Selecione um Arquivo de Imagem")]
    public string ImageName { get; set; }
  }
}
