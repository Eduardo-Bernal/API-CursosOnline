using CursosOnline.Applications.Services;
using CursosOnline.DTOs.InstrutorDto;
using CursosOnline.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CursosOnline.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InstrutorController : ControllerBase
    {
        private readonly InstrutorService _service;

        public InstrutorController(InstrutorService service)
        {
            _service = service; 
        }

        [HttpGet]
        public ActionResult<List<LerInstrutorDto>> Listar()
        {
            List<LerInstrutorDto> instrutor = _service.Listar();

            return Ok(instrutor);
        }

        [HttpGet("{id}")]
        public ActionResult<LerInstrutorDto> ObterPorId(int id)
        {
            LerInstrutorDto instrutor = _service.ObterPorId(id);

             try
            {
                if (instrutor == null) return NotFound();

                return Ok(instrutor);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("email/{email}")]
        public ActionResult<LerInstrutorDto> ObterPorEmail(string email)
        {
            LerInstrutorDto instrutor = _service.ObterPorEmail(email);

            try
            {
                if (instrutor == null) return NotFound();

                return Ok(instrutor);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


        }

        [HttpPost]
        public ActionResult<LerInstrutorDto> Adicionar(CriarInstrutorDto criarInstrutorDto)
        {
            try
            {
                LerInstrutorDto instrutorDto = _service.Adicionar(criarInstrutorDto);
                if (instrutorDto == null) return NotFound();

                return StatusCode(201, instrutorDto);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("{id}")]
        public ActionResult<LerInstrutorDto> Atualizar(int id, CriarInstrutorDto criarInstrutorDto)
        {
            try
            {
                LerInstrutorDto instrutorAtualizado = _service.Atualizar(id, criarInstrutorDto);

                return StatusCode(200, instrutorAtualizado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }


        [HttpDelete("{id}")]
        public IActionResult Remover(int id)
        {
            try
            {
                _service.Remover(id);
                return StatusCode(204, id);
            }
            catch (DomainException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
