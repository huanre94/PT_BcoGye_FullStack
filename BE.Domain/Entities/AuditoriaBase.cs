namespace BE.Domain.Entities
{
    public abstract class AuditoriaBase
    {
        public bool EsActivo { get; set; } = true;
        public DateTime Creado { get; set; }
        public DateTime? Actualizado { get; set; }
    }
}
