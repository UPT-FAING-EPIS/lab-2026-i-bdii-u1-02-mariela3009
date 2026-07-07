using ClienteAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ClienteAPI.Data;

public class BdClientesContext : DbContext
{
    public BdClientesContext(DbContextOptions<BdClientesContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cliente> Clientes { get; set; }
    public virtual DbSet<ClienteDocumento> ClientesDocumentos { get; set; }
    public virtual DbSet<TipoDocumento> TiposDocumentos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.IdCliente).HasName("PK_CLIENTES");
            entity.ToTable("CLIENTES");
            entity.Property(e => e.IdCliente).HasColumnName("ID_CLIENTE");
            entity.Property(e => e.NomCliente).HasColumnName("NOM_CLIENTE").HasMaxLength(100).IsRequired();
        });

        modelBuilder.Entity<TipoDocumento>(entity =>
        {
            entity.HasKey(e => e.IdTipoDocumento).HasName("PK_TIPOS_DOCUMENTOS");
            entity.ToTable("TIPOS_DOCUMENTOS");
            entity.Property(e => e.IdTipoDocumento).HasColumnName("ID_TIPO_DOCUMENTO");
            entity.Property(e => e.DesTipoDocumento).HasColumnName("DES_TIPO_DOCUMENTO").HasMaxLength(50).IsRequired();
        });

        modelBuilder.Entity<ClienteDocumento>(entity =>
        {
            entity.HasKey(e => new { e.IdCliente, e.IdTipoDocumento }).HasName("PK_CLIENTES_DOCUMENTOS");
            entity.ToTable("CLIENTES_DOCUMENTOS");
            entity.Property(e => e.IdCliente).HasColumnName("ID_CLIENTE");
            entity.Property(e => e.IdTipoDocumento).HasColumnName("ID_TIPO_DOCUMENTO");
            entity.Property(e => e.NumDocumento).HasColumnName("NUM_DOCUMENTO").HasMaxLength(15).IsRequired();

            entity.HasOne(d => d.Cliente)
                .WithMany(p => p.ClientesDocumentos)
                .HasForeignKey(d => d.IdCliente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CLIENTES_DOCUMENTOS_X_CLIENTE");

            entity.HasOne(d => d.TipoDocumento)
                .WithMany(p => p.ClientesDocumentos)
                .HasForeignKey(d => d.IdTipoDocumento)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CLIENTES_DOCUMENTOS_X_TIPO_DOCUMENTO");
        });
    }
}
