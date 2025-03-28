using ApiPrueba.Interfases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiPrueba.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ApiController : ControllerBase
    {
        private readonly IConsumoWS _consumoWS;

        public ApiController(IConsumoWS consumoWS = null)
        {
            _consumoWS = consumoWS;
        }

        [HttpPost(Name = "ObtenerToken")]
        public async Task<string> MostrarToken(string usuario, string clave)
        {
            return await _consumoWS.GetTokenAsync(usuario,clave);
        }

        [HttpGet(Name = "ObtenerNotificaciones")]
        public async Task<string> MostrarNotificaciones(string token, string fechaInicio, string fechaFin)
        {
            return await _consumoWS.GetNotificaciones(token, fechaInicio,fechaFin);
        }
    }
}
