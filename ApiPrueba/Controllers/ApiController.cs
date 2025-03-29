using ApiPrueba.Dtos;
using ApiPrueba.Interfases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiPrueba.Controllers
{
    [ApiController]
    [Route("apiPrueba")]
    public class ApiController : ControllerBase
    {
        private readonly IConsumoWS _consumoWS;

        public ApiController(IConsumoWS consumoWS)
        {
            _consumoWS = consumoWS;
        }

        [HttpPost("ObtenerToken")]
        public async Task<string> MostrarToken(LoginRequest loginRequest)
        {
            return await _consumoWS.GetTokenAsync(loginRequest);
        }

        [HttpGet("ObtenerNotificaciones")]
        public async Task<string> MostrarNotificaciones(string token, string fechaInicio, string fechaFin)
        {
            return await _consumoWS.GetNotificaciones(token, fechaInicio,fechaFin);
        }
    }
}
