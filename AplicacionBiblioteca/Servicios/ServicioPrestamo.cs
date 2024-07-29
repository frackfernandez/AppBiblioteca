using AplicacionBiblioteca.Dto;
using AplicacionBiblioteca.Recursos;

namespace AplicacionBiblioteca.Servicios
{
    public static class ServicioPrestamo
    {
        public static void AgregarPrestamo(List<Prestamo> prestamos, Prestamo prestamo)
        {
            prestamos.Add(prestamo);
            Console.WriteLine("Prestamo agregado correctamente.");
        }
        public static void EliminarPrestamo(List<Prestamo> prestamos, int id)
        {
            Prestamo elementoAEliminar = prestamos.Find(x => x.Identificador == id);
            if (elementoAEliminar != null)
            {
                prestamos.Remove(elementoAEliminar);
                Console.WriteLine("Prestamo eliminado correctamente.");
            }
            else
            {
                Console.WriteLine("Prestamo no encontrado.");
            }
        }
        public static void ModificarPrestamoFechaVencimiento(List<Prestamo> prestamos, int id, DateTime nuevoVencimiento)
        {
            Prestamo elementoAEditar = prestamos.Find(x => x.Identificador == id);
            if (elementoAEditar != null)
            {
                elementoAEditar.FechaVencimiento = nuevoVencimiento;
                Console.WriteLine("Fecha modificada exitosamente.");
            }
            else
            {
                Console.WriteLine("Prestamo no encontrado.");
            }
        }
        public static void MostrarPrestamos(List<Prestamo> prestamos, List<Cliente> clientes, List<Libro> libros)
        {
            Console.WriteLine("Lista de prestamos:");
            foreach (var item in prestamos)
            {
                Cliente elementoencontrado = clientes.Find(x => x.Identificador == item.ClienteSolicitante);
                if (elementoencontrado == null)
                {
                    Console.WriteLine($"Id: {item.Identificador}, Cliente:{item.ClienteSolicitante}. NO EXISTE, F. Solicitud: {item.FechaSolicitud.ToString("d")}, F. Vencimiento: {item.FechaVencimiento.ToString("d")}, Credito Total: {item.CreditoTotal}");
                }
                else
                {
                    Console.WriteLine($"Id: {item.Identificador}, Cliente:{item.ClienteSolicitante}. {elementoencontrado.Nombre}, F. Solicitud: {item.FechaSolicitud.ToString("d")}, F. Vencimiento: {item.FechaVencimiento.ToString("d")}, Credito Total: {item.CreditoTotal}");
                }
                Console.Write("Libros:\t\t");
                foreach (var item1 in item.Libros)
                {
                    Libro elementoencontrado2 = libros.Find(x => x.Identificador == item1);
                    if (elementoencontrado2 == null)
                    {
                        Console.Write($"NO EXISTE\t");
                    }
                    else
                    {
                        Console.Write($"{elementoencontrado2.Titulo}\t");
                    }
                }
                Console.WriteLine();
            }
        }
        public static void PagarPrestamo(List<Prestamo> prestamos, int id)
        {

            decimal pago;
            decimal tarifa = 0.0m;
            int creditos;
            int diasInicio;
            int diasVenc;
            string? entrada = "";

            creditos = prestamos[id - 1].CreditoTotal;

            var tiempoInicio = DateTime.Now - prestamos[id - 1].FechaSolicitud;
            diasInicio = tiempoInicio.Days;

            var tiempoVenc = DateTime.Now - prestamos[id - 1].FechaVencimiento;
            diasVenc = tiempoVenc.Days;

            //Console.WriteLine($"diasInicio {diasInicio}");
            //Console.WriteLine($"diasVenc {diasVenc}");

            if (diasInicio == 0)
            {
                tarifa = Constantes.pagoMismoDia;
            }
            else if (diasVenc <= 0)
            {
                tarifa = Constantes.pagoMenorIgual;
            }
            else if (diasVenc > 0 && diasVenc < 8)
            {
                tarifa = Constantes.pagoMayor1Semana;
            }
            else
            {
                tarifa = Constantes.pagoMayor1Mes;
            }

            pago = creditos * tarifa;

            Console.WriteLine();
            Console.WriteLine($"Creditos: {creditos}");
            Console.WriteLine($"Tarifa: {tarifa}");
            Console.WriteLine($"El pago total es de: {pago}$");
            Console.WriteLine();
            do
            {
                Console.WriteLine("Se realizo el pago?");
                Console.WriteLine("Ingrese Si/No");
                entrada = Console.ReadLine();

                if (entrada.ToLower() == "si")
                {
                    EliminarPrestamo(prestamos, id);
                    Console.WriteLine();
                    break;
                }

                if (entrada.ToLower() == "no")
                {
                    Console.WriteLine();
                    break;
                }
            } while (true);
        }
    }
}
