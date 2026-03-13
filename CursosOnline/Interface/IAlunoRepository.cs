using CursosOnline.Domains;

namespace CursosOnline.Interface
{
    public interface IAlunoRepository
    {
        List<Aluno> Listar();
        Aluno? ObterPorId(int id);
        Aluno? ObterPorEmail(string email);
        bool EmailExiste(string email);
        void Adicionar(Aluno aluno);
        void Atualizar(Aluno aluno);
        void Remover(int id);
    }
}
