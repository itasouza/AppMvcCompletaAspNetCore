var CustomSweetAlert = (function () {
    return {
        init: function () {
            var scope = this;
        },
        setDefaultSuccess: function () {
            swal({
                title: 'Sucesso!',
                text: "Operação realizada com sucesso.",
                type: 'success',
                showCancelButton: false,
                confirmButtonColor: '#28a745',
                confirmButtonText: 'Ok'
            });
        },
        setDefaultError: function () {
            swal({
                title: 'Erro!',
                text: "Operação não foi realizada com sucesso.",
                type: 'error',
                showCancelButton: false,
                //confirmButtonColor: '#dc3545',
                confirmButtonText: 'Ok'
            });
        }
    };

})();

$(function () {
    CustomSweetAlert.init();

});
