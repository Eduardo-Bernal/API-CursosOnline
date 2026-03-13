namespace CursosOnline.DTOs.CursoDto
{
    public class CriarCursoDto
    {
        public string Nome { get; set; } = null!;

        public int CargaHoraria { get; set; } 

        public int InstrutorId { get; set; }
    }
}
