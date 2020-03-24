using DevIO.App.Extensions;
using DevIO.App.Language;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DevIO.App.ViewModels
{
    public class ProdutoViewModel
    {

        [Key]
        public Guid Id { get; set; }


        [Display(ResourceType = typeof(Traducao), Name = "NomeProduto")]
        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E001")]
        [MinLength(10, ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E002")]
        public string Nome { get; set; }


        [Display(ResourceType = typeof(Traducao), Name = "DescricaoProduto")]
        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E001")]
        [MinLength(10, ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E002")]
        [MaxLength(1000, ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E003")]
        public string Descricao { get; set; }


        [Display(ResourceType = typeof(Traducao), Name = "ImagemProduto")]
        public IFormFile ImagemUpload { get; set; }

        [Display(ResourceType = typeof(Traducao), Name = "ImagemProduto")]
        public string Imagem { get; set; }


        [Display(ResourceType = typeof(Traducao), Name = "ValidadeProduto")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public DateTime? DataValidade { get; set; }



        [Display(ResourceType = typeof(Traducao), Name = "PromocaoProduto")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public bool ProdutoPromocao { get; set; }

        [Moeda]
        [Display(ResourceType = typeof(Traducao), Name = "ValorProduto")]
        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E001")]
        public decimal Valor { get; set; }


        [Display(ResourceType = typeof(Traducao), Name = "Ativo")]
        public bool Ativo { get; set; }


        [Display(ResourceType = typeof(Traducao), Name = "DataCadastro")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [ScaffoldColumn(false)]
        public DateTime? DataCadastro { get; set; }


        [Display(ResourceType = typeof(Traducao), Name = "DataAlteracao")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [ScaffoldColumn(false)]
        public DateTime? DataAlteracao { get; set; }


        [Display(ResourceType = typeof(Traducao), Name = "NomeFornecedor")]
        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E001")]
        public Guid FornecedorId { get; set; }

        public FornecedorViewModel Fornecedor { get; set; }

        //Lista de Forncedores para o cadastro de produto
        public IEnumerable<FornecedorViewModel> Fornecedores { get; set; }

    }
}
