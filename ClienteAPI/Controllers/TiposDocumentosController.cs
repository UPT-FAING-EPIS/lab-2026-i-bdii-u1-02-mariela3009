using ClienteAPI.Data;
using ClienteAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClienteAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TiposDocumentosController : ControllerBase
{
    private readonly BdClientesContext _context;

    public TiposDocumentosController(BdClientesContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TipoDocumento>>> GetTiposDocumentos()
    {
        return await _context.TiposDocumentos.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TipoDocumento>> GetTipoDocumento(byte id)
    {
        var tipoDocumento = await _context.TiposDocumentos.FindAsync(id);
        if (tipoDocumento == null)
        {
            return NotFound();
        }

        return tipoDocumento;
    }

    [HttpPost]
    public async Task<ActionResult<TipoDocumento>> PostTipoDocumento(TipoDocumento tipoDocumento)
    {
        _context.TiposDocumentos.Add(tipoDocumento);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetTipoDocumento), new { id = tipoDocumento.IdTipoDocumento }, tipoDocumento);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutTipoDocumento(byte id, TipoDocumento tipoDocumento)
    {
        if (id != tipoDocumento.IdTipoDocumento)
        {
            return BadRequest();
        }

        _context.Entry(tipoDocumento).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!TipoDocumentoExists(id))
            {
                return NotFound();
            }

            throw;
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTipoDocumento(byte id)
    {
        var tipoDocumento = await _context.TiposDocumentos.FindAsync(id);
        if (tipoDocumento == null)
        {
            return NotFound();
        }

        _context.TiposDocumentos.Remove(tipoDocumento);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool TipoDocumentoExists(byte id)
    {
        return _context.TiposDocumentos.Any(e => e.IdTipoDocumento == id);
    }
}
