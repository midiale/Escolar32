using Escolar32.Services;
using Escolar32.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Escolar32.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        
        public AccountController(UserManager<IdentityUser> userManager,
               SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            
        }        
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel registroVM)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser { UserName = registroVM.UserName};
                var result = await _userManager.CreateAsync(user, registroVM.Password);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "Member");
                    string code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code }, protocol: HttpContext.Request.Scheme);
                    await ServicoEmail.EnviaEmailAsync(registroVM.UserName, "Meu Transporte Escolar", "Confirme a sua conta clicando <a href=\"" + callbackUrl + "\">AQUI</a>");
                    ViewBag.Message = " Verifique seu e-mail e clique no link enviado para confirmar a sua conta ";
                    return View("Info");
                    
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        if (error.Code == "DuplicateUserName")
                        {
                            ModelState.AddModelError("UserName", "E-mail já cadastrado. Entre com seu e-mail e senha!");
                        }
                        else
                        {

                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                    }
                }

            }

            return View(registroVM);
        }
        public IActionResult Login(string returnUrl)
        {
            return View(new LoginViewModel()
            {
                ReturnUrl = returnUrl
            });
        }

        [HttpPost]
        public async Task<ActionResult> Login(LoginViewModel loginVM)
        {
            if (!ModelState.IsValid)
            {
                return View(loginVM);
            }                

            var user = await _userManager.FindByNameAsync(loginVM.UserName);
            
            if (user != null )
            {
                if (!await _userManager.IsEmailConfirmedAsync(user))
                {
                    ViewBag.errorMessage = "Verifique EMAIL DE CONFIRMAÇÃO antes de fazer o login.";
                    return View("Error");
                }

                var result = await _signInManager.PasswordSignInAsync(user, loginVM.Password, false, false);

                if (result.Succeeded)
                {
                    var role = await _userManager.IsInRoleAsync(user, "Admin");
                    if (role)
                    {
                        return RedirectToAction("Index", "Admin", new { area = "Admin" });
                    }

                    return RedirectToAction("Aluno", "Home", new { area = "Usuario" });

                }                           

            }
            ModelState.AddModelError("", "Falha ao realizar Login");
            return View(loginVM);
        }        

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            HttpContext.Session.Clear();
            HttpContext.User = null;
            await _signInManager.SignOutAsync();
            return RedirectToAction("Home", "Home");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }

        [AllowAnonymous]
        public async Task<ActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return View("Error");
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return View("Error");
            }

            var result = await _userManager.ConfirmEmailAsync(user, code);
            if (result.Succeeded)
            {
                return View();
            }
            else
            {
                return View("Error");
            }
        }

        // GET: /Account/ForgotPassword
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        // POST: /Account/ForgotPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel forgotVM)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(forgotVM.UserName);
                if (user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return View("ForgotPasswordNotConfirmed");
                }
               
                string code = await _userManager.GeneratePasswordResetTokenAsync(user);
                var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code }, protocol: HttpContext.Request.Scheme);
                await ServicoEmail.EnviaEmailAsync(forgotVM.UserName, "Resetar a Senha", "Resete a sua senha clicando <a href=\"" + callbackUrl + "\">AQUI</a>");
                return RedirectToAction("ForgotPasswordConfirmation", "Account");
            }

            return View(forgotVM);
        }

        // GET: /Account/ForgotPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        // GET: /Account/ForgotPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ForgotPasswordNotConfirmed()
        {
            return View();
        }

        // GET: /Account/ResetPassword
        [AllowAnonymous]
        public ActionResult ResetPassword(string code)
        {
            return code == null ? View("Error") : View();
        }

        // POST: /Account/ResetPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(ResetPasswordViewModel resetPVM)
        {
            if (!ModelState.IsValid)
            {
                return View(resetPVM);
            }
            var user = await _userManager.FindByNameAsync(resetPVM.UserName);
            if (user == null)
            {
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            var result = await _userManager.ResetPasswordAsync(user, resetPVM.Code, resetPVM.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
           
            return View();
        }

        //
        // GET: /Account/ResetPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ResetPasswordConfirmation()
        {
            return View();
        }

    }

}