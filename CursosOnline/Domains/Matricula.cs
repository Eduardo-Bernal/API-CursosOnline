using System;
using System.Collections.Generic;

namespace CursosOnline.Domains;

public partial class Matricula
{
    public int Id { get; set; }

    public int AlunoId { get; set; }

    public int CursoId { get; set; }

    public virtual Aluno Aluno { get; set; } = null!;

    public virtual Curso Curso { get; set; } = null!;
}
