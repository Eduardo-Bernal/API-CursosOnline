using System;
using System.Collections.Generic;

namespace CursosOnline.Domains;

public partial class Aluno
{
    public int Id { get; set; }

    public string Nome { get; set; } = null!;

    public string Email { get; set; } = null!;

    public byte[] Senha { get; set; } = null!;

    public virtual ICollection<Matricula> Matricula { get; set; } = new List<Matricula>();
}
