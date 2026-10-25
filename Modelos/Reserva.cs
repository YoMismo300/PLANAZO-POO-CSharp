namespace PLANAZO_POO_CSharp.Modelos;

public enum EstadoReserva
{
    Activa,
    Cancelada
}

public class Reserva
{
    public int IdReserva { get; private set; }
    public Usuario Usuario { get; private set; }
    public Evento Evento { get; private set; }
    public int CantidadEntradas { get; private set; }
    public decimal ValorTotal { get; private set; }
    public EstadoReserva Estado { get; private set; }

    public Reserva(int idReserva, Usuario usuario, Evento evento, int cantidadEntradas)
    {
        if (idReserva <= 0)
            throw new ArgumentException("El identificador de reserva debe ser mayor que cero.");
        if (usuario == null)
            throw new ArgumentNullException(nameof(usuario));
        if (evento == null)
            throw new ArgumentNullException(nameof(evento));
        if (cantidadEntradas <= 0)
            throw new ArgumentException("La cantidad de entradas debe ser mayor que cero.");

        IdReserva = idReserva;
        Usuario = usuario;
        Evento = evento;
        CantidadEntradas = cantidadEntradas;
        ValorTotal = evento.CalcularPrecioFinal() * cantidadEntradas;
        Estado = EstadoReserva.Activa;
    }

    public void Cancelar()
    {
        if (Estado == EstadoReserva.Cancelada)
            throw new InvalidOperationException("Esta reserva ya fue cancelada.");

        Evento.DevolverCupos(CantidadEntradas);
        Estado = EstadoReserva.Cancelada;
    }

    public void MostrarInformacion()
    {
        Console.WriteLine($"Reserva #{IdReserva} | Estado: {Estado}");
        Console.WriteLine($"Usuario: {Usuario.Nombre} ({Usuario.Correo})");
        Console.WriteLine($"Evento: {Evento.Nombre} | Entradas: {CantidadEntradas}");
        Console.WriteLine($"Valor total: {ValorTotal:C0}");
    }
}
