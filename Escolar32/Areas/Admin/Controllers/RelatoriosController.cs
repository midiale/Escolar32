using Escolar32.Areas.Admin.ViewModels;
using Escolar32.Context;
using Escolar32.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Escolar32.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class RelatoriosController : Controller
    {
        private readonly AppDbContext _context;

        public RelatoriosController(AppDbContext context)
        {
            _context = context;
        }               

        public IActionResult Pagamentos()
        {

            var alunos = _context.Alunos.ToList();

            return View(alunos.Where(x => x.ExAluno == false));
        }

        public IActionResult Popup()
        {
            // Obtém os anos distintos de AnoDespesa na tabela Despesas
            var anosDespesaDistintos = _context.Despesas
                .Select(d => d.AnoDespesa)
                .Distinct()
                .OrderBy(ano => ano)
                .ToList();

            return PartialView("_PopupAnos", anosDespesaDistintos);

        }

        public async Task<IActionResult> Lucros(int? ano)
        {
           
            // Obter os dados do banco de dados
            var alunos = _context.Alunos.ToList();
            var receitas = _context.Receitas.Where(r => !ano.HasValue || r.AnoReceita == ano).ToList();
            var despesas = _context.Despesas.Where(d => !ano.HasValue || d.AnoDespesa == ano).ToList();

            // Calcular os totais mensais de receitas, despesas e pagamentos
            decimal[] totalReceitas = CalcularTotalPorMes(receitas);
            decimal[] totalDespesas = CalcularTotalPorMes(despesas);
            decimal[] totalPagamentos = CalcularTotalPagamentosPorMes(alunos);

            // Calcular o total anual de lucro
            decimal totalLucro = CalcularLucroTotal(totalReceitas, totalDespesas, totalPagamentos);

            // Criar o ViewModel
            var viewModel = new LucroViewModel
            {
                Alunos = alunos,
                TotalMeses = totalPagamentos,
                TotalReceitas = totalReceitas,
                TotalDespesas = totalDespesas,
                TotalLucroPorMes = CalcularLucroPorMes(totalReceitas, totalDespesas, totalPagamentos),
                TotalLucro = totalLucro
            };

            return View(viewModel);
        }


        private decimal[] CalcularTotalPorMes(IEnumerable<dynamic> items)
        {
            // Inicializar o array para armazenar os totais mensais
            decimal[] totalMeses = new decimal[12];

            // Loop sobre os itens e acumular os valores por mês
            foreach (var item in items)
            {
                int mes = ExtrairMes(item);
                decimal valor = ExtrairValor(item);

                if (mes >= 1 && mes <= 12)
                {
                    totalMeses[mes - 1] += valor;
                }
            }

            return totalMeses;
        }

        private decimal[] CalcularTotalPagamentosPorMes(IEnumerable<Aluno> alunos)
        {
            // Inicializar o array para armazenar os totais mensais de pagamentos
            decimal[] totalMeses = new decimal[12];

            // Loop sobre os alunos e acumular os pagamentos por mês
            foreach (var aluno in alunos)
            {
                if (aluno.Jan) totalMeses[0] += aluno.ValorParcela;
                if (aluno.Fev) totalMeses[1] += aluno.ValorParcela;
                if (aluno.Mar) totalMeses[2] += aluno.ValorParcela;
                if (aluno.Abr) totalMeses[3] += aluno.ValorParcela;
                if (aluno.Mai) totalMeses[4] += aluno.ValorParcela;
                if (aluno.Jun) totalMeses[5] += aluno.ValorParcela;
                if (aluno.Jul) totalMeses[6] += aluno.ValorParcela;
                if (aluno.Ago) totalMeses[7] += aluno.ValorParcela;
                if (aluno.Set) totalMeses[8] += aluno.ValorParcela;
                if (aluno.Out) totalMeses[9] += aluno.ValorParcela;
                if (aluno.Nov) totalMeses[10] += aluno.ValorParcela;
                if (aluno.Dez) totalMeses[11] += aluno.ValorParcela;
            }

            return totalMeses;
        }

        private int ExtrairMes(dynamic item)
        {
            // Extrair o mês do item dinâmico
            if (item is Despesa despesa)
            {
                return despesa.MesDespesa;
            }
            else if (item is Receita receita)
            {
                return receita.MesReceita;
            }
            else
            {
                throw new ArgumentException("O item não é uma despesa ou receita válida.");
            }
        }

        private decimal ExtrairValor(dynamic item)
        {
            // Extrair o valor do item dinâmico
            if (item is Despesa despesa)
            {
                return despesa.ValorDespesa;
            }
            else if (item is Receita receita)
            {
                return receita.ValorReceita;
            }
            else
            {
                throw new ArgumentException("O item não é uma despesa ou receita válida.");
            }
        }

        private decimal CalcularLucroMensal(decimal totalReceita, decimal totalDespesa, decimal totalPagamento)
        {
            return totalPagamento - totalDespesa + totalReceita;
        }

        private decimal[] CalcularLucroPorMes(decimal[] totalReceitas, decimal[] totalDespesas, decimal[] totalPagamentos)
        {
            // Inicializar o array para armazenar os lucros por mês
            decimal[] lucroPorMes = new decimal[12];

            // Calcular o lucro por mês
            for (int i = 0; i < 12; i++)
            {
                lucroPorMes[i] = CalcularLucroMensal(totalReceitas[i], totalDespesas[i], totalPagamentos[i]);
            }

            return lucroPorMes;
        }


        private decimal CalcularLucroTotal(decimal[] totalReceitas, decimal[] totalDespesas, decimal[] totalPagamentos)
        {
            // Calcular o total anual de lucro
            decimal totalReceita = totalReceitas.Sum();
            decimal totalDespesa = totalDespesas.Sum();
            decimal totalPagamento = totalPagamentos.Sum();

            return totalPagamento + totalReceita - totalDespesa;
        }
    }

}
