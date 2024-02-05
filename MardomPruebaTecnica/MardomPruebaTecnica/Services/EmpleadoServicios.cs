using MardomPruebaTecnica.Models;
using Newtonsoft.Json;

namespace MardomPruebaTecnica.Services
{
    public class EmpleadoServicios
    {
        private readonly string filePath = "C://PruebaTecnica_Mardom/Empleados.txt";

        public async Task CrearEmpleado(Empleados empleados)
        {
            try
            {
                var EmpleadosExistentes = await ReadEmployeesFromFile();

                if (EmpleadosExistentes.Any(e => e.Document == empleados.Document))
                {
                    throw new InvalidOperationException("Ya existe un empleado con este documento");
                }

                EmpleadosExistentes.Add(empleados);

                await WriteEmployeesToFile(EmpleadosExistentes);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al crear empleado: {ex.Message}");
            }
        }

        private async Task<List<Empleados>> ReadEmployeesFromFile()
        {
            try
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    string json = await reader.ReadToEndAsync();
                    return JsonConvert.DeserializeObject<List<Empleados>>(json) ?? new List<Empleados>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al leer empleados desde el archivo: {ex.Message}");
                throw;
            }
        }

        private async Task WriteEmployeesToFile(List<Empleados> empleados)
        {
            try
            {
                string json = JsonConvert.SerializeObject(empleados, Formatting.Indented);

                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    await writer.WriteAsync(json);
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine($"Error al escribir empleados en el archivo: {ex.Message}");
            }
        }
        
        public async Task<IEnumerable<Empleados>> ObtenerEmpleadoRangoSalarial(decimal salarioMinimo, decimal salarioMaximo)
        {
            try
            {
                var empleadosTodos = await ObtenerEmpleadosAll();

                var empleadosPorRango = empleadosTodos.Where(e => e.Salary >= salarioMinimo && e.Salary <= salarioMaximo);

                return empleadosPorRango;
            }

            catch (Exception ex)
            {
                Console.WriteLine($"Error al leer empleados por rango salarial: {ex.Message}");
                throw;
            }
        }

        public async Task<IEnumerable<Empleados>> ObtenerEmpleadosAll()
        {
            try
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    string json = await reader.ReadToEndAsync();
                    return JsonConvert.DeserializeObject<List<Empleados>>(json) ?? new List<Empleados>();
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine($"Error al leer todos los empleados desde el archivo: {ex.Message}");
                throw;
            }
        }

        public async Task<IEnumerable<Empleados>> ObtenerEmpleadoUnico()
        {
            try
            {
                var empleadosTodos = await ObtenerEmpleadosAll();

                var empleadosUnicos = empleadosTodos.GroupBy(e => e.Document).Select(g => g.First());

                return empleadosUnicos;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al leer empleados evitando duplicados: {ex.Message}");
                throw;
            }
        }

        public async Task<IEnumerable<Empleados>> SalariosAjustados()
        {
            try
            {
                var empleadosTodos = await ObtenerEmpleadosAll();

                var empleadosSalariosAjustados = empleadosTodos.Select(employee =>
                {
                    if (employee.Salary > 100000)
                    {
                        employee.Salary *= 1.25m;
                    }
                    else
                    {
                        employee.Salary *= 0.7m;
                    }
                    return employee;
                });

                return empleadosSalariosAjustados;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al calcular salarios ajustados: {ex.Message}");
                throw;
            }
        }

        /*public async Task<Dictionary<string, double>> ObtenerPorcentajeGenero()
        {

        }*/

        public async Task BorrarEmpleado(string document)
        {
            try
            {
                var empleadosTodos = (await ObtenerEmpleadosAll()).ToList();

                var empleadoPorBorrar = empleadosTodos.FirstOrDefault(e => e.Document == document);

                if (empleadoPorBorrar != null)
                {
                    empleadosTodos.Remove(empleadoPorBorrar);

                    await WriteEmployeesToFile(empleadosTodos);
                }
                else
                {
                    Console.WriteLine("No se encontro un empleado con el documento especificado");
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Error al borrar empleado por documento: {ex.Message}");
            }
        }
    }
}
