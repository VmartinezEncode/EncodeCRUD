using DataAccess.Generic;
using Entities.Domain;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly IGenericRepository<Usuario> _genericRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UsuariosController(IGenericRepository<Usuario> genericRepository, IUnitOfWork unitOfWork)
        {
            this._genericRepository = genericRepository;
            this._unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IEnumerable<Usuario>> Get()
        {
            return await _genericRepository.GetAsync();
        }

        [HttpGet("buscarPersona")]
        public async Task<IEnumerable<Usuario>> Get([FromQuery] string nombre)
        {
            return await _genericRepository.GetAsync(a => a.Nombre == nombre);
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] Usuario user)
        {
            if (ModelState.IsValid)
            {

                bool state = await _genericRepository.CreateAsync(user);
                if (state)
                {
                    _unitOfWork.Commit();
                    return Ok();
                }
                else
                {
                    return NotFound();
                }
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            bool state = await _genericRepository.DeleteByIdAsync(id);
            if (state)
            {
                _unitOfWork.Commit();
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] Usuario user)
        {
            bool state = await _genericRepository.UpdateByIdAsync(id, user);
            if (state)
            {
                _unitOfWork.Commit();
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }
    }
}
