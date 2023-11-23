using AseguradoraApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace AseguradoraApp.Controllers
{
    
    public class RegisterDriverController : Controller
    {

        public RegisterDriverController()
        {
            
        }

        public IActionResult RegisterDriverDos()
        {
            return View("RegisterDriver");
        }

        [HttpPost]
        public async Task<IActionResult> RegisterNewDriver([Bind("DriverName,DriverLastname,DriverBirthday,TelephoneNumber,License,DriverPassword")]DriverClient newDriver) 
        {
            try
            {
                // Convertir el objeto a JSON
                string jsonBody = JsonSerializer.Serialize(newDriver);

                // Configurar el cliente HttpClient
                using (HttpClient httpClient = new HttpClient())
                {
                    // URL de la API a la que te estás conectando
                    string apiUrl = "https://ejemplo.com/api/registro";

                    // Crear el contenido de la solicitud con el cuerpo JSON
                    HttpContent content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

                    // Realizar la solicitud POST
                    HttpResponseMessage response = await httpClient.PostAsync(apiUrl, content);

                    // Verificar el estado de la respuesta
                    if (response.IsSuccessStatusCode)
                    {
                        // Si la respuesta es exitosa, puedes manejar el resultado aquí
                        // Por ejemplo, leer el contenido de la respuesta
                        string responseBody = await response.Content.ReadAsStringAsync();

                        // Puedes retornar una respuesta de éxito si es necesario
                        return Ok(responseBody);
                    }
                    else
                    {
                        // Si la respuesta no es exitosa, puedes manejar el error aquí
                        // Por ejemplo, leer el contenido de la respuesta para obtener detalles del error
                        string errorResponse = await response.Content.ReadAsStringAsync();

                        // Puedes retornar un código de error y el mensaje de error si es necesario
                        return BadRequest(errorResponse);
                    }
                }
            }
            catch (Exception ex)
            {
                // Manejar excepciones si ocurren
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }
        

    }
}
