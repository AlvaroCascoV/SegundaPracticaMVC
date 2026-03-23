using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PracticaMvcCore2ACV.Data;
using PracticaMvcCore2ACV.Models;
using System.Drawing;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PracticaMvcCore2ACV.Repositories
{
    public class RepositoryLibros
    {
        private LibrosContext context;
        public RepositoryLibros(LibrosContext context)
        {
            this.context = context;
        }

        public async Task<List<Libros>> GetLibrosAsync()
        {
            return await this.context.Libros.ToListAsync();
        }

        public async Task<List<Generos>> GetGenerosAsync()
        {
            return await this.context.Generos.ToListAsync();
        }

        public async Task<Libros> FindLibroAsync(int idLibro)
        {
            Libros libro = await this.context.Libros.FirstOrDefaultAsync(x => x.IdLibro == idLibro);
            return libro;
        }

        public async Task<List<Libros>> GetLibrosGeneroAsync(int idGenero)
        {
            return await this.context.Libros.Where(x => x.IdGenero == idGenero).ToListAsync();
        }

        public async Task<List<Libros>> GetLibrosCarritoAsync(List<int> idsLibros)
        {
            var consulta = from datos in this.context.Libros
                           where idsLibros.Contains(datos.IdLibro)
                           select datos;
            return await consulta.ToListAsync();
        }

        public async Task<Usuarios> LogInUsuarioAsync(string email, string password)
        {
            Usuarios usuario = await this.context.Usuarios.Where(x => x.Email == email && x.Pass == password).FirstOrDefaultAsync();
            return usuario;
        }

        public async Task<List<VistaPedidos>> VerPedidosAsync(int idUser)
        {
            List<VistaPedidos> pedidos = await this.context.VistasPedidos.Where(z => z.IdUsuario == idUser).ToListAsync();
            return pedidos;
        }

        public async Task InsertPedido(List<int> idLibros, int idUsuario)
        {
            int maxPedidoId = await this.context.Pedidos
                .DefaultIfEmpty()
                .MaxAsync(p => (int?)p.IdPedido) ?? 0;

            int nuevoPedidoId = maxPedidoId + 1;
            DateTime fecha = DateTime.Now;

            foreach (int idLibro in idLibros)
            {
                Pedido pedido = new Pedido
                {
                    IdPedido = nuevoPedidoId,
                    IdFactura = 1,
                    Fecha = fecha,
                    IdLibro = idLibro,
                    IdUsuario = idUsuario,
                    Cantidad = 1
                };
                this.context.Pedidos.Add(pedido);
            }

            await this.context.SaveChangesAsync();
        }
    }
}
