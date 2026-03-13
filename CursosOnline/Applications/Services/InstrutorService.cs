using CursosOnline.Domains;
using CursosOnline.DTOs.InstrutorDto;
using CursosOnline.Exceptions;
using CursosOnline.Interface;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text;

namespace CursosOnline.Applications.Services
{
    public class InstrutorService
    {

        private readonly IInstrutorRepository _repository;

        public InstrutorService(IInstrutorRepository repository)
        {
            _repository = repository;
        }

        private static LerInstrutorDto LerDto(Instrutor instrutor)
        {
            LerInstrutorDto lerInstrutorDto = new LerInstrutorDto
            {
                InstrutorId = instrutor.Id,
                Nome = instrutor.Nome,
                Email = instrutor.Email,
                AreaEspecializacao = instrutor.AreaEspecializacao,

            };
            return lerInstrutorDto;
        }

        public List<LerInstrutorDto> Listar()
        {
            List<Instrutor> instrutors = _repository.Listar();

            List<LerInstrutorDto> instrutoresDto = instrutors.Select(InstrutorBanco => LerDto(InstrutorBanco)).ToList();
            return instrutoresDto;
        }

        private static void ValidarEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email) || !email.Contains("@"))
            {
                throw new DomainException("Email Inválido.");
            }
        }
        private static void ValidarNome(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new DomainException("Nome Inválido.");
            }
        }

        private static byte[] HashSenha(string senha)
        {
            if (string.IsNullOrEmpty(senha))
            {
                throw new DomainException("Senha é obrigatória.");
            }
            using var sha256 = SHA256.Create();
            return sha256.ComputeHash(Encoding.UTF8.GetBytes(senha));
        }

        public LerInstrutorDto ObterPorId(int id)
        {
            Instrutor instrutor = _repository.ObterPorId(id);

            if (instrutor == null)
            {
                throw new DomainException("Usuário não existe.");
            }

            return LerDto(instrutor);
        }

        public LerInstrutorDto ObterPorEmail(string email)
        {
            Instrutor instrutor = _repository.ObterPorEmail(email);

            if (instrutor == null)
            {
                throw new DomainException("Usuário não existe.");
            }

            return LerDto(instrutor);
        }

        public LerInstrutorDto Adicionar(CriarInstrutorDto instrutorDto)
        {
            ValidarEmail(instrutorDto.Email);

            if (_repository.EmailExiste(instrutorDto.Email)) throw new DomainException("Email já cadastrado!");

            Instrutor instrutor = new Instrutor
            {
                Nome = instrutorDto.Nome,
                Email = instrutorDto.Email,
                Senha = HashSenha(instrutorDto.Senha)
            };

            _repository.Adicionar(instrutor);

            return LerDto(instrutor);
        }

        public LerInstrutorDto Atualizar(int id, CriarInstrutorDto InstrutorDto)
        {
            Instrutor instrutorBanco = _repository.ObterPorId(id);

            if (instrutorBanco == null) throw new DomainException("Usuário não foi encontrado!");

            ValidarEmail(InstrutorDto.Email);

            Instrutor InstrutorComMesmoEmail = _repository.ObterPorEmail(InstrutorDto.Email);

            if (InstrutorComMesmoEmail != null && InstrutorComMesmoEmail.Id != id)
            {
                throw new DomainException("Já existe um usuário com este email");
            }

            instrutorBanco.Nome = InstrutorDto.Nome;
            instrutorBanco.Email = InstrutorDto.Email;
            instrutorBanco.Senha = HashSenha(InstrutorDto.Senha);

            _repository.Atualizar(instrutorBanco);

            return LerDto(instrutorBanco);
        }

        public void Remover(int id)
        {
            Instrutor instrutor = _repository.ObterPorId(id);

            if (instrutor == null) throw new DomainException("Usuário não encontrado");

            _repository.Remover(id);
        }



    }
}

