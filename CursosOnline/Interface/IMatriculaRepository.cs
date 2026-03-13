using CursosOnline.Domains;

namespace CursosOnline.Interface
{
    public interface IMatriculaRepository
    {
        List<Matricula> Listar();
        Matricula? ObterPorId(int id);
        List<Matricula> ListarPorAluno(int alunoId);
        List<Matricula> ListarPorCurso(int cursoId);
        void Adicionar(Matricula matricula);
        void Remover(int id);
    }
}
