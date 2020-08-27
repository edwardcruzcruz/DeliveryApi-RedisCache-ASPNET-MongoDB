using DeliveryApi.Models;
using DeliveryApi.Services;
using Microsoft.AspNetCore.Mvc;
using DeliveryApi.Redis;
using Microsoft.Extensions.Caching.Distributed;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DeliveryApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpleadosController : ControllerBase
    {
        /*private readonly IDistributedCache _cache;

        public ClientesController( IDistributedCache cache)
        {   
            _cache = cache;
        }*/
        
        private readonly EmpleadoService _empleadoService;        
        
        private readonly IDistributedCache _cache;

        public EmpleadosController(EmpleadoService empleadoService,IDistributedCache cache)
        {
            _empleadoService = empleadoService;
            _cache = cache;
        }

        [HttpGet]
        public ActionResult<List<Empleado>> Get() =>
            _empleadoService.Get();

        [HttpGet("{id:length(24)}", Name = "GetEmpleado")]
        public async Task<ActionResult<Empleado>> GetAsync(string id)
        {
            var empleado = new Empleado();
            bool EmployeIsHere= await RedisCache.ExistObjectAsync<bool>(_cache,id);
            if(!EmployeIsHere){
                empleado = _empleadoService.Get(id);

                if (empleado == null)
                {
                    return NotFound();
                }
                await RedisCache.SetObjectAsync<Empleado>(_cache,id,empleado);
            }else{
                empleado = await RedisCache.GetObjectAsync<Empleado>(_cache, id);
            }
            return empleado;
        }

        [HttpPost]
        public ActionResult<Empleado> Create(Empleado empleado)
        {
            _empleadoService.Create(empleado);

            return CreatedAtRoute("GetEmpleado", new { id = empleado.Id.ToString() }, empleado);
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, Empleado empleadoIn)
        {
            var empleado = _empleadoService.Get(id);

            if (empleado == null)
            {
                return NotFound();
            }

            _empleadoService.Update(id, empleadoIn);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var empleado = _empleadoService.Get(id);

            if (empleado == null)
            {
                return NotFound();
            }

            _empleadoService.Remove(empleado.Id);

            return NoContent();
        }
    }
}