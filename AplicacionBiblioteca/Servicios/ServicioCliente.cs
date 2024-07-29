using AplicacionBiblioteca.Dto;

namespace AplicacionBiblioteca.Servicios
{
    public static class ServicioCliente
    {
        public static void AgregarCliente(List<Cliente> clientes, Cliente cliente)
        {
            clientes.Add(cliente);
            Console.WriteLine("Cliente agregado correctamente.");
        }
        public static void EliminarCliente(List<Cliente> clientes, int id)
        {
            Cliente elementoAEliminar = clientes.Find(x => x.Identificador == id);
            if (elementoAEliminar != null)
            {
                clientes.Remove(elementoAEliminar);
                Console.WriteLine("Cliente eliminado correctamente.");
            }
            else
            {
                Console.WriteLine("Cliente no encontrado.");
            }
        }
        public static void ModificarClienteTelefono(List<Cliente> clientes, int id, string nuevoTelefono)
        {
            Cliente elementoAEditar = clientes.Find(x => x.Identificador == id);
            if (elementoAEditar != null)
            {
                elementoAEditar.Telefono = nuevoTelefono;
                Console.WriteLine("Telefono modificado correctamente.");
            }
            else
            {
                Console.WriteLine("Cliente no encontrado.");
            }
        }
        public static void ModificarClienteCorreo(List<Cliente> clientes, int id, string nuevoCorreo)
        {
            Cliente elementoAEditar = clientes.Find(x => x.Identificador == id);
            if (elementoAEditar != null)
            {
                elementoAEditar.Correo = nuevoCorreo;
                Console.WriteLine("Correo modificado correctamente.");
            }
            else
            {
                Console.WriteLine("Cliente no encontrado.");
            }
        }
        public static void MostrarClientes(List<Cliente> clientes)
        {
            Console.WriteLine("Lista de Clientes:");
            foreach (var item in clientes)
            {
                Console.WriteLine($"Id: {item.Identificador}, Nombre: {item.Nombre}, Documento: {item.NumeroDocumento}, Telefono: {item.Telefono}, Correo: {item.Correo}");
            }
        }
    }
}
