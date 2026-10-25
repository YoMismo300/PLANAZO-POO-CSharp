namespace PLANAZO_POO_CSharp.Modelos;

public class EventoDeportivo : Evento
{
    public EventoDeportivo(int identificador, string nombre, DateTime fecha, decimal precioBase, int capacidadMaxima)
        : base(identificador, nombre, fecha, precioBase, capacidadMaxima)
    {
    }

    public override decimal CalcularPrecioFinal()
    {
        // Reto adicional: los eventos deportivos aumentan un 20 % el precio base.
        return PrecioBase * 1.20m;
    }
}
