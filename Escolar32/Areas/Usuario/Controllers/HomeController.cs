using Escolar32.Context;
using Escolar32.Models;
using Escolar32.Repositories.Interfaces;
using Escolar32.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Escolar32.Areas.Usuario.Controllers
{
    [Area("Usuario")]
    [Authorize(Roles = "Member,Admin")]
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IAlunoRepository _alunoRepository;

        public HomeController(UserManager<IdentityUser> userManager,
               AppDbContext context,IAlunoRepository alunoRepository)
        {
            _userManager = userManager;
            _context = context;
            _alunoRepository = alunoRepository;
        }

        public async Task<IActionResult> Create()
        {
            var cadastro = new CadastroViewModel();
            var user = await _userManager.GetUserAsync(User);

            cadastro.Aluno = new Aluno
            {
                Email = user.UserName
            };
            
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

        // POST: Usuario/Cadastro/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Member,Admin")]
        public IActionResult Create(int id, [Bind("AlunoId,Nome,NomeUsuario,DataNasc,Mae,Pai,Cep,Endereco,NumeroCasa,Complemento,Bairro,Cidade,Telefone1,Telefone2,Telefone3," +
                                                                "VanAnterior,QualEscolar,EscolaId,Serie,Periodo,RespFinan,Rg,Cpf,Email,Profissao,FirmaRec," +
                                                                "Cartorio,DataCadastro,ExAluno")] Aluno aluno)

        {
            aluno.DataCadastro = DateTime.Now;

            if (ModelState.IsValid)
            {
                return RedirectToAction(nameof(Review), aluno);
            }

            var cadastro = new CadastroViewModel();

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

        [Authorize(Roles = "Member,Admin")]
        public IActionResult Review(Aluno aluno)
        {
            
            var escola = _context.Escolas.FirstOrDefault(e => e.EscolaId == aluno.EscolaId);
            if (escola != null)
            {
                aluno.Escola = escola;
            }

            return View(aluno);
        }        

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Member,Admin")]
        public async Task<IActionResult> ReviewConfirmed([Bind("AlunoId,Nome,NomeUsuario,DataNasc,Mae,Pai,Cep,Endereco,NumeroCasa,Complemento,Bairro,Cidade,Telefone1,Telefone2,Telefone3," +
                                                                "VanAnterior,QualEscolar,EscolaId,Serie,Periodo,RespFinan,Rg,Cpf,Email,Profissao,FirmaRec," +
                                                                "Cartorio,DataCadastro")]Aluno aluno)
        {
            aluno.DataCadastro = DateTime.Now;
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            string usuario = Convert.ToString(user.UserName);
            aluno.NomeUsuario = usuario;

            if (ModelState.IsValid)
            {
                _context.Alunos.Add(aluno);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Aluno), new { id = aluno.AlunoId });
            }
            
            return RedirectToAction(nameof(Create), aluno);
        }

        // GET: Usuario/Cadastro/Edit/5
        [HttpPost]
        [Authorize(Roles = "Member,Admin")]
        public IActionResult Edit([Bind("AlunoId,Nome,NomeUsuario,DataNasc,Mae,Pai,Cep,Endereco,NumeroCasa,Complemento,Bairro,Cidade,Telefone1,Telefone2,Telefone3," +
                                                                "VanAnterior,QualEscolar,EscolaId,Serie,Periodo,RespFinan,Rg,Cpf,Email,Profissao,FirmaRec," +
                                                                "Cartorio,DataCadastro")]Aluno aluno)
        {
                     
            var cadastroViewModel = new CadastroViewModel
            {
                Aluno = aluno,
                ComboEscolas = _context.Escolas.Select(e => new SelectListItem { Value = e.EscolaId.ToString(), Text = e.EscolaNome }).ToList()
            };

            var alunoJson = JsonConvert.SerializeObject(aluno);

            TempData["AlunoJson"] = alunoJson;

            return RedirectToAction("EditCadastro");
        }

        public IActionResult EditCadastro()
        {
            var cadastro = new CadastroViewModel();
            
            var alunoJson = TempData["AlunoJson"] as string;

            if (alunoJson != null)
            {
                var aluno = JsonConvert.DeserializeObject<Aluno>(alunoJson);
                                
                cadastro.Aluno = aluno;
            }
            else
            {
                return RedirectToAction("Error");
            }

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

        [Authorize(Roles = "Member,Admin")]
        public IActionResult Aluno()
        {
            var user = User.Identity.Name;

            var alunos = _alunoRepository.GetAlunoByUsuario(user).Include(a => a.Escola).ToList();

            if (alunos.Any() && alunos.All(a => a.ExAluno))
            {
                return RedirectToAction("AccessDenied", "Account", new { area = "" });
            }

            else if (alunos.Count == 0)
            {
                return RedirectToAction("Create");
            }
            else
            {
                return View(alunos.Where(a => a.ExAluno==false));
            }
        }

        [Authorize(Roles = "Member,Admin")]
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
        private bool AlunoExists(int id)
        {
            return _context.Alunos.Any(e => e.AlunoId == id);
        }
    }
}