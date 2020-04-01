﻿using DevIO.Business.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DevIO.Business.Interfaces
{
    public interface IFornecedorRepository :IRepositoryGenerico<Fornecedor>
    {
        Task<Fornecedor> ObterFornecedorEndereco(Guid id);
        Task<Fornecedor> ObterFornecedorProdutosEndereco(Guid id);
        Task<IEnumerable<Fornecedor>> ObterParaAutocomplete(string text);
        Task<Fornecedor> ObterParaAutocomplete(Guid id);
    }
}
