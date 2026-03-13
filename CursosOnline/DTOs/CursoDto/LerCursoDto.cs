namespace CursosOnline.DTOs.CursoDto
{
    public class LerCursoDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int CargaHoraria { get; set; }
        public bool StatusCurso { get; set; }
        public int InstrutorId { get; set; }

    }
}
