using minimal_api.Dominio.DTOs;
using minimal_api.Dominio.Entidades;
using minimal_api.Dominio.Interfaces;
using minimal_api.Infraestrutura.Db;

namespace minimal_api.Dominio.Servicos
{
    public class AdministradorServico : IAdministradorServico
    {
        private readonly DbContexto _db;
        public AdministradorServico(DbContexto db)
        {
            _db = db;
        }

        public Administrador Incluir(Administrador administrador)
        {
            _db.Administradores.Add(administrador);
            _db.SaveChanges();

            return administrador;
        }

        public Administrador BuscaPorId(int id)
        {
            return _db.Administradores.Where(v => v.Id == id).FirstOrDefault();
        }

        public List<Administrador> Todos(int? pagina)
        {
            var query = _db.Administradores.AsQueryable();

            int itensPorPagina = 10;

            if(pagina != null)
            {
                query = query.Skip(((int)pagina - 1) * itensPorPagina).Take(itensPorPagina);
            }

            return query.ToList();
        }

        public Administrador Login(LoginDTO loginDTO)
        {
            var adm = _db.Administradores.Where(a => a.Email == loginDTO.Email && a.Senha == loginDTO.Senha).FirstOrDefault();
            return adm;
        }
    }
}