using ClienteAPI.Data;
using ClienteAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClienteAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClientesDocumentosController : ControllerBase
{
    private readonly BdClientesContext _context;

    public ClientesDocumentosController(BdClientesContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ClienteDocumento>>> GetClientesDocumentos()
    {
        return await _context.ClientesDocumentos.ToListAsync();
    }

    [HttpGet("{idCliente}/{idTipoDocumento}")]
    public async Task<ActionResult<ClienteDocumento>> GetClienteDocumento(int idCliente, byte idTipoDocumento)
    {
        var clienteDocumento = await _context.ClientesDocumentos.FindAsync(idCliente, idTipoDocumento);
        if (clienteDocumento == null)
        {
            return NotFound();
        }

        return clienteDocumento;
    }

    [HttpPost]
    public async Task<ActionResult<ClienteDocumento>> PostClienteDocumento(ClienteDocumento clienteDocumento)
    {
        _context.ClientesDocumentos.Add(clienteDocumento);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetClienteDocumento), new { idCliente = clienteDocumento.IdCliente, idTipoDocumento = clienteDocumento.IdTipoDocumento }, clienteDocumento);
    }

    [HttpPut("{idCliente}/{idTipoDocumento}")]
    public async Task<IActionResult> PutClienteDocumento(int idCliente, byte idTipoDocumento, ClienteDocumento clienteDocumento)
    {
        if (idCliente != clienteDocumento.IdCliente || idTipoDocumento != clienteDocumento.IdTipoDocumento)
        {
            return BadRequest();
        }

        _context.Entry(clienteDocumento).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ClienteDocumentoExists(idCliente, idTipoDocumento))
            {
                return NotFound();
            }

            throw;
        }

        return NoContent();
    }

    [HttpDelete("{idCliente}/{idTipoDocumento}")]
    public async Task<IActionResult> DeleteClienteDocumento(int idCliente, byte idTipoDocumento)
    {
        var clienteDocumento = await _context.ClientesDocumentos.FindAsync(idCliente, idTipoDocumento);
        if (clienteDocumento == null)
        {
            return NotFound();
        }

        _context.ClientesDocumentos.Remove(clienteDocumento);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool ClienteDocumentoExists(int idCliente, byte idTipoDocumento)
    {
        return _context.ClientesDocumentos.Any(e => e.IdCliente == idCliente && e.IdTipoDocumento == idTipoDocumento);
    }
}
