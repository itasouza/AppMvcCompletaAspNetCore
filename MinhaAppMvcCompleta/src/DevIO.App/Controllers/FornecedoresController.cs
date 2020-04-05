using AutoMapper;
using DevIO.App.Extensions;
using DevIO.App.ViewModels;
using DevIO.Business.Intefaces;
using DevIO.Business.Interfaces;
using DevIO.Business.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using X.PagedList;

namespace DevIO.App.Controllers
{
    [Authorize]
    public class FornecedoresController : BaseController
    {
        private readonly IFornecedorRepository _fornecedorRepository;
        private readonly IEnderecoRepository _enderecoRepository;
        private readonly IMapper _mapper;

        public FornecedoresController(IFornecedorRepository fornecedorRepository, IMapper mapper,
                                      IEnderecoRepository enderecoRepository, INotificador notificador) : base(notificador)
        {
            _fornecedorRepository = fornecedorRepository;
            _enderecoRepository = enderecoRepository;
            _mapper = mapper;
        }


        [AllowAnonymous]
        [Route("Fornecedores/lista-de-fornecedores")]
        public async Task<IActionResult> Index(string TextoPesquisa = null,
                                               int valorSelecao = 0,
                                               string DataInicial = null,
                                               string DataFinal = null,
                                               int pagina = 1,
                                               int tamanhoPagina = 10)
        {
            //retorna o que foi selecionado
            DateTime? dataInicio = null;
            DateTime? dataFinal = null;


            if (TextoPesquisa != null)
            {
                ViewBag.TextoPesquisa = TextoPesquisa;
                ViewBag.show = "show";
            }

            if (valorSelecao >= 0)
                ViewBag.valorSelecao = valorSelecao;

            if (DataInicial != null)
            {
                ViewBag.DataInicial = DataInicial;
                dataInicio = Convert.ToDateTime(DataInicial).AddHours(0).AddMinutes(00).AddSeconds(00);
            }
            if (DataFinal != null)
            {
                ViewBag.DataFinal = DataFinal;
                dataFinal = Convert.ToDateTime(DataFinal).AddHours(23).AddMinutes(59).AddSeconds(59);
            }

            if (valorSelecao >= 0)
            {
                if (TextoPesquisa != null && valorSelecao == 1)
                {
                    var consulta = await _mapper.Map<IEnumerable<FornecedorViewModel>>(
                                                 await _fornecedorRepository.Buscar(p => p.Nome.Contains(TextoPesquisa)))
                                                .ToPagedListAsync(pagina, tamanhoPagina);
                    ViewBag.TamanhoPagina = tamanhoPagina;
                    return View(consulta);
                }

                if (TextoPesquisa != null && valorSelecao == 2)
                {
                    var consulta = await _mapper.Map<IEnumerable<FornecedorViewModel>>(
                                                 await _fornecedorRepository.Buscar(p => p.Documento.Contains(TextoPesquisa)))
                                                .ToPagedListAsync(pagina, tamanhoPagina);
                    ViewBag.TamanhoPagina = tamanhoPagina;
                    return View(consulta);
                }

                if (valorSelecao == 3 && DataInicial != null && DataFinal != null)
                {
                    var consulta = await _mapper.Map<IEnumerable<FornecedorViewModel>>(
                             await _fornecedorRepository.Buscar(p => p.DataCadastro >= dataInicio && p.DataCadastro <= dataFinal))
                            .ToPagedListAsync(pagina, tamanhoPagina);

                    ViewBag.TamanhoPagina = tamanhoPagina;
                    return View(consulta);
                }

            }


            var dados = await _mapper.Map<IEnumerable<FornecedorViewModel>>(
                                     await _fornecedorRepository.ObterTodos())
                                     .ToPagedListAsync(pagina, tamanhoPagina);
            ViewBag.TamanhoPagina = tamanhoPagina;
            return View(dados);
        }

        [ClaimsAuthorize("Fornecedor", "Adicionar")]
        [Route("Fornecedores/criar-novo-fornecedor")]
        public IActionResult Create()
        {
            return View();
        }

        [ClaimsAuthorize("Fornecedor", "Adicionar")]
        [Route("Fornecedores/criar-novo-fornecedor")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(FornecedorViewModel fornecedorViewModel)
        {

            if (!ModelState.IsValid) return View(fornecedorViewModel);


            try
            {
                var dados = _mapper.Map<Fornecedor>(fornecedorViewModel);
                await _fornecedorRepository.Adicionar(dados);
                TempData["msg"] = "O Cadastro foi realizado com sucesso";
            }
            catch (Exception ex)
            {
                TempData["Erro"] = "Não foi possivel gravar o registro." + ex.Message;
                return RedirectToAction(nameof(Create));
                throw;
            }

            return RedirectToAction(nameof(Index));

        }


        [ClaimsAuthorize("Fornecedor", "Editar")]
        [Route("Fornecedores/editar-fornecedor/{id:Guid}")]
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var fornecedorViewModel = await ObterFornecedorProdutosEndereco(id);

            if (fornecedorViewModel == null)
            {
                return NotFound();
            }
            return View(fornecedorViewModel);
        }

        [ClaimsAuthorize("Fornecedor", "Editar")]
        [Route("Fornecedores/editar-fornecedor/{id:Guid}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, FornecedorViewModel fornecedorViewModel)
        {
            if (id != fornecedorViewModel.Id) return NotFound();
            if (!ModelState.IsValid) return View(fornecedorViewModel);
           

            try
            {
                var dados = _mapper.Map<Fornecedor>(fornecedorViewModel);
                await _fornecedorRepository.Atualizar(dados);
                TempData["msg"] = "O Cadastro foi atualizado com sucesso";
            }
            catch (Exception ex)
            {

                TempData["Erro"] = "Não foi possivel atualizar o registro." + ex.Message;
                return RedirectToAction(nameof(Index));
                throw;
            }



            return RedirectToAction(nameof(Index));

        }

        [ClaimsAuthorize("Fornecedor", "Excluir")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            bool Excluir = true;
            string MensagemTipo = "";
            string MensagemTexto = "";

            var fornecedorViewModel = await ObterFornecedorProdutosEndereco(id);
            if (fornecedorViewModel == null) return NotFound();

            try
            {
               //se tiver endereço, ele precisa ser removido antes 
               if(fornecedorViewModel.Endereco != null)
                {
                    Guid IdEndereco = fornecedorViewModel.Endereco.Id;
                    await _enderecoRepository.Remover(IdEndereco);
                }

               //se tiver produto, precisa avisa que não pode excluir o fornecedor
                foreach (var item in fornecedorViewModel.Produtos)
                {
                    if(item.Id != null)
                    {
                        Excluir = false;
                        TempData["Erro"] = "Não foi possivel remover o registro porque ele está sendo usado em um produto";
                        MensagemTipo = "error";
                        MensagemTexto = "Não foi possivel remover o registro porque ele está sendo usado em um produto";
                    }
                }

                if (Excluir)
                {
                    //remover o fornecedor
                    await _fornecedorRepository.Remover(id);
                    TempData["msg"] = fornecedorViewModel.Nome + " foi excluido com sucesso.";
                    MensagemTipo = "success";
                    MensagemTexto = fornecedorViewModel.Nome + " foi excluido com sucesso.";
                }

            }
            catch (Exception ex)
            {

                TempData["Erro"] = fornecedorViewModel.Nome +  "Não foi possivel remover o registro." + ex.Message;
                return RedirectToAction(nameof(Index));
                throw;
            }
            return Json(new
            {
                result = MensagemTipo,
                mensaje = MensagemTexto
            });
        }



        [ClaimsAuthorize("Fornecedor", "Editar")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AtualizarEndereco(FornecedorViewModel fornecedorViewModel)
        {


            try
            {
                var dados = _mapper.Map<Endereco>(fornecedorViewModel.Endereco);
                await _enderecoRepository.Atualizar(dados);
                TempData["msg"] = "O Cadastro foi atualizado com sucesso";
            }
            catch (Exception ex)
            {

                TempData["Erro"] = "Não foi possivel atualizar o registro." + ex.Message;
                return RedirectToAction(nameof(Index));
                throw;
            }

            return Json(new { result = "ok", mensaje = "O Cadastro foi atualizado com sucesso", 
                 id = fornecedorViewModel.Endereco.FornecedorId });
        }




        private async Task<FornecedorViewModel> ObterFornecedorEndereco(Guid id)
        {
            return _mapper.Map<FornecedorViewModel>(await _fornecedorRepository.ObterFornecedorEndereco(id));
        }

        private async Task<FornecedorViewModel> ObterFornecedorProdutosEndereco(Guid id)
        {
            return _mapper.Map<FornecedorViewModel>(await _fornecedorRepository.ObterFornecedorProdutosEndereco(id));
        }



    }
}
