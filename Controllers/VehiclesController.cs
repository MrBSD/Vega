using Microsoft.AspNetCore.Mvc;
using Vega.Models;

namespace Vega.Controllers
{
    [Route("/api/vehicles")]
    public class VehiclesController:Controller
    {
        [HttpPost]
        public  IActionResult CreateVehicle(Vehicle vehicle)
        {
            return Ok(vehicle);
        }
    }
}