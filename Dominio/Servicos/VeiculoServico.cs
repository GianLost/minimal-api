using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using minimal_api.Dominio.Entidades;
using minimal_api.Dominio.Interfaces;
using minimal_api.Infraestrutura.Db;

namespace minimal_api.Dominio.Servicos
{
    public class VeiculoServico : IVeiculoServico
    {
        private readonly DbContexto _db;
        public VeiculoServico(DbContexto db)
        {
            _db = db;
        }

        public void Apagar(Veiculo veiculo)
        {
            _db.Veiculos.Remove(veiculo);
            _db.SaveChanges();
        }

        public void Atualizar(Veiculo veiculo)
        {
            _db.Veiculos.Update(veiculo);
            _db.SaveChanges();
        }

        public Veiculo BuscaPorId(int id)
        {
            return _db.Veiculos.Where(v => v.Id == id).FirstOrDefault();
        }

        public void Incluir(Veiculo veiculo)
        {
            _db.Veiculos.Add(veiculo);
            _db.SaveChanges();
        }

        public List<Veiculo> Todos(int? pagina = 1, string nome = null, string marca = null)
        {
            var query = _db.Veiculos.AsQueryable();

            if(!string.IsNullOrEmpty(nome))
            {
                query = query.Where(v => EF.Functions.Like(v.Nome.ToLower(), $"%{nome.ToLower()}%"));
            }

            int itensPorPagina = 10;

            if(pagina != null)
            {
                query = query.Skip(((int)pagina - 1) * itensPorPagina).Take(itensPorPagina);
            }

            return query.ToList();
        }
    }
}