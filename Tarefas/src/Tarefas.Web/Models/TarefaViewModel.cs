using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

using System.ComponentModel.DataAnnotations;
namespace Tarefas.Web.Models;

public class TarefaViewModel
{
    public int Id { get; set; }
    
    [Required(ErrorMessage ="Informe o Título")]
    [MinLength(10,ErrorMessage ="A titulo deve ter no minumo 10 caracter")]
    [DisplayName("Título")]    
    public string? Titulo { get; set; }        
    [Required(ErrorMessage ="Informe a Descrição")]
    [MinLength(10,ErrorMessage ="A descrição deve ter no minumo 10 caracter")]
    [DisplayName("Descrição")]    
    public string? Descricao { get; set; }  

    [Required]
    [DisplayName("Concluída")]
    public bool Concluida { get; set; }
}