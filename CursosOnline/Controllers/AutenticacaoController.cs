using CursosOnline.Applications.Services;
using CursosOnline.DTOs.AutenticacaoDto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CursosOnline.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutenticacaoController : ControllerBase
    {
        private readonly AutenticacaoService _autenticacaoService;

        public AutenticacaoController(AutenticacaoService autenticacaoService)
        {
            _autenticacaoService = autenticacaoService;
        }

        [HttpPost("login")]
        public ActionResult Login(LoginDto loginDto)
        {
            try
            {
                var token = _autenticacaoService.Login(loginDto);
                return Ok(token);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
