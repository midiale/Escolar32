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

// Chama a função para os elementos relevantes
controlarExibicao(document.getElementById('qualEscolarGroup'), 'Aluno.VanAnterior');
controlarExibicao(document.getElementById('cartorioGroup'), 'Aluno.FirmaRec');
