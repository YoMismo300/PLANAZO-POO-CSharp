using PLANAZO_POO_CSharp.Modelos;
using PLANAZO_POO_CSharp.Servicios;

namespace PLANAZO_POO_CSharp;

internal class Program
{
    private static readonly GestorReservas gestor = new();

    private static void Main()
    {
        CargarEventosIniciales();

        int opcion;
        do
        {
            MostrarMenu();
            opcion = LeerEntero("Seleccione una opción: ");

            try
            {
                switch (opcion)
                {
                    case 1:
                        gestor.MostrarEventosDisponibles();
                        break;
                    case 2:
                        ConsultarEvento();
                        break;
                    case 3:
                        RealizarReserva();
                        break;
                    case 4:
                        gestor.MostrarReservas();
                        break;
                    case 5:
                        CancelarReserva();
                        break;
                    case 6:
                        gestor.MostrarResumen();
                        break;
                    case 7:
                        ConsultarReservasUsuario();
                        break;
                    case 0:
                        Console.WriteLine("Gracias por usar PLANAZO.");
                        break;
                    default:
                        Console.WriteLine("Opción no válida. Intente nuevamente.");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"No se pudo completar la operación: {ex.Message}");
            }

            if (opcion != 0)
            {
                Console.WriteLine("\nPresione una tecla para continuar...");
                Console.ReadKey();
                Console.Clear();
            }

        } while (opcion != 0);
    }

    private static void CargarEventosIniciales()
    {
        gestor.AgregarEvento(new Cine(1, "Cómo entrenar a tu dragón", new DateTime(2026, 11, 10), 18000m, 80));
        gestor.AgregarEvento(new Cine(2, "Robot salvaje", new DateTime(2026, 11, 12), 16000m, 60));
        gestor.AgregarEvento(new Concierto(3, "Noche de Rock", new DateTime(2026, 11, 15), 50000m, 120));
        gestor.AgregarEvento(new Concierto(4, "Festival de Música", new DateTime(2026, 11, 20), 75000m, 200));
        gestor.AgregarEvento(new Taller(5, "Taller de fotografía", new DateTime(2026, 11, 18), 40000m, 25));
        gestor.AgregarEvento(new Taller(6, "Pintura para principiantes", new DateTime(2026, 11, 22), 30000m, 20));
        gestor.AgregarEvento(new EventoDeportivo(7, "Torneo de fútbol", new DateTime(2026, 11, 25), 25000m, 100));
    }

    private static void MostrarMenu()
    {
        Console.WriteLine("==============================");
        Console.WriteLine("       PLANAZO - EVENTOS");
        Console.WriteLine("==============================");
        Console.WriteLine("1. Ver eventos disponibles");
        Console.WriteLine("2. Consultar evento");
        Console.WriteLine("3. Reservar entradas");
        Console.WriteLine("4. Ver reservas realizadas");
        Console.WriteLine("5. Cancelar una reserva");
        Console.WriteLine("6. Ver resumen de reservas");
        Console.WriteLine("7. Consultar reservas por usuario");
        Console.WriteLine("0. Salir");
        Console.WriteLine();
    }

    private static void ConsultarEvento()
    {
        int id = LeerEntero("Ingrese el identificador del evento: ");
        gestor.ConsultarEvento(id);
    }

    private static void RealizarReserva()
    {
        Console.Write("Nombre de la persona: ");
        string nombre = Console.ReadLine() ?? "";
        Console.Write("Correo electrónico: ");
        string correo = Console.ReadLine() ?? "";

        int idEvento = LeerEntero("Identificador del evento: ");
        int cantidad = LeerEntero("Cantidad de entradas: ");

        Reserva reserva = gestor.RealizarReserva(nombre, correo, idEvento, cantidad);
        Console.WriteLine("\nReserva realizada correctamente.");
        Console.WriteLine($"Código de reserva: {reserva.IdReserva}");
        Console.WriteLine($"Evento: {reserva.Evento.Nombre}");
        Console.WriteLine($"Entradas: {reserva.CantidadEntradas}");
        Console.WriteLine($"Valor total: {reserva.ValorTotal:C0}");
    }

    private static void CancelarReserva()
    {
        int id = LeerEntero("Ingrese el identificador de la reserva: ");
        gestor.CancelarReserva(id);
        Console.WriteLine("La reserva fue cancelada y los cupos fueron devueltos.");
    }

    private static void ConsultarReservasUsuario()
    {
        Console.Write("Ingrese el correo del usuario: ");
        string correo = Console.ReadLine() ?? "";
        gestor.MostrarReservasUsuario(correo);
    }

    private static int LeerEntero(string mensaje)
    {
        Console.Write(mensaje);
        string? entrada = Console.ReadLine();

        if (!int.TryParse(entrada, out int valor))
        {
            throw new ArgumentException("Debe ingresar un número entero válido.");
        }

        return valor;
    }
}
