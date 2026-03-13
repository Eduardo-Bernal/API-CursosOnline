using CursosOnline.Domains;

namespace CursosOnline.Interface
{
    public interface IInstrutorRepository
    {
        List<Instrutor> Listar();
        Instrutor? ObterPorId(int id);
        Instrutor? ObterPorEmail(string email);
        bool EmailExiste(string email);
        void Adicionar(Instrutor instrutor);
        void Atualizar(Instrutor instrutor);
        void Remover(int id);
    }
}
