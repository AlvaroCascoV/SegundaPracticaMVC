using Microsoft.EntityFrameworkCore;
using PracticaMvcCore2ACV.Data;
using PracticaMvcCore2ACV.Models;

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

        public async Task<Usuarios> LogInUsuarioAsync(string email, string password)
        {
            Usuarios usuario = await this.context.Usuarios.Where(x => x.Email == email && x.Pass == password).FirstOrDefaultAsync();
            return usuario;
        }
    }
}
