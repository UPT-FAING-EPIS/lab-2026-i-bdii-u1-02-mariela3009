namespace ClienteAPI.Models;

public class Cliente
{
    public int IdCliente { get; set; }
    public string NomCliente { get; set; } = string.Empty;
    public ICollection<ClienteDocumento> ClientesDocumentos { get; set; } = new List<ClienteDocumento>();
}
