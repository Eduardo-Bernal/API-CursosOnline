using CursosOnline.Domains;

namespace CursosOnline.Interface
{
    public interface ICursoRepository
    {
        List<Curso> Listar();
        Curso? ObterPorId(int id);
        List<Curso> ListarPorInstrutor(int instrutorId);
        void Adicionar(Curso curso);
        void Atualizar(Curso curso);
        void Remover(int id);
    }
}
