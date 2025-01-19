// Função para validar a senha conforme os critérios
function validaSenha() {
    let maiusculo = /(?=.*[A-Z]).*$/;
    let minusculo = /(?=.*[a-z]).*$/;
    let digito = /(?=.*[0-9]).*$/;
    let especiais = /(?=.*\W+).*/;

    let senha = $("#passwordInput").val();
    let validMaiusculo = senha.match(maiusculo);
    let validMinusculo = senha.match(minusculo);
    let validDigito = senha.match(digito);
    let validEspeciais = senha.match(especiais);
    let validComprimento = senha.length >= 8;

    if (senha.length >= 8) $("#Valida1").removeClass("text-danger").addClass("text-success");
    else $("#Valida1").removeClass("text-success").addClass("text-danger");

    if (senha.match(maiusculo)) $("#Valida2").removeClass("text-danger").addClass("text-success");
    else $("#Valida2").removeClass("text-success").addClass("text-danger");

    if (senha.match(minusculo)) $("#Valida3").removeClass("text-danger").addClass("text-success");
    else $("#Valida3").removeClass("text-success").addClass("text-danger");

    if (senha.match(digito)) $("#Valida4").removeClass("text-danger").addClass("text-success");
    else $("#Valida4").removeClass("text-success").addClass("text-danger");

    if (senha.match(especiais)) $("#Valida5").removeClass("text-danger").addClass("text-success");
    else $("#Valida5").removeClass("text-success").addClass("text-danger");

    if (validComprimento && validMaiusculo && validMinusculo && validDigito && validEspeciais) {
        $("#validationContainer").css("background-color", "lightgreen");
    } else {
        $("#validationContainer").css("background-color", "");
    }
}
//Máscara de CPF
function mascara(i, t)
{
    if (t === "cpf")
{
    i.setAttribute("maxlength", "14");

i.addEventListener("input", function (event)
{
            var value = event.target.value.replace(/\D/g, "");

            if (value.length > 3 && value.length < 7)
{
    event.target.value = value.substring(0, 3) + "." + value.substring(3);
            }
            else if (value.length >= 7 && value.length < 11)
{
    event.target.value = value.substring(0, 3) + "." + value.substring(3, 6) + "." + value.substring(6);
            } else if (value.length >= 11)
{
    event.target.value = value.substring(0, 3) + "." + value.substring(3, 6) + "." + value.substring(6, 9) + "-" + value.substring(9);
            }

        });
    }
}

//Macara de Telefone
function mask(o, f)
{
    setTimeout(function () {
        var v = mphone(o.value);
        if (v != o.value) {
            o.value = v;
        }
    }, 1);
}
function mphone(v)
{
    var r = v.replace(/\D/g, "");
r = r.replace(/^0/, "");
    if (r.length > 10)
{
    r = r.replace(/^(\d\d)(\d{5})(\d{4}).*/, "($1) $2-$3");
    } else if (r.length > 5)
{
    r = r.replace(/^(\d\d)(\d{4})(\d{0,4}).*/, "($1) $2-$3");
    } else if (r.length > 2)
{
    r = r.replace(/^(\d\d)(\d{0,5})/, "($1) $2");
    } else {
    r = r.replace(/^(\d*)/, "($1");
    }
return r;
}

//Busca CEP
(function () {
    var script = document.createElement('script');
    script.src = 'https://ajax.googleapis.com/ajax/libs/jquery/3.6.0/jquery.min.js';
    script.onload = function () {
        
        $(document).ready(function () {
            $('#Cep').on('change', function () {
                var cep = $(this).val();
                if (cep.length === 8) {
                    $.ajax({
                        url: 'https://viacep.com.br/ws/' + cep + '/json/',
                        type: 'GET',
                        dataType: 'json',
                        success: function (data) {
                            $('#Endereco').val(data.logradouro);
                            $('#Bairro').val(data.bairro);
                            $('#Cidade').val(data.localidade);
                        },
                        error: function () {
                           message.alert("CEP não encontrado");
                        }
                    });
                }
            });
        });
    };
    document.head.appendChild(script);
})();

//Limpa Campo Data
const camposData = document.querySelectorAll('.campo-data');

function formatarData(data) {
    const dia = String(data.getDate()).padStart(2, '0');
    const mes = String(data.getMonth() + 1).padStart(2, '0');
    const ano = data.getFullYear();
    return `${ano}-${mes}-${dia}`;
}

camposData.forEach(function (campo) {
    const dataInicial = new Date();
    campo.value = formatarData(dataInicial);
});

// Exibe campo quando a opção for "true"" e oculta com "false""
function controlarExibicao(elemento, radioButtonName) {
    var radioButtons = document.querySelectorAll('input[name="' + radioButtonName + '"]');

    radioButtons.forEach(function (radioButton) {
        radioButton.addEventListener('change', function () {
            if (this.value === "true" && this.checked) {
                elemento.style.display = 'block';
            } else {
                elemento.style.display = 'none';
            }
        });
    });
}

document.addEventListener("DOMContentLoaded", () => {
    const abrirPopup = document.getElementById("abrir-popup");
    const popup = document.getElementById("popup-container");

    if (abrirPopup && popup) {
        // Abrir o popup e carregar os anos dinamicamente
        abrirPopup.addEventListener("click", (event) => {
            event.preventDefault();

            // Fazer uma requisição AJAX para carregar os anos
            fetch("/Admin/Relatorios/Popup")
                .then((response) => {
                    if (!response.ok) throw new Error("Erro ao carregar o popup.");
                    return response.text();
                })
                .then((html) => {
                    popup.innerHTML = html; // Inserir o conteúdo no popup
                    popup.style.display = "block"; // Mostrar o popup

                    // Adicionar evento ao dropdown para redirecionar ao selecionar o ano
                    const dropdown = document.getElementById("dropdown-anos");
                    if (dropdown) {
                        dropdown.addEventListener("change", () => {
                            const anoSelecionado = dropdown.value;

                            if (anoSelecionado) {
                                // Redirecionar para a página Lucros com o ano selecionado
                                window.location.href = `/Admin/Relatorios/Lucros?ano=${anoSelecionado}`;
                            }
                        });
                    }
                })
                .catch((error) => {
                    console.error("Erro ao carregar o popup:", error);
                });
        });
    }

    window.formatDecimalInput = function (input) {
        // Substitui todos os pontos por vírgulas
        input.value = input.value.replace(/\./g, ',');
    };

    window.formatOnBlur = function (input) {
        // Remove espaços desnecessários e ajusta a formatação decimal
        input.value = input.value.trim().replace(/\./g, ',');
    };


});

// Chama a função para os elementos relevantes
controlarExibicao(document.getElementById('qualEscolarGroup'), 'Aluno.VanAnterior');
controlarExibicao(document.getElementById('cartorioGroup'), 'Aluno.FirmaRec');
