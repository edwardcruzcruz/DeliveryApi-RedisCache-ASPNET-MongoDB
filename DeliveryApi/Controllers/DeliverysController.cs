using DeliveryApi.Models;
using DeliveryApi.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DeliveryApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeliverysController : ControllerBase
    {
        /*private readonly IDistributedCache _cache;

        public ClientesController( IDistributedCache cache)
        {   
            _cache = cache;
        }*/
        
        private readonly DeliveryService _deliveryService;        
        //private readonly IDistributedCache _cache;

        public DeliverysController(DeliveryService deliveryService)//,IDistributedCache cache)
        {
            _deliveryService = deliveryService;
            //_cache = cache;
        }

        [HttpGet]
        public ActionResult<List<Delivery>> Get() =>
            _deliveryService.Get();

        [HttpGet("{id:length(24)}", Name = "GetDelivery")]
        public ActionResult<Delivery> Get(string id)
        {
            var delivery = new Delivery();
            /* string cachedClientJson = _cache.GetString(id);
            if (string.IsNullOrEmpty(cachedClientJson)){*/
                delivery = _deliveryService.Get(id);

                if (delivery == null)
                {
                    return NotFound();
                }
            /* }else{
                cliente = cachedClientJson;
            }*/

            return delivery;
        }

        [HttpPost]
        public ActionResult<Delivery> Create(Delivery delivery)
        {
            _deliveryService.Create(delivery);

            return CreatedAtRoute("GetDelivery", new { id = delivery.Id.ToString() }, delivery);
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, Delivery deliveryIn)
        {
            var delivery = _deliveryService.Get(id);

            if (delivery == null)
            {
                return NotFound();
            }

            _deliveryService.Update(id, deliveryIn);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var delivery = _deliveryService.Get(id);

            if (delivery == null)
            {
                return NotFound();
            }

            _deliveryService.Remove(delivery.Id);

            return NoContent();
        }
    }
}