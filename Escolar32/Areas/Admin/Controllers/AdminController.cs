using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Escolar32.Context;
using Microsoft.AspNetCore.Authorization;
using Escolar32.Models;
using Microsoft.AspNetCore.Identity;
using Escolar32.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;

namespace Escolar32.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public AdminController(UserManager<IdentityUser> userManager,
               AppDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        // GET: Admin/Cadastro
        [Authorize("Admin")]
        public IActionResult Index()
        {
            var db = _context.Alunos.Include(x=>x.Escola);

            return View(db.Where(x => x.ExAluno == false));
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Alunos == null)
            {
                return NotFound();
            }

            var db = _context.Alunos.Include(x => x.Escola);
            var aluno = await db
                .FirstOrDefaultAsync(m => m.AlunoId == id);
            if (aluno == null)
            {
                return NotFound();
            }

            return View(aluno);

        }

       // GET: Admim/Cadastro/Create

        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {

            var cadastro = new CadastroViewModel();
            cadastro.Aluno = new Aluno();

            var ListaEscolas = _context.Escolas.ToList();
            
            cadastro.ComboEscolas = new List<SelectListItem>();
            cadastro.ComboSeries = new List<SelectListItem>
            {
                new SelectListItem { Value = "Mat", Text = "Mat" },
                new SelectListItem { Value = "Jd", Text = "Jd" },
                new SelectListItem { Value = "Pré", Text = "Pré" },
                new SelectListItem { Value = "1°", Text = "1°" },
                new SelectListItem { Value = "2°", Text = "2°" },
                new SelectListItem { Value = "3°", Text = "3°" },
                new SelectListItem { Value = "4°", Text = "4°" },
                new SelectListItem { Value = "5°", Text = "5°" },
                new SelectListItem { Value = "6°", Text = "6°" },
                new SelectListItem { Value = "7°", Text = "7°" },
                new SelectListItem { Value = "8°", Text = "8°" },
                new SelectListItem { Value = "9°", Text = "9°" },
                new SelectListItem { Value = "1°M", Text = "1°M" },
                new SelectListItem { Value = "2°M", Text = "2°M" },
                new SelectListItem { Value = "3°M", Text = "3°M" },


            };

            foreach (var item in ListaEscolas)
            {
                var newItem = new SelectListItem { Value = item.EscolaId.ToString(), Text = item.EscolaNome };

                cadastro.ComboEscolas.Add(newItem);
            }

            return View(cadastro);
        }

        // POST: Admin/Cadastro/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(int id, [Bind("AlunoId,Nome,NomeUsuario,DataNasc,Mae,Pai,Cep,Endereco,NumeroCasa,Complemento,Bairro,Cidade,Telefone1,Telefone2,Telefone3," +
                                                                "VanAnterior,QualEscolar,EscolaId,Serie,Periodo,RespFinan,Rg,Cpf,Email,Profissao,FirmaRec," +
                                                                "Cartorio,ValorParcela,QtdeParcelas,TotalContrato,DataCadastro,ExAluno,DataInicio, DataFim, " +
                                                                "InicioPgto, FimPgto, Pago, Jan, Fev, Mar, Abr, Mai, Jun, Jul, Ago, Set, Out, Nov, Dez")] Aluno aluno)
        {

            aluno.NomeUsuario = aluno.Email;
            aluno.DataCadastro = DateTime.Now;
            aluno.TotalContrato = aluno.ValorParcela * aluno.QtdeParcelas;
            aluno.InicioPgto = aluno.DataCadastro;
            aluno.FimPgto = aluno.InicioPgto.AddMonths((aluno.QtdeParcelas - 1));

            var cadastro = new CadastroViewModel();
            cadastro.Aluno = new Aluno();

            var ListaEscolas = _context.Escolas.ToList();

            cadastro.ComboSeries = new List<SelectListItem>
            {
                new SelectListItem { Value = "Mat", Text = "Mat" },
                new SelectListItem { Value = "Jd", Text = "Jd" },
                new SelectListItem { Value = "Pré", Text = "Pré" },
                new SelectListItem { Value = "1°", Text = "1°" },
                new SelectListItem { Value = "2°", Text = "2°" },
                new SelectListItem { Value = "3°", Text = "3°" },
                new SelectListItem { Value = "4°", Text = "4°" },
                new SelectListItem { Value = "5°", Text = "5°" },
                new SelectListItem { Value = "6°", Text = "6°" },
                new SelectListItem { Value = "7°", Text = "7°" },
                new SelectListItem { Value = "8°", Text = "8°" },
                new SelectListItem { Value = "9°", Text = "9°" },
                new SelectListItem { Value = "1°M", Text = "1°M" },
                new SelectListItem { Value = "2°M", Text = "2°M" },
                new SelectListItem { Value = "3°M", Text = "3°M" },

            };
            cadastro.ComboEscolas = new List<SelectListItem>();

            foreach (var item in ListaEscolas)
            {
                var newItem = new SelectListItem { Value = item.EscolaId.ToString(), Text = item.EscolaNome };

                cadastro.ComboEscolas.Add(newItem);
            }


            if (ModelState.IsValid)
            {
                _context.Add(aluno);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Review), aluno);

            }
            return View(cadastro);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Review()
        {
            var cadastro = new CadastroViewModel();
            
            var ListaEscolas = _context.Escolas.ToList();
            
            cadastro.ComboEscolas = new List<SelectListItem>();
           
            foreach (var item in ListaEscolas)
            {
                var newItem = new SelectListItem { Value = item.EscolaId.ToString(), Text = item.EscolaNome };

                cadastro.ComboEscolas.Add(newItem);
            }
                        
            var db = _context.Alunos.Include(x => x.Escola);
            var ultimo = db.OrderByDescending(m => m.AlunoId).Take(1).Last();

            return View(ultimo);
        }

        // GET: Admin/Cadastro/Edit/5

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            var cadastro = new CadastroViewModel();
            cadastro.Aluno = new Aluno();

            var ListaEscolas = _context.Escolas.ToList();
                        
            cadastro.ComboEscolas = new List<SelectListItem>();
            cadastro.ComboSeries = new List<SelectListItem>
            {
                new SelectListItem { Value = "Mat", Text = "Mat" },
                new SelectListItem { Value = "Jd", Text = "Jd" },
                new SelectListItem { Value = "Pré", Text = "Pré" },
                new SelectListItem { Value = "1°", Text = "1°" },
                new SelectListItem { Value = "2°", Text = "2°" },
                new SelectListItem { Value = "3°", Text = "3°" },
                new SelectListItem { Value = "4°", Text = "4°" },
                new SelectListItem { Value = "5°", Text = "5°" },
                new SelectListItem { Value = "6°", Text = "6°" },
                new SelectListItem { Value = "7°", Text = "7°" },
                new SelectListItem { Value = "8°", Text = "8°" },
                new SelectListItem { Value = "9°", Text = "9°" },
                new SelectListItem { Value = "1°M", Text = "1°M" },
                new SelectListItem { Value = "2°M", Text = "2°M" },
                new SelectListItem { Value = "3°M", Text = "3°M" },

            };

            foreach (var item in ListaEscolas)
            {
                var newItem = new SelectListItem { Value = item.EscolaId.ToString(),
                    Text = item.EscolaNome};

                cadastro.ComboEscolas.Add(newItem);
            }                      

            if (id == null || _context.Alunos == null)
            {
                return NotFound();
            }

            cadastro.Aluno = await _context.Alunos.FindAsync(id);
            if (cadastro == null)
            {
                return NotFound();
            }
            return View(cadastro);
        }

        // POST: Admin/Cadastro/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("AlunoId,Nome,NomeUsuario,DataNasc,Mae,Pai,Cep,Endereco,NumeroCasa,Complemento,Bairro,Cidade,Telefone1,Telefone2,Telefone3," +
                                                                "VanAnterior,QualEscolar,EscolaId,Serie,Periodo,RespFinan,Rg,Cpf,Email,Profissao,FirmaRec," +
                                                                "Cartorio,ValorParcela,QtdeParcelas,TotalContrato,DataCadastro,ExAluno,DataInicio, DataFim, " +
                                                                "InicioPgto, FimPgto, Pago, Jan, Fev, Mar, Abr, Mai, Jun, Jul, Ago, Set, Out, Nov, Dez")]Aluno aluno)
        {
            aluno.TotalContrato = aluno.ValorParcela * aluno.QtdeParcelas;
            aluno.FimPgto = aluno.InicioPgto.AddMonths((aluno.QtdeParcelas - 1));

            var cadastro = new CadastroViewModel();
            cadastro.Aluno = new Aluno();

            var ListaEscolas = _context.Escolas.ToList();

            cadastro.ComboSeries = new List<SelectListItem>
            {
                new SelectListItem { Value = "Mat", Text = "Mat" },
                new SelectListItem { Value = "Jd", Text = "Jd" },
                new SelectListItem { Value = "Pré", Text = "Pré" },
                new SelectListItem { Value = "1°", Text = "1°" },
                new SelectListItem { Value = "2°", Text = "2°" },
                new SelectListItem { Value = "3°", Text = "3°" },
                new SelectListItem { Value = "4°", Text = "4°" },
                new SelectListItem { Value = "5°", Text = "5°" },
                new SelectListItem { Value = "6°", Text = "6°" },
                new SelectListItem { Value = "7°", Text = "7°" },
                new SelectListItem { Value = "8°", Text = "8°" },
                new SelectListItem { Value = "9°", Text = "9°" },
                new SelectListItem { Value = "1°M", Text = "1°M" },
                new SelectListItem { Value = "2°M", Text = "2°M" },
                new SelectListItem { Value = "3°M", Text = "3°M" },

            };
            cadastro.ComboEscolas = new List<SelectListItem>();
            
            foreach (var item in ListaEscolas)
            {
                var newItem = new SelectListItem { Value = item.EscolaId.ToString(),
                    Text = item.EscolaNome };

                cadastro.ComboEscolas.Add(newItem);
            }
                        
            if (id != aluno.AlunoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                
                try
                {
                    _context.Update(aluno);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AlunoExists(aluno.AlunoId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Pagamentos", "Relatorios");
            }
            return View(cadastro);
        }

        // GET: Admin/Cadastro/Delete/5

        [Authorize("Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Alunos == null)
            {
                return NotFound();
            }

            var db = _context.Alunos.Include(x => x.Escola);
            var aluno = await db.FirstOrDefaultAsync(m => m.AlunoId == id);
            if (aluno == null)
            {
                return NotFound();
            }

            return View(aluno);
        }

        // POST: Admin/Cadastro/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize("Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Alunos == null)
            {
                return Problem("Entity set 'AppDbContext.Alunos' is null.");
            }
            var aluno = await _context.Alunos.FindAsync(id);
            if (aluno != null)
            {
                _context.Alunos.Remove(aluno);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("List", "ExAluno");
        }

        private bool AlunoExists(int id)
        {
            return _context.Alunos.Any(e => e.AlunoId == id);
        }
    }
}