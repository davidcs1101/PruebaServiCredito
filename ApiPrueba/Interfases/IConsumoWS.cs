namespace ApiPrueba.Interfases
{
    public interface IConsumoWS
    {
        Task<string> GetNotificaciones(string token, string startDate, string endDate);
        Task<string> GetTokenAsync(string usuario, string clave);
    }
}
