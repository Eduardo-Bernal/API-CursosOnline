using System;
using System.Collections.Generic;

namespace CursosOnline.Domains;

public partial class Curso
{
    public int Id { get; set; }

    public string Nome { get; set; } = null!;

    public int CargaHoraria { get; set; }

    public int InstrutorId { get; set; }

    public bool StatusCurso { get; set; }

    public virtual Instrutor Instrutor { get; set; } = null!;

    public virtual ICollection<Matricula> Matricula { get; set; } = new List<Matricula>();
}
