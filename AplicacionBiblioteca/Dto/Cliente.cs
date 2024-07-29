namespace AplicacionBiblioteca.Dto
{
    public class Cliente
    {
        static int cont = 1;
        public Cliente(string nombre, string numerodocumento, string telefono, string correo)
        {
            Identificador = cont;
            Nombre = nombre;
            NumeroDocumento = numerodocumento;
            Telefono = telefono;
            Correo = correo;
            cont++;
        }
        public int Identificador { get; set; }
        public string Nombre { get; set; }
        public string NumeroDocumento { get; set; }
        public string Telefono { get; set; }
        public string Correo { get; set; }

    }
}
