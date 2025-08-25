// Criação da tabela de ativos
$(document).ready(function () {
    $('.data-table').DataTable({
    "autoWidth": true, 
    "responsive": true, 
    "ordering": true,
    "paging": true,
    "searching": true,
    "language": {
        "emptyTable": "Nenhum registro encontrado na tabela",
        "info": "Mostrando _START_ até _END_ de _TOTAL_ registros",
        "infoEmpty": "Mostrando 0 até 0 de 0 registros",
        "infoFiltered": "(Filtrado de _MAX_ registros totais)",
        "lengthMenu": "Mostrar _MENU_ registros por página",
        "loadingRecords": "Carregando...",
        "processing": "Processando...",
        "zeroRecords": "Nenhum registro encontrado",
        "search": "Pesquisar",
        "paginate": {
            "next": "Próximo",
            "previous": "Anterior",
            "first": "Primeiro",
            "last": "Último"
        },
        "aria": {
            "sortAscending": ": Ordenar colunas de forma ascendente",
            "sortDescending": ": Ordenar colunas de forma descendente"
        }
    }
});
});

// Ao clicar no botão de confirmar deleção aparece o modal de confirmação
$(document).ready(function () {
    $('.btn-confirmar-deletar').click(function (page) {
        page.preventDefault(); // Impede navegação, permitindo a PartialView

        var url = $(this).attr("href"); // Pega a URL já montada pelo asp-route

        $.ajax({
            type: 'GET',
            url: url, // Usa direto a URL da variável
            success: function (result) {
                $('#modalDeletarAtivo .modal-content').html(result);

                var modal = new bootstrap.Modal($('#modalDeletarAtivo'));
                modal.show();
            }
        });
    });
});
