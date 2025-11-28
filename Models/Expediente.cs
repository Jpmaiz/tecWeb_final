namespace final.Models
{
    public class Expediente
    {
        public Guid Id { get; set; }
        public string Codigo { get; set; }
        public string DelitoPrincipal { get; set; }
        public DateTime FechaRegistro { get; set; }

        // Relación 1:1 con Recluso
        //public Guid ReclusoId { get; set; }
        //public Recluso Recluso { get; set; }
    }
}
