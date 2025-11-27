namespace final.Models.DTOs
{
    public class CreateExpedienteDto
    {
        public string Codigo { get; set; }
        public string DelitoPrincipal { get; set; }
        public DateTime FechaRegistro { get; set; }
        public Guid ReclusoId { get; set; }
    }
}
