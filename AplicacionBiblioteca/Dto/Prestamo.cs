namespace AplicacionBiblioteca.Dto
{
    public class Prestamo
    {
        static int cont = 1;
        public Prestamo(int clientesolicitante, int[] libros, DateTime fechasolicitud, DateTime fechavencimiento, int creditototal)
        {
            Identificador = cont;
            ClienteSolicitante = clientesolicitante;
            Libros = libros;
            FechaSolicitud = fechasolicitud;
            FechaVencimiento = fechavencimiento;
            CreditoTotal = creditototal;
            cont++;
        }
        public int Identificador { get; set; }
        public int ClienteSolicitante { get; set; }
        public int[] Libros { get; set; }
        public DateTime FechaSolicitud { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public int CreditoTotal { get; set; }
    }
}
