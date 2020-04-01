

$(document).ready(function () {
    definirDatePicker();

    $('.campovalor').mask('#.##0,00', { reverse: true });

    var idRecebido = $('#FornecedorId').val();

    $.ajax({
        url: "/Produtos/ObterFornecedorParaAutocompleteId/" + idRecebido,
        dataType: 'json',
        type: 'GET',
        success: function (data) {
            // console.log(data.results);
            $(".fornecedor-edicao").empty();
            $.each(data, function (index, row) {
                $(".fornecedor-edicao").append("<option value='" + row.id + "'>" + row.nome + "</option>");
            });
        }
    });

});


//======================================================================

$(".fornecedor-selecao").select2({
    placeholder: "Selecione",
    minimumInputLength: 1,
    ajax: {
        url: '/Produtos/ObterFornecedorParaAutocompleteTexto',
            type: 'GET',
            dataType: 'json',
            quietMillis: 250,
            allowClear: true,
            data: function (params) {
                return { 'text': params.term };
            },
            success: function (data) {
              //  console.log(data.results);
           },
           processResults: function (data) {
                var _results = [];
               $.each(data.results, function (index) {
                   //console.log(data.results[index].id);
                    _results.push({
                        id: data.results[index].id,
                        text: data.results[index].nome
                    });
                });
                return { results: _results };
            }

    }

});


//========================================================================

toastr.options = {
    "closeButton": false,
    "debug": false,
    "newestOnTop": false,
    "progressBar": true,
    "positionClass": "toast-top-right",
    "preventDuplicates": false,
    "showDuration": "200",
    "hideDuration": "500",
    "timeOut": "3000",
    "extendedTimeOut": "1000",
    "showEasing": "swing",
    "hideEasing": "linear",
    "showMethod": "fadeIn",
    "hideMethod": "fadeOut"
}


//====================================================================

$("#formBusca").submit(function (event) {

    var data1 = $("#datainicial").val();
    var data2 = $("#datafinal").val();

    var dataInicial = ConverteParaData(data1);
    var dataFinal = ConverteParaData(data2);

    if (dataInicial > dataFinal) {
        toastr["error"]('A data inicial não pode ser menor que a data final!');
        event.preventDefault();
    }

});

function ConverteParaData(data) {
    var dataArray = data.split('/');
    var novaData = new Date(dataArray[2], dataArray[1], dataArray[0]);
    return novaData;
}
//====================================================================

//configuração do datepicker
function definirDatePicker() {
    $.fn.datepicker.dates['pt-BR'] = {
        days: ['Domingo', 'Segunda', 'Terça', 'Quarta', 'Quinta', 'Sexta', 'Sábado'],
        daysShort: ['Dom', 'Seg', 'Ter', 'Qua', 'Qui', 'Sex', 'Sáb'],
        daysMin: ['Do', 'Se', 'Te', 'Qu', 'Qu', 'Se', 'Sa'],
        months: ['Janeiro', 'Fevereiro', 'Março', 'Abril', 'Maio', 'Junho', 'Julho', 'Agosto', 'Setembro', 'Outubro', 'Novembro', 'Dezembro'],
        monthsShort: ['Jan', 'Fev', 'Mar', 'Abr', 'Mai', 'Jun', 'Jul', 'Ago', 'Set', 'Out', 'Nov', 'Dez'],
        today: 'Hoje',
        monthsTitle: 'Meses',
        clear: 'Limpar',
        format: 'dd/mm/yyyy'
    };

    $('.datepicker').datepicker({
        format: 'dd/mm/yyyy',
        language: 'pt-BR'
    });

}

function CarregarImagem(input) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();
        reader.onload = function (e) {
            $('#imagem').attr('src', e.target.result).height("136px");
        }
    }
    reader.readAsDataURL(input.files[0]);
};