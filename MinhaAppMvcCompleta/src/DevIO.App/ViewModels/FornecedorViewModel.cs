using DevIO.App.Language;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DevIO.App.ViewModels
{
    public class FornecedorViewModel
    {
        [Key]
        public Guid Id { get; set; }


        [Display(ResourceType = typeof(Traducao), Name = "NomeFornecedor")]
        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E001")]
        [MinLength(2, ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E002")]
        [MaxLength(100, ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E003")]
        public string Nome { get; set; }


        [Display(ResourceType = typeof(Traducao), Name = "DocFornecedor")]
        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E001")]
        [MinLength(14, ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E002")]
        public string Documento { get; set; }


        [Display(ResourceType = typeof(Traducao), Name = "TipoFornecedor")]
        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E001")]
        public int TipoFornecedor { get; set; }

      
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


        public EnderecoViewModel Endereco { get; set; }
        public IEnumerable<ProdutoViewModel> Produtos { get; set; }
    }
}
