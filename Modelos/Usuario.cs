namespace PLANAZO_POO_CSharp.Modelos;

public class Usuario
{
    public int IdUsuario { get; private set; }
    public string Nombre { get; private set; }
    public string Correo { get; private set; }

    public Usuario(int idUsuario, string nombre, string correo)
    {
        if (idUsuario <= 0)
            throw new ArgumentException("El identificador del usuario debe ser mayor que cero.");
        if (string.IsNullOrWhiteSpace(nombre))
            throw new ArgumentException("El nombre no puede estar vacío.");
        if (string.IsNullOrWhiteSpace(correo) || !correo.Contains('@'))
            throw new ArgumentException("Ingrese un correo electrónico válido.");

        IdUsuario = idUsuario;
        Nombre = nombre.Trim();
        Correo = correo.Trim();
    }
}
