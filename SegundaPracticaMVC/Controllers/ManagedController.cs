using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using PracticaMvcCore2ACV.Models;
using PracticaMvcCore2ACV.Repositories;
using System;
using System.Security.Claims;

namespace PracticaMvcCore2ACV.Controllers
{
    public class ManagedController : Controller
    {
        private RepositoryLibros repo;

        public ManagedController(RepositoryLibros repo)
        {
            this.repo = repo;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            Usuarios usuario = await this.repo.LogInUsuarioAsync(email, password);

            if (usuario != null)
            {
                ClaimsIdentity identity = new ClaimsIdentity(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    ClaimTypes.Name, ClaimTypes.Role);

                Claim claimName = new Claim(ClaimTypes.Name, usuario.Nombre);
                Claim claimId = new Claim(ClaimTypes.NameIdentifier, usuario.IdUsuario.ToString());
                Claim claimEmail = new Claim("Email", usuario.Email);
                Claim claimApellido = new Claim("Apellido", usuario.Apellido);
                Claim claimImg = new Claim("Imagen", usuario.Foto);

                identity.AddClaim(claimName);
                identity.AddClaim(claimId);
                identity.AddClaim(claimApellido);
                identity.AddClaim(claimEmail);
                identity.AddClaim(claimImg);

                ClaimsPrincipal userPrincipal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    userPrincipal);

                    string controller = "Home";
                    string action = "Index";
                

                if (TempData["id"] != null)
                {
                    string id = TempData["id"].ToString();

                    return RedirectToAction(action, controller, new { id = id });
                }
                else
                {
                    return RedirectToAction(action, controller);
                }
            }
            else
            {
                ViewData["MENSAJE"] = "Credenciales incorrectas";
                return View();
            }
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(
                CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Index", "Home");
        }

        public IActionResult ErrorAcceso()
        {
            return View();
        }
    }
}
