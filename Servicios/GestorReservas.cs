using PLANAZO_POO_CSharp.Modelos;

namespace PLANAZO_POO_CSharp.Servicios;

public class GestorReservas
{
    private readonly List<Evento> eventos = new();
    private readonly List<Reserva> reservas = new();
    private readonly List<Usuario> usuarios = new();
    private int siguienteReserva = 1;
    private int siguienteUsuario = 1;

    public void AgregarEvento(Evento evento)
    {
        if (evento == null)
            throw new ArgumentNullException(nameof(evento));
        if (eventos.Any(e => e.Identificador == evento.Identificador))
            throw new InvalidOperationException("Ya existe un evento con ese identificador.");

        eventos.Add(evento);
    }

    public void MostrarEventosDisponibles()
    {
        Console.WriteLine("\n--- EVENTOS DISPONIBLES ---");
        foreach (Evento evento in eventos)
        {
            evento.MostrarInformacion();
            Console.WriteLine("--------------------------------");
        }
    }

    public void ConsultarEvento(int id)
    {
        Evento? evento = eventos.FirstOrDefault(e => e.Identificador == id);
        if (evento == null)
            throw new KeyNotFoundException("No existe un evento con ese identificador.");

        evento.MostrarInformacion();
    }

    public Reserva RealizarReserva(string nombre, string correo, int idEvento, int cantidad)
    {
        if (cantidad <= 0)
            throw new ArgumentException("No se puede reservar una cantidad negativa o igual a cero.");

        Evento? evento = eventos.FirstOrDefault(e => e.Identificador == idEvento);
        if (evento == null)
            throw new KeyNotFoundException("No existe un evento con ese identificador.");

        Usuario usuario = ObtenerOCrearUsuario(nombre, correo);

        // Primero se validan y descuentan los cupos; solo después se guarda la reserva.
        evento.ReservarCupos(cantidad);
        Reserva reserva = new(siguienteReserva++, usuario, evento, cantidad);
        reservas.Add(reserva);
        return reserva;
    }

    public void CancelarReserva(int idReserva)
    {
        Reserva? reserva = reservas.FirstOrDefault(r => r.IdReserva == idReserva);
        if (reserva == null)
            throw new KeyNotFoundException("No existe una reserva con ese identificador.");

        reserva.Cancelar();
    }

    public void MostrarReservas()
    {
        Console.WriteLine("\n--- RESERVAS REALIZADAS ---");
        if (reservas.Count == 0)
        {
            Console.WriteLine("Todavía no hay reservas.");
            return;
        }

        foreach (Reserva reserva in reservas)
        {
            reserva.MostrarInformacion();
            Console.WriteLine("--------------------------------");
        }
    }

    public void MostrarResumen()
    {
        List<Reserva> activas = reservas.Where(r => r.Estado == EstadoReserva.Activa).ToList();
        int entradas = activas.Sum(r => r.CantidadEntradas);
        decimal total = activas.Sum(r => r.ValorTotal);

        Console.WriteLine("\n--- RESUMEN DE RESERVAS ACTIVAS ---");
        Console.WriteLine($"Cantidad de reservas activas: {activas.Count}");
        Console.WriteLine($"Cantidad de entradas reservadas: {entradas}");
        Console.WriteLine($"Valor total de reservas activas: {total:C0}");
    }

    public void MostrarReservasUsuario(string correo)
    {
        if (string.IsNullOrWhiteSpace(correo))
            throw new ArgumentException("Debe ingresar el correo del usuario.");

        List<Reserva> delUsuario = reservas
            .Where(r => r.Usuario.Correo.Equals(correo.Trim(), StringComparison.OrdinalIgnoreCase))
            .ToList();

        if (delUsuario.Count == 0)
        {
            Console.WriteLine("No se encontraron reservas para ese correo.");
            return;
        }

        foreach (Reserva reserva in delUsuario)
        {
            reserva.MostrarInformacion();
            Console.WriteLine("--------------------------------");
        }
    }

    private Usuario ObtenerOCrearUsuario(string nombre, string correo)
    {
        if (string.IsNullOrWhiteSpace(nombre))
            throw new ArgumentException("El nombre de la persona no puede estar vacío.");
        if (string.IsNullOrWhiteSpace(correo) || !correo.Contains('@'))
            throw new ArgumentException("Ingrese un correo electrónico válido.");

        Usuario? usuario = usuarios.FirstOrDefault(
            u => u.Correo.Equals(correo.Trim(), StringComparison.OrdinalIgnoreCase));

        if (usuario != null)
            return usuario;

        usuario = new Usuario(siguienteUsuario++, nombre, correo);
        usuarios.Add(usuario);
        return usuario;
    }
}
