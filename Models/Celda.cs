namespace final.Models
{
    public class Celda
    {
        public Guid Id { get; set; }
        public int Numero { get; set; }
        public string Pabellon { get; set; }
        public int Capacidad { get; set; }

        // Relación 1:N con Recluso
        public ICollection<Recluso> Reclusos { get; set; } = new List<Recluso>();
    }
}
