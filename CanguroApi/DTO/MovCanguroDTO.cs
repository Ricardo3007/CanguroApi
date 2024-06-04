namespace CanguroApi.DTO
{
    public class MovCanguroDTO
    {
        public int Codigo { get; set; }

        public string Descripcion { get; set; }

        public string Direccion { get; set; } 

        public string Identificacion { get; set; } 

        public DateTime FechaCreacion { get; set; }

        public int Moneda { get; set; }
        public string? DescripcionMoneda { get; set; }
        public bool Estado { get; set; }
    }
}
