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

// Função para exibir quanto custará os ativos na tela de cadastro/edição de ativos
function CotacaoTempoReal(tickerInput, cotasInput, resultContainer) { 
    $(cotasInput).on('input', function () {
    let ticker = $(tickerInput).val();
    let cotas = $(cotasInput).val();

    // Guard Clause para inputs inválidos
    if (ticker.length < 5 || cotas < 1 ) {
        $(resultadoContainer).empty(); // Não exibe nada
            return;
    }

        $.ajax({
        type: 'GET',
        dataType: 'json', // // Convertendo do endpoint para Json
        url: `/Ativo/PesquisaAtivo?ticker=${ticker.toUpperCase()}`, // Endpoint do controller para pesquisa
        success: function (data) {
            // Guard Clause para ativos não retornados, minimizando uso de elses
            if (data.length < 1 || data == null || data.stock.toUpperCase() != ticker.toUpperCase()) {
                $(resultadoContainer).empty(); // Não exibe nada
                return; // Para parar aqui se não houver um ativo retornado
            }

            // Insere o preço total no resultContainer
            $(resultContainer).html(
                `<h5>O preço total aproximado é: R$ ${(data.close * cotas).toFixed(2)}</h5>`
            )
        },
        error: function (e) {
            $(resultContainer).html(
                `<h5>Houve um erro inesperado durante a busca da cotação em tempo real.</h5>`
            )
        }
    });
});
}

$(document).ready(function () {
    CotacaoTempoReal('#autocomplete-pesquisa', '#cotas', '#valorTotal' );
});

/* Aparição de Modal de negociação, mas sem JQuery para fins didáticos. */
let buttonNegociar = document.getElementById('ativaModalNegociar');
let modalNegociar = document.getElementById('modalEscolhaNegociacaoAtivo');
let buttonFechaNegociar = document.getElementById('btn-modal-close');

buttonNegociar.addEventListener("click", () => {
    if (!modalNegociar.open) {
        modalNegociar.showModal();
    }
});

buttonFechaNegociar.addEventListener("click", () => {
    if (modalNegociar.open) {
        modalNegociar.close();
    }
});