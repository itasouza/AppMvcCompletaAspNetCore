
$(document).ready(function () {
    definirDatePicker();
    
    $('.campovalor').mask('#.##0,00', { reverse: true });

});


$("#formBusca").submit(function (event) {

    var data1 = $("#datainicial").val();
    var data2 = $("#datafinal").val();

    var dataInicial = ConverteParaData(data1);
    var dataFinal = ConverteParaData(data2);

    if (dataInicial > dataFinal) {
        alert('A data inicial não pode ser menor que a data final!');
        event.preventDefault();
    }

});




function ConverteParaData(data) {
    var dataArray = data.split('/');
    var novaData = new Date(dataArray[2], dataArray[1], dataArray[0]);
    return novaData;
}





function ErroSweetalert() {
    swal({
        position: 'top-end',
        showConfirmButton: false,
        toast: true,
        timer: 3000,
        type: 'error',
        title: 'A data inicial não pode ser menor que a data final!'
    });
}



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




function AjaxModal() {

    $(document).ready(function () {
        $(function () {
            $.ajaxSetup({ cache: false });

            $("a[data-modal]").on("click",
                function (e) {
                    $('#myModalContent').load(this.href,
                        function () {
                            $('#myModal').modal({
                                keyboard: true
                            },
                                'show');
                            bindForm(this);
                        });
                    return false;
                });
        });

        function bindForm(dialog) {
            $('form', dialog).submit(function () {
                $.ajax({
                    url: this.action,
                    type: this.method,
                    data: $(this).serialize(),
                    success: function (result) {
                        if (result.success) {
                            $('#myModal').modal('hide');
                            $('#EnderecoTarget').load(result.url); // Carrega o resultado HTML para a div demarcada
                        } else {
                            $('#myModalContent').html(result);
                            bindForm(dialog);
                        }
                    }
                });
                return false;
            });
        }
    });
}