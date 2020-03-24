using AutoMapper;
using DevIO.App.ViewModels;
using DevIO.Business.Interfaces;
using DevIO.Business.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using X.PagedList;

namespace DevIO.App.Controllers
{
    public class FornecedoresController : BaseController
    {
        private readonly IFornecedorRepository _fornecedorRepository;
        private readonly IMapper _mapper;

        public FornecedoresController(IFornecedorRepository fornecedorRepository, IMapper mapper)
        {
            _fornecedorRepository = fornecedorRepository;
            _mapper = mapper;
        }

        // GET: Fornecedores
        public async Task<IActionResult> Index(string TextoPesquisa = null,
                                               int valorSelecao = 0,
                                               DateTime? DataInicial = null,
                                               DateTime? DataFinal = null,
                                               int pagina = 1,
                                               int tamanhoPagina = 10)
        {
            //retorna o que foi selecionado

            if (TextoPesquisa != null)
            {
                ViewBag.TextoPesquisa = TextoPesquisa;
                ViewBag.show = "show";
            }

            if (valorSelecao >= 0)
                ViewBag.valorSelecao = valorSelecao;
            if (DataInicial != null)
                ViewBag.DataInicial = DataInicial;
            if (DataFinal != null)
                ViewBag.DataFinal = DataFinal;

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

                if (TextoPesquisa != null && valorSelecao == 3)
                {
                    //TODO - falta implementar consulta por data
                }

            }


            var dados = await _mapper.Map<IEnumerable<FornecedorViewModel>>(
                                     await _fornecedorRepository.ObterTodos())
                                     .ToPagedListAsync(pagina, tamanhoPagina);
            ViewBag.TamanhoPagina = tamanhoPagina;
            return View(dados);
        }


        // GET: Fornecedores/Create
        public IActionResult Create()
        {
            return View();
        }

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
                return RedirectToAction(nameof(Index));
                throw;
            }

            return RedirectToAction(nameof(Index));

        }

        // GET: Fornecedores/Edit/5
        public async Task<IActionResult> Edit(Guid id)
        {
            var fornecedorViewModel = await ObterFornecedorProdutosEndereco(id);

            if (fornecedorViewModel == null)
            {
                return NotFound();
            }
            return View(fornecedorViewModel);
        }

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



        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var fornecedorViewModel = await ObterFornecedorEndereco(id);
            if (fornecedorViewModel == null) return NotFound();

            try
            {
                await _fornecedorRepository.Remover(id);
                TempData["msg"] = fornecedorViewModel.Nome + " foi excluido com sucesso."; 
            }
            catch (Exception ex)
            {

                TempData["Erro"] = "Não foi possivel remover o registro." + ex.Message;
                return RedirectToAction(nameof(Index));
                throw;
            }

            return Json(fornecedorViewModel.Nome + "  foi excluido com sucesso.");
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
