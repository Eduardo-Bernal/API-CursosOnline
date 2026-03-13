using CursosOnline.Applications.Autenticacao;
using CursosOnline.Domains;
using CursosOnline.DTOs.AutenticacaoDto;
using CursosOnline.Interface;

namespace CursosOnline.Applications.Services
{
    public class AutenticacaoService
    {
        private readonly IInstrutorRepository _repository;
        private readonly GeradorTokenJwt _tokenJwt;

        public AutenticacaoService(IInstrutorRepository repository, GeradorTokenJwt geradorTokenJwt)
        {
            _repository = repository;
            _tokenJwt = geradorTokenJwt;
        }

        private static bool VerificarSenha(string senhaDigitada, byte[] senhaHashBanco)
        {
            using var sha = System.Security.Cryptography.SHA256.Create();

            var hashDigitada = sha.ComputeHash(System.Text.Encoding.UTF8.GetBytes(senhaDigitada));

            return hashDigitada.SequenceEqual(senhaHashBanco);
        }

        public TokenDto Login(LoginDto loginDto)
        {
            Instrutor instrutor = _repository.ObterPorEmail(loginDto.Email);

            if (instrutor == null) throw new Exception("Email ou Senha Invalidos.");

            if (instrutor == null) throw new Exception("Email ou Senha Invalidos.");

            if (!VerificarSenha(loginDto.Senha, instrutor.Senha)) throw new Exception("Email ou Senha Invalidos.");

            var token = _tokenJwt.GerarToken(instrutor);

            TokenDto novoToken = new TokenDto() { Token = token };

            return novoToken;
        }
    }
}
