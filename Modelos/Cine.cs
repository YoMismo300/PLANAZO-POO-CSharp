namespace PLANAZO_POO_CSharp.Modelos;

public class Cine : Evento
{
    public Cine(int identificador, string nombre, DateTime fecha, decimal precioBase, int capacidadMaxima)
        : base(identificador, nombre, fecha, precioBase, capacidadMaxima)
    {
    }

    public override decimal CalcularPrecioFinal()
    {
        return PrecioBase;
    }
}
