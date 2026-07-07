namespace ClienteAPI.Models;

public class ClienteDocumento
{
    public int IdCliente { get; set; }
    public byte IdTipoDocumento { get; set; }
    public string NumDocumento { get; set; } = string.Empty;

    public Cliente? Cliente { get; set; }
    public TipoDocumento? TipoDocumento { get; set; }
}
