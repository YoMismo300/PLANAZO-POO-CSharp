namespace PLANAZO_POO_CSharp.Modelos;

public abstract class Evento
{
    public int Identificador { get; private set; }
    public string Nombre { get; private set; }
    public DateTime Fecha { get; private set; }
    public decimal PrecioBase { get; private set; }
    public int CapacidadMaxima { get; private set; }
    public int CuposDisponibles { get; private set; }

    protected Evento(int identificador, string nombre, DateTime fecha, decimal precioBase, int capacidadMaxima)
    {
        if (identificador <= 0)
            throw new ArgumentException("El identificador debe ser mayor que cero.");
        if (string.IsNullOrWhiteSpace(nombre))
            throw new ArgumentException("El nombre del evento no puede estar vacío.");
        if (precioBase < 0)
            throw new ArgumentException("El precio base no puede ser negativo.");
        if (capacidadMaxima <= 0)
            throw new ArgumentException("La capacidad máxima debe ser mayor que cero.");

        Identificador = identificador;
        Nombre = nombre;
        Fecha = fecha;
        PrecioBase = precioBase;
        CapacidadMaxima = capacidadMaxima;
        CuposDisponibles = capacidadMaxima;
    }

    public abstract decimal CalcularPrecioFinal();

    public void ReservarCupos(int cantidad)
    {
        if (cantidad <= 0)
            throw new ArgumentException("La cantidad de entradas debe ser mayor que cero.");
        if (cantidad > CuposDisponibles)
            throw new InvalidOperationException("No hay suficientes cupos disponibles.");

        CuposDisponibles -= cantidad;
    }

    public void DevolverCupos(int cantidad)
    {
        if (cantidad <= 0)
            throw new ArgumentException("La cantidad debe ser mayor que cero.");
        if (CuposDisponibles + cantidad > CapacidadMaxima)
            throw new InvalidOperationException("La devolución supera la capacidad máxima.");

        CuposDisponibles += cantidad;
    }

    public virtual void MostrarInformacion()
    {
        Console.WriteLine($"ID: {Identificador} | {Nombre}");
        Console.WriteLine($"Fecha: {Fecha:dd/MM/yyyy}");
        Console.WriteLine($"Precio base: {PrecioBase:C0} | Precio final: {CalcularPrecioFinal():C0}");
        Console.WriteLine($"Cupos: {CuposDisponibles}/{CapacidadMaxima}");
    }
}
