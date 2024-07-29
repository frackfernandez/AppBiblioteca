using AplicacionBiblioteca.Dto;
using AplicacionBiblioteca.Recursos;
using AplicacionBiblioteca.Servicios;

namespace AplicacionBiblioteca
{
    public static class Biblioteca
    {
        public static void Inicio()
        {
            List<Autor> autores = new List<Autor>();
            List<Cliente> clientes = new List<Cliente>();
            List<Libro> libros = new List<Libro>();
            List<Prestamo> prestamos = new List<Prestamo>();

            Precargar(libros, autores, clientes, prestamos);

            string subMenuSelection = "";
            string menuSelection = "";
            string? entrada;

            Console.WriteLine("Bienvenido");
            Console.WriteLine();
            do
            {
                Console.Clear();
                Console.WriteLine("MENU PRINCIPAL:");
                Console.WriteLine();
                Console.WriteLine("\t1. Prestamos");
                Console.WriteLine("\t2. Clientes");
                Console.WriteLine("\t3. Libros");
                Console.WriteLine("\t4. Autores");
                Console.WriteLine();
                Console.WriteLine("Ingrese su opcion elegida (o escriba Salir para salir del programa)");

                entrada = Console.ReadLine();
                if (entrada != null)
                {
                    menuSelection = entrada.ToLower();
                }

                Console.Clear();
                switch (menuSelection)
                {
                    // Prestamos
                    case "1":
                        do
                        {
                            empiezaprestamo:
                            //Console.Clear();
                            Console.WriteLine("PRESTAMOS");
                            Console.WriteLine();
                            Console.WriteLine("\t1. Agregar");
                            Console.WriteLine("\t2. Eliminar");
                            Console.WriteLine("\t3. Modificar fecha de vencimiento");
                            Console.WriteLine("\t4. Listar");
                            Console.WriteLine("\t5. Pagar Prestamo");
                            Console.WriteLine();
                            Console.WriteLine("Ingrese su opcion elegida (o escriba Salir para ir al menu principal)");
                            entrada = Console.ReadLine();
                            if (entrada != null)
                            {
                                subMenuSelection = entrada.ToLower();
                            }

                            switch (subMenuSelection)
                            {
                                case "1": // AGREGAR

                                    int idcliente= 0;
                                    int cant = 0;
                                    int[] librosprest = new int[4];                                    
                                    int idlibro;
                                    int creditotal = 0;
                                    DateTime fechafin;
                                    bool fechavalid = false;
                                    bool numbervalid = false;

                                    // id cliente
                                    Console.WriteLine();
                                    ServicioCliente.MostrarClientes(clientes);
                                    Console.WriteLine();
                                    do
                                    {                                        
                                        Console.WriteLine("Ingresa el Identificador del cliente");
                                        entrada = Console.ReadLine();
                                        numbervalid = int.TryParse(entrada, out idcliente);
                                        if (numbervalid)
                                        {
                                            Cliente elementoencontrado = clientes.Find(x => x.Identificador == idcliente);
                                            if (elementoencontrado == null)
                                            {
                                                Console.WriteLine("Cliente no encontrado.");
                                                Console.WriteLine();
                                                numbervalid = false;
                                            }
                                        }
                                    } while (numbervalid == false);

                                    // libros                                    
                                    do
                                    {
                                        Console.WriteLine("Ingresa la cantidad de libros (No debe ser mayor que 4)");
                                        entrada = Console.ReadLine();
                                        if (int.TryParse(entrada, out cant))
                                        {
                                            if (cant < 5)
                                            {
                                                Console.WriteLine();
                                                ServicioLibro.MostrarLibros(libros, autores);
                                                Console.WriteLine();
                                                for (int i = 0; i < cant; i++)
                                                {
                                                    do
                                                    {
                                                        Console.WriteLine($"Ingresa el Identificador del libro {i + 1}");
                                                        entrada = Console.ReadLine();                                                        
                                                        numbervalid = int.TryParse(entrada, out idlibro);
                                                        if (numbervalid)
                                                        {
                                                            Libro elementoencontrado = libros.Find(x => x.Identificador == idlibro);
                                                            if (elementoencontrado == null)
                                                            {
                                                                Console.WriteLine("Libro no encontrado");
                                                                numbervalid = false;
                                                            }                                                            
                                                            else
                                                            {
                                                                librosprest[i] = idlibro;
                                                            }                                                            
                                                        }
                                                    } while (numbervalid == false);
                                                }
                                                for (int i = 0; i < cant; i++)
                                                {
                                                    for (int j = 0; j < libros.LongCount(); j++)
                                                    {
                                                        if (libros[j].Identificador == librosprest[i])
                                                        {
                                                            creditotal += libros[j].Credito;

                                                            if (creditotal > Constantes.maxCreditos)
                                                            {
                                                                Console.WriteLine("No hay credito suficiente");
                                                                Console.WriteLine($"{creditotal} / {Constantes.maxCreditos}");
                                                                Console.WriteLine();
                                                                goto empiezaprestamo;
                                                            }
                                                        }
                                                    }
                                                }
                                                for (int i = 0; i < cant; i++)
                                                {
                                                    for (int j = 0; j < libros.LongCount(); j++)
                                                    {
                                                        if (libros[j].Identificador == librosprest[i])
                                                        {
                                                            if (libros[j].Stock < 1)
                                                            {
                                                                Console.WriteLine("No hay stock disponible");
                                                                Console.WriteLine();
                                                                goto empiezaprestamo;
                                                            }
                                                            else
                                                            {
                                                                libros[j].Stock--;
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            entrada = "";
                                        }
                                    } while (entrada == "" || cant > 4);

                                    int[] librossi = new int[cant];
                                    Array.Copy(librosprest, librossi, cant);
                                                                        
                                    // fecha de vencimiento
                                    do
                                    {
                                        Console.WriteLine("Ingrese la fecha de vencimiento");
                                        entrada = Console.ReadLine();
                                        fechavalid = DateTime.TryParse(entrada, out fechafin);
                                    }
                                    while (fechavalid == false);

                                    //fecha de inicio
                                    DateTime fechainicio = DateTime.Now;

                                    ServicioPrestamo.AgregarPrestamo(prestamos,new Prestamo(idcliente, librossi, fechainicio, fechafin, creditotal));
                                    Console.WriteLine();

                                    break;
                                case "2": // ELIMINAR

                                    int ideliminar = 0;
                                    bool validnumber = false;

                                    Console.WriteLine();
                                    ServicioPrestamo.MostrarPrestamos(prestamos, clientes, libros);
                                    do
                                    {
                                        Console.WriteLine("Ingrese el Identificador del prestamo a eliminar:");
                                        entrada = Console.ReadLine();
                                        validnumber = int.TryParse(entrada, out ideliminar);
                                        if (validnumber)
                                        {
                                            ServicioPrestamo.EliminarPrestamo(prestamos, ideliminar);
                                            Console.WriteLine();
                                        }
                                    } while (validnumber == false);
                                    break;
                                case "3": // MODIFICAR FECHA DE VENCIMIENTO

                                    int idmodificar = 0;
                                    bool validnumber2 = false;
                                    bool validfecha = false;
                                    DateTime nuevafecha;

                                    Console.WriteLine();
                                    ServicioPrestamo.MostrarPrestamos(prestamos, clientes, libros);
                                    do
                                    {
                                        Console.WriteLine("Ingrese el Identificador del prestamo a modificar:");
                                        entrada = Console.ReadLine();
                                        validnumber2 = int.TryParse(entrada, out idmodificar);
                                        Prestamo elementoencontrado = prestamos.Find(x => x.Identificador == idmodificar);
                                        if (elementoencontrado == null)
                                        {
                                            Console.WriteLine("Elemento no encontrado");
                                            Console.WriteLine();
                                            break;
                                        }
                                        if (validnumber2) 
                                        {
                                            do
                                            {                                                                                               
                                                Console.WriteLine();
                                                Console.WriteLine("Ingrese la nueva fecha de vencimiento:");
                                                entrada = Console.ReadLine();
                                                validfecha = DateTime.TryParse(entrada, out nuevafecha);
                                                if (validfecha)
                                                    ServicioPrestamo.ModificarPrestamoFechaVencimiento(prestamos, idmodificar, nuevafecha);

                                            } while (validfecha == false);                                       
                                        }
                                    } while (validnumber2 == false);
                                    break;
                                case "4": // LISTAR

                                    Console.WriteLine();
                                    ServicioPrestamo.MostrarPrestamos(prestamos, clientes, libros);
                                    Console.WriteLine();
                                    break;
                                case "5": // PAGAR PRESTAMO

                                    int idpagar;
                                    validnumber2 = false;
                                    
                                    Console.WriteLine();
                                    ServicioPrestamo.MostrarPrestamos(prestamos, clientes, libros);
                                    Console.WriteLine();
                                    do
                                    {
                                        Console.WriteLine("Ingrese el identificador del prestamo a pagar:");
                                        entrada = Console.ReadLine();
                                        validnumber2 = int.TryParse(entrada, out idpagar);                                        
                                        if (validnumber2)
                                        {
                                            Prestamo elementoencontrado = prestamos.Find(x => x.Identificador == idpagar);
                                            if (elementoencontrado == null)
                                            {
                                                Console.WriteLine("Prestamo no encontrado.");
                                                Console.WriteLine();
                                                break;
                                            }

                                            ServicioPrestamo.PagarPrestamo(prestamos, idpagar);

                                        }
                                    } while (validnumber2 == false);
                                    break;
                                default:
                                    break;
                            }

                        }
                        while (subMenuSelection != "salir");
                        break;
                    // Clientes
                    case "2":
                        do
                        {
                            //Console.Clear();
                            Console.WriteLine("CLIENTES");
                            Console.WriteLine();
                            Console.WriteLine("\t1. Agregar");
                            Console.WriteLine("\t2. Eliminar");
                            Console.WriteLine("\t3. Modificar Telefono");
                            Console.WriteLine("\t4. Modificar Correo");
                            Console.WriteLine("\t5. Listar");
                            Console.WriteLine();
                            Console.WriteLine("Ingrese su opcion elegida (o escriba Salir para ir al menu principal)");
                            entrada = Console.ReadLine();
                            if (entrada != null)
                            {
                                subMenuSelection = entrada.ToLower();
                            }

                            switch (subMenuSelection)
                            {
                                case "1": // AGREGAR

                                    string nombre = "";
                                    string nrodocumento = "";
                                    string telefono = "";
                                    string correo = "";
                                                                        
                                    Console.WriteLine();

                                    // nombre cliente
                                    do
                                    {
                                        Console.WriteLine("Ingresa el nombre del cliente");
                                        entrada = Console.ReadLine();
                                        if (entrada != null)
                                        {
                                            nombre = entrada;
                                        }
                                    } while (entrada == "");

                                    // numero documento
                                    do
                                    {
                                        Console.WriteLine("Ingresa el numero de documento");
                                        entrada = Console.ReadLine();
                                        if (entrada != null)
                                        {
                                            nrodocumento = entrada;
                                        }
                                        Cliente elementoencontrado = clientes.Find(x => x.NumeroDocumento == nrodocumento);
                                        if (elementoencontrado != null)
                                        {
                                            Console.WriteLine("Este numero de documento ya esta registrado");
                                            entrada = "";
                                        }
                                    } while (entrada == "");

                                    // telefono
                                    do
                                    {
                                        Console.WriteLine("Ingresa el numero de telefono");
                                        entrada = Console.ReadLine();
                                        if (entrada != null)
                                        {
                                            telefono = entrada;
                                        }                                        
                                    } while (entrada == "");

                                    // correo
                                    do
                                    {
                                        Console.WriteLine("Ingresa el correo");
                                        entrada = Console.ReadLine();
                                        if (entrada != null)
                                        {
                                            correo = entrada;
                                        }
                                        Cliente elementoencontrado = clientes.Find(x => x.Correo == correo);
                                        if (elementoencontrado != null)
                                        {
                                            Console.WriteLine("Este correo ya esta registrado");
                                            entrada = "";
                                        }
                                    } while (entrada == "");

                                    ServicioCliente.AgregarCliente(clientes, new Cliente(nombre, nrodocumento, telefono, correo));
                                    Console.WriteLine();
                                    break;
                                case "2": // ELIMINAR

                                    int ideliminar = 0;
                                    bool validnumber = false;

                                    Console.WriteLine();
                                    ServicioCliente.MostrarClientes(clientes);
                                    Console.WriteLine();
                                    do
                                    {
                                        Console.WriteLine("Ingrese el identificador del cliente a eliminar:");
                                        entrada = Console.ReadLine();
                                        validnumber = int.TryParse(entrada, out ideliminar);
                                        if (validnumber)
                                        {
                                            ServicioCliente.EliminarCliente(clientes, ideliminar);
                                            Console.WriteLine();
                                        }
                                    } while (validnumber == false);
                                    break;
                                case "3": // MODIFICAR TELEFONO

                                    int idmodificar;
                                    validnumber = false;
                                    string nuevotelf;

                                    Console.WriteLine();
                                    ServicioCliente.MostrarClientes(clientes);
                                    Console.WriteLine();
                                    do
                                    {
                                        Console.WriteLine("Ingrese el identificador del cliente a modificar:");
                                        entrada = Console.ReadLine();
                                        validnumber = int.TryParse(entrada, out idmodificar);
                                        
                                        if (validnumber)
                                        {
                                            Cliente elementoencontrado = clientes.Find(x => x.Identificador == idmodificar);
                                            if (elementoencontrado == null)
                                            {
                                                Console.WriteLine("Cliente no encontrado");
                                                Console.WriteLine();
                                                break;
                                            }
                                            do
                                            {
                                                Console.WriteLine("Ingrese el nuevo telefono:");
                                                entrada = Console.ReadLine();
                                                nuevotelf = entrada;

                                                ServicioCliente.ModificarClienteTelefono(clientes, idmodificar, nuevotelf);
                                                Console.WriteLine();

                                            } while (entrada == "");
                                        }
                                    } while (validnumber == false);
                                    break;
                                case "4": // MODIFICAR CORREO

                                    int idmodificarc;
                                    validnumber = false;
                                    string nuevocorreo;

                                    Console.WriteLine();
                                    ServicioCliente.MostrarClientes(clientes);
                                    Console.WriteLine();
                                    do
                                    {
                                        Console.WriteLine("Ingrese el identificador del cliente a modificar:");
                                        entrada = Console.ReadLine();
                                        validnumber = int.TryParse(entrada, out idmodificarc);                                        
                                        
                                        if (validnumber)
                                        {
                                            Cliente elementoencontrado = clientes.Find(x => x.Identificador == idmodificarc);
                                            if (elementoencontrado == null)
                                            {
                                                Console.WriteLine("Cliente no encontrado");
                                                Console.WriteLine();
                                                break;
                                            }
                                            do
                                            {
                                                Console.WriteLine("Ingrese el nuevo correo:");
                                                entrada = Console.ReadLine();
                                                nuevocorreo = entrada;

                                                ServicioCliente.ModificarClienteCorreo(clientes, idmodificarc, nuevocorreo);
                                                Console.WriteLine();

                                            } while (entrada == "");
                                        }
                                    } while (validnumber == false);
                                    break;
                                case "5": //LISTAR

                                    Console.WriteLine();
                                    ServicioCliente.MostrarClientes(clientes);
                                    Console.WriteLine();
                                    break;
                                default:
                                    break;
                            }

                        }
                        while (subMenuSelection != "salir");
                        break;
                    // Libros
                    case "3":
                        do
                        {
                            empiezalibro:
                            //Console.Clear();
                            Console.WriteLine("LIBROS");
                            Console.WriteLine();
                            Console.WriteLine("\t1. Agregar");
                            Console.WriteLine("\t2. Eliminar");
                            Console.WriteLine("\t3. Modificar Credito");
                            Console.WriteLine("\t4. Modificar Stock");
                            Console.WriteLine("\t5. Listar");
                            Console.WriteLine();
                            Console.WriteLine("Ingrese su opcion elegida (o escriba Salir para ir al menu principal)");
                            entrada = Console.ReadLine();
                            if (entrada != null)
                            {
                                subMenuSelection = entrada.ToLower();
                            }

                            switch (subMenuSelection)
                            {
                                case "1": // AGREGAR

                                    int idautor;
                                    string titulo = "";
                                    string categoria = "";
                                    int credito = 0;
                                    int stock = 0;
                                    bool numvalido = false;

                                    // autor
                                    Console.WriteLine();
                                    ServicioAutor.MostrarAutores(autores);
                                    Console.WriteLine();
                                    do
                                    {
                                        Console.WriteLine("Ingresa el Identificador del autor");
                                        entrada = Console.ReadLine();
                                        numvalido = int.TryParse(entrada, out idautor);
                                        if (numvalido)
                                        {
                                            Autor elementoencontrado = autores.Find(x => x.Identificador == idautor);
                                            if (elementoencontrado == null)
                                            {
                                                Console.WriteLine("Autor no encontrado.");
                                                Console.WriteLine();
                                                goto empiezalibro;
                                            }
                                        }
                                    } while (numvalido == false);

                                    // titulo
                                    do
                                    {
                                        Console.WriteLine("Ingresa el titulo");
                                        entrada = Console.ReadLine();
                                        if (entrada != null)
                                        {
                                            titulo = entrada;
                                        }
                                    } while (entrada == "");

                                    // categoria
                                    do
                                    {
                                        Console.WriteLine("Ingresa la categoria");
                                        entrada = Console.ReadLine();
                                        if (entrada != null)
                                        {
                                            categoria = entrada;
                                        }
                                    } while (entrada == "");

                                    // credito
                                    do
                                    {
                                        Console.WriteLine("Ingresa el credito");
                                        entrada = Console.ReadLine();
                                        numvalido = int.TryParse(entrada, out credito);
                                    } while (numvalido == false);

                                    // stock
                                    do
                                    {
                                        Console.WriteLine("Ingresa el stock");
                                        entrada = Console.ReadLine();
                                        numvalido = int.TryParse(entrada, out stock);
                                    } while (numvalido == false);

                                    ServicioLibro.AgregarLibro(libros, new Libro(idautor, titulo, categoria, credito, stock));
                                    Console.WriteLine();

                                    break;
                                case "2": // ELIMINAR

                                    int ideliminar = 0;
                                    bool validnumber = false;

                                    Console.WriteLine();
                                    ServicioLibro.MostrarLibros(libros, autores);
                                    Console.WriteLine();
                                    do
                                    {
                                        Console.WriteLine("Ingrese el Identificador del Libro a eliminar:");
                                        entrada = Console.ReadLine();
                                        validnumber = int.TryParse(entrada, out ideliminar);
                                        if (validnumber)
                                        {
                                            ServicioLibro.EliminarLibro(libros, ideliminar);
                                            Console.WriteLine();
                                        }
                                    } while (validnumber == false);
                                    break;
                                case "3": // MODIFICAR CREDITO

                                    int idmodificar;
                                    validnumber = false;
                                    int nuevocredito;

                                    Console.WriteLine();
                                    ServicioLibro.MostrarLibros(libros, autores);
                                    Console.WriteLine();
                                    do
                                    {
                                        Console.WriteLine("Ingrese el identificador del libro a modificar:");
                                        entrada = Console.ReadLine();
                                        validnumber = int.TryParse(entrada, out idmodificar);

                                        if (validnumber)
                                        {
                                            Libro elementoencontrado = libros.Find(x => x.Identificador == idmodificar);
                                            if (elementoencontrado == null)
                                            {
                                                Console.WriteLine("Libro no encontrado");
                                                Console.WriteLine();
                                                break;
                                            }
                                            do
                                            {
                                                Console.WriteLine("Ingrese el nuevo credito:");
                                                entrada = Console.ReadLine();
                                                validnumber = int.TryParse(entrada,out nuevocredito);
                                                if (validnumber)
                                                {
                                                    ServicioLibro.ModificarLibroCredito(libros, idmodificar, nuevocredito);
                                                    Console.WriteLine();
                                                }
                                            } while (validnumber == false);
                                        }
                                    } while (validnumber == false);
                                    break;
                                case "4": // MODIFICAR STOCK

                                    int idmodificars;
                                    validnumber = false;
                                    int nuevostock;

                                    Console.WriteLine();
                                    ServicioLibro.MostrarLibros(libros, autores);
                                    Console.WriteLine();
                                    do
                                    {
                                        Console.WriteLine("Ingrese el identificador del libro a modificar:");
                                        entrada = Console.ReadLine();
                                        validnumber = int.TryParse(entrada, out idmodificars);

                                        if (validnumber)
                                        {
                                            Libro elementoencontrado = libros.Find(x => x.Identificador == idmodificars);
                                            if (elementoencontrado == null)
                                            {
                                                Console.WriteLine("Libro no encontrado");
                                                Console.WriteLine();
                                                break;
                                            }
                                            do
                                            {
                                                Console.WriteLine("Ingrese el nuevo stock:");
                                                entrada = Console.ReadLine();
                                                validnumber = int.TryParse(entrada, out nuevostock);
                                                if (validnumber)
                                                {
                                                    ServicioLibro.ModificarLibroStock(libros, idmodificars, nuevostock);
                                                    Console.WriteLine();
                                                }
                                            } while (validnumber == false);
                                        }
                                    } while (validnumber == false);
                                    break;
                                case "5": // LISTAR

                                    Console.WriteLine();
                                    ServicioLibro.MostrarLibros(libros, autores);
                                    Console.WriteLine();
                                    break;
                                default:
                                    break;
                            }

                        }
                        while (subMenuSelection != "salir");
                        break;
                    // Autores
                    case "4":
                        do
                        {
                            //Console.Clear();
                            Console.WriteLine("AUTORES");
                            Console.WriteLine();
                            Console.WriteLine("\t1. Agregar");
                            Console.WriteLine("\t2. Eliminar");
                            Console.WriteLine("\t3. Modificar Nombre");
                            Console.WriteLine("\t4. Listar");
                            Console.WriteLine();
                            Console.WriteLine("Ingrese su opcion elegida (o escriba Salir para ir al menu principal)");
                            entrada = Console.ReadLine();
                            if (entrada != null)
                            {
                                subMenuSelection = entrada.ToLower();
                            }

                            switch (subMenuSelection)
                            {
                                case "1": // AGREGAR

                                    string autor = "";

                                    Console.WriteLine();

                                    // nombre autor                                    
                                    do
                                    {
                                        Console.WriteLine("Ingresa el nombre del autor");
                                        entrada = Console.ReadLine();
                                        if (entrada != null)
                                        {
                                            autor = entrada;
                                        }
                                    } while (entrada == "");

                                    ServicioAutor.AgregarAutor(autores, new Autor(autor));

                                    Console.WriteLine();
                                    break;
                                case "2": // ELIMINAR

                                    int ideliminar = 0;
                                    bool validnumber = false;
                                    
                                    Console.WriteLine();
                                    ServicioAutor.MostrarAutores(autores);
                                    Console.WriteLine();
                                    do
                                    {                                        
                                        Console.WriteLine("Ingrese el identificador del autor a eliminar:");
                                        entrada = Console.ReadLine();
                                        validnumber = int.TryParse(entrada, out ideliminar);
                                        if (validnumber)
                                        {
                                            ServicioAutor.EliminarAutor(autores, ideliminar);
                                            Console.WriteLine();
                                        }
                                    } while (validnumber == false);
                                    break;
                                case "3": // MODIFICAR

                                    int idmodificar;
                                    bool validnumber2 = false;
                                    string nuevonombre;

                                    Console.WriteLine();
                                    ServicioAutor.MostrarAutores(autores);
                                    Console.WriteLine();
                                    do
                                    {
                                        Console.WriteLine("Ingrese el identificador del autor a modificar:");
                                        entrada = Console.ReadLine();
                                        validnumber2 = int.TryParse(entrada, out idmodificar);

                                        if (validnumber2)
                                        {
                                            Autor elementoencontrado = autores.Find(x => x.Identificador == idmodificar);
                                            if (elementoencontrado == null)
                                            {
                                                Console.WriteLine("Autor no encontrado.");
                                                Console.WriteLine();
                                                break;
                                            }
                                            do
                                            {
                                                Console.WriteLine("Ingrese el nuevo nombre:");
                                                entrada = Console.ReadLine();
                                                nuevonombre = entrada;

                                                ServicioAutor.ModificarAutor(autores, idmodificar, nuevonombre);
                                                Console.WriteLine();

                                            } while (entrada == "");
                                        }
                                    } while (validnumber2 == false);
                                    break;
                                case "4": // LISTAR

                                    Console.WriteLine();
                                    ServicioAutor.MostrarAutores(autores);
                                    Console.WriteLine();
                                    break;
                                default:
                                    break;
                            }

                        }
                        while (subMenuSelection != "salir");
                        break;
                    default:
                        break;
                }

            }
            while (menuSelection != "salir");
        }
        public static void Precargar(List<Libro> libros, List<Autor> autores, List<Cliente> clientes, List<Prestamo> prestamos)
        {
            ServicioLibro.AgregarLibro(libros, new Libro(1, "El Alquimista", "Novela", 30, 3));
            ServicioLibro.AgregarLibro(libros, new Libro(2, "El Principito", "Novela", 30, 0));
            ServicioLibro.AgregarLibro(libros, new Libro(3, "Paco Yunque", "Cuento", 30, 1));
            ServicioLibro.AgregarLibro(libros, new Libro(4, "Odisea", "Epopeya", 50, 2));
            ServicioLibro.AgregarLibro(libros, new Libro(5, "Orgullo y Prejuicio", "Novela", 50, 2));

            ServicioAutor.AgregarAutor(autores, new Autor("Paulo Coelho"));
            ServicioAutor.AgregarAutor(autores, new Autor("Antoine de Saint-Exupéry"));
            ServicioAutor.AgregarAutor(autores, new Autor("Cesar Vallejo"));
            ServicioAutor.AgregarAutor(autores, new Autor("Homero"));
            ServicioAutor.AgregarAutor(autores, new Autor("Jane Austen"));

            ServicioCliente.AgregarCliente(clientes, new Cliente("Frack", "70252819", "910905613", "frackshanto@gmail.com"));
            ServicioCliente.AgregarCliente(clientes, new Cliente("Pepe", "99999999", "900900900", "pepito123@gmail.com"));
            ServicioCliente.AgregarCliente(clientes, new Cliente("Andres", "88888888", "900100200", "Andres456@gmail.com"));
            ServicioCliente.AgregarCliente(clientes, new Cliente("William", "77777777", "999000000", "willy2000@gmail.com"));

            int[] librosprest = { 1, 2};
            ServicioPrestamo.AgregarPrestamo(prestamos, new Prestamo(1, librosprest , new DateTime(2024, 02, 21), new DateTime(2024, 02, 22), 60));
            int[] librosprest2 = { 4 };
            ServicioPrestamo.AgregarPrestamo(prestamos, new Prestamo(3, librosprest2, new DateTime(2024, 02, 10), new DateTime(2024, 02, 21), 50));
            int[] librosprest3 = { 3, 5 };
            ServicioPrestamo.AgregarPrestamo(prestamos, new Prestamo(2, librosprest3, new DateTime(2024, 02, 10), new DateTime(2024, 02, 14), 80));
            int[] librosprest4 = { 3 };
            ServicioPrestamo.AgregarPrestamo(prestamos, new Prestamo(4, librosprest4, new DateTime(2024, 02, 10), new DateTime(2024, 02, 13), 30));

        }
    }
}
