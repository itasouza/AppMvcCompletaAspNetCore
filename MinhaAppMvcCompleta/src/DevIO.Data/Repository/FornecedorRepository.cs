using DevIO.Business.Interfaces;
using DevIO.Business.Models;
using DevIO.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DevIO.Data.Repository
{
    public class FornecedorRepository : RepositoryGenerico<Fornecedor>, IFornecedorRepository
    {
        public FornecedorRepository(MeuDbContext context) : base(context) { }


        public async Task<Fornecedor> ObterFornecedorEndereco(Guid id)
        {
            return await Db.Fornecedores.AsNoTracking()
                .Include(c => c.Endereco)
                .FirstOrDefaultAsync(f => f.Id == id);
        }

        public async Task<Fornecedor> ObterFornecedorProdutosEndereco(Guid id)
        {
            return await Db.Fornecedores.AsNoTracking()
                   .Include(c => c.Produtos)
                   .Include(c => c.Endereco)
                   .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<Fornecedor>> ObterParaAutocomplete(string text)
        {
            return await Db.Fornecedores.AsNoTracking()
                .Where(x => x.Nome.Contains(text))
                .OrderBy(p => p.Nome)
                .ToListAsync();
        }


        public async Task<Fornecedor> ObterParaAutocomplete(Guid id)
        {
            return await Db.Fornecedores.AsNoTracking()
                .FirstOrDefaultAsync(f => f.Id == id);
        }

        //public async Task<IEnumerable<Fornecedor>> ObterParaAutocomplete(string text)
        //{
        //    return await Db.Fornecedores.AsNoTracking()
        //                                .Where(x => x.Nome.Contains(text))
        //                                .OrderBy(p => p.Nome)
        //                                .Select(c => new FornecedorConsulta { Id = c.Id, Nome = c.Nome})
        //                                .ToListAsync();

        //}

    }
}
