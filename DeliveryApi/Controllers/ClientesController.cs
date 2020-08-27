using DeliveryApi.Models;
using DeliveryApi.Redis;
using DeliveryApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace DeliveryApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {   
        private readonly ClienteService _clienteService;        
        private readonly IDistributedCache _cache;

        public ClientesController(ClienteService clienteService,IDistributedCache cache)
        {
            _clienteService = clienteService;
            _cache = cache;
        }

        [HttpGet]
        public ActionResult<List<Cliente>> Get() =>
            _clienteService.Get();

        [HttpGet("{id:length(24)}", Name = "GetCliente")]
        public async Task<ActionResult<Cliente>> GetAsync(string id)
        {
            var cliente = new Cliente();
            //Trace.WriteLine(id);//client.Nombre);
            // string cachedClientJson = _cache.GetString(id);
            bool ClientIsHere= await RedisCache.ExistObjectAsync<bool>(_cache,id);
            if (!ClientIsHere){//string.IsNullOrEmpty(cachedClientJson)){*/
                cliente = _clienteService.Get(id);

                if (cliente == null)
                {
                    return NotFound();
                }
                await RedisCache.SetObjectAsync<Cliente>(_cache,id,cliente);
            } else{
                cliente = await RedisCache.GetObjectAsync<Cliente>(_cache, id);
            }

            return cliente;
        }

        [HttpPost]
        public ActionResult<Cliente> Create(Cliente cliente)
        {
            _clienteService.Create(cliente);

            return CreatedAtRoute("GetCliente", new { id = cliente.Id.ToString() }, cliente);
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, Cliente clienteIn)
        {
            var cliente = _clienteService.Get(id);

            if (cliente == null)
            {
                return NotFound();
            }

            _clienteService.Update(id, clienteIn);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var cliente = _clienteService.Get(id);

            if (cliente == null)
            {
                return NotFound();
            }

            _clienteService.Remove(cliente.Id);

            return NoContent();
        }
    }
}