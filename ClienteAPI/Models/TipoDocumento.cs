namespace ClienteAPI.Models;

public class TipoDocumento
{
    public byte IdTipoDocumento { get; set; }
    public string DesTipoDocumento { get; set; } = string.Empty;
    public ICollection<ClienteDocumento> ClientesDocumentos { get; set; } = new List<ClienteDocumento>();
}
