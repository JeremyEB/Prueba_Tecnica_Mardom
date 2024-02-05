using MardomPruebaTecnica.Models;
using MardomPruebaTecnica.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MardomPruebaTecnica.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpleadosController : ControllerBase
    {
        private readonly EmpleadoServicios empleadoServicios;

        public EmpleadosController(EmpleadoServicios empleadoServicios)
        {
            this.empleadoServicios = empleadoServicios;
        }

        [HttpPost]
        public async Task<IActionResult> CrearEmpleado([FromBody] Empleados empleados)
        {
            await this.empleadoServicios.CrearEmpleado(empleados);
            return Ok("Empleado creado exitosamente");
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerEmpleadosAll()
        {
            var empleados = await this.empleadoServicios.ObtenerEmpleadosAll();
            return Ok(empleados);
        }

        [HttpGet("RangoSalarial")]
        public async Task<IActionResult> ObtenerEmpleadosPorRangoSalarial([FromQuery] decimal salarioMinimo, [FromQuery] decimal salarioMaximo)
        {
            var empleadosPorRango = await this.empleadoServicios.ObtenerEmpleadoRangoSalarial(salarioMinimo, salarioMaximo);
            return Ok(empleadosPorRango);
        }

        [HttpGet("EmpleadosUnicos")]
        public async Task<IActionResult> ObtenerEmpleadoUnico()
        {
            var empleadosUnicos = await this.empleadoServicios.ObtenerEmpleadoUnico();
            return Ok(empleadosUnicos);
        }

        [HttpGet("SalariosAjustados")]
        public async Task<IActionResult> SalariosAjustados()
        {
            var empleadosSalariosAjustados = await this.empleadoServicios.SalariosAjustados();
            return Ok(empleadosSalariosAjustados);
        }

        [HttpDelete("BorrarEmpleados")]
        public async Task<IActionResult> BorrarEmpleado([FromQuery] string document)
        {
            await this.empleadoServicios.BorrarEmpleado(document);
            return Ok("Empleado eliminado exitosamente");
        }
    }
}
