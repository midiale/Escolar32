document.addEventListener('DOMContentLoaded', function () {
    function atualizarPagamento() {
        document.querySelectorAll('.form-check-input').forEach(checkbox => {
            checkbox.addEventListener('change', function () {
                const alunoId = this.dataset.id;
                const mes = this.dataset.mes;

                // Formando o objeto que a ação espera
                const dadosPagamento = {
                    id: alunoId,
                    aluno: {
                        AlunoId: alunoId,
                        [mes]: true // Aqui você deve ajustar os valores para o mês correto, por exemplo: Jan: true
                    }
                };

                fetch('/Admin/Admin/AtualizarPagamento', {
                    method: 'POST',
                    headers: {
                        'Content-Type': 'application/json',
                    },
                    body: JSON.stringify(dadosPagamento)
                })
                    .then(response => {
                        if (response.ok) {
                            location.reload(); // Recarrega a página após a atualização
                        } else {
                            alert('Erro ao atualizar o pagamento.');
                        }
                    })
                    .catch(() => {
                        alert('Erro ao atualizar o pagamento.');
                    });
            });
        });
    }

    atualizarPagamento();
});


