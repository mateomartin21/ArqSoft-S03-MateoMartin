using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text.Json;

namespace Catalogo.Presentation.Controllers
{
    public class CuentaController : Controller
    {
        // Ruta donde guardaremos el JSON de usuarios
        private readonly string _rutaUsuarios;

        public CuentaController(IWebHostEnvironment env)
        {
            _rutaUsuarios = Path.Combine(env.ContentRootPath, "data", "usuarios.json");
        }

        // GET: Abre la pantalla de Login
        public IActionResult Login() => View();

        // POST: Recibe los datos del formulario de Login
        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            var usuarios = LeerUsuariosJson();
            var usuarioValido = usuarios.FirstOrDefault(u => u.Username == username && u.Password == password);

            if (usuarioValido != null)
            {
                // Si existe, creamos la Cookie de sesión
                var claims = new List<Claim> { new Claim(ClaimTypes.Name, username) };
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
                return RedirectToAction("Index", "Catalogo"); // Lo mandamos a la Pokédex
            }

            ViewBag.Error = "Usuario o contraseña incorrectos.";
            return View();
        }

        // POST: Cierra sesión y borra la Cookie
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
        // ==========================================
        // LÓGICA DE REGISTRO AÑADIDA
        // ==========================================

        // GET: Abre la pantalla de Registro
        public IActionResult Register() => View();

        // POST: Recibe los datos, crea el usuario y lo guarda en el JSON
        [HttpPost]
        public IActionResult Register(string username, string password)
        {
            var usuarios = LeerUsuariosJson();

            // Verificamos si el usuario ya existe
            if (usuarios.Any(u => u.Username == username))
            {
                ViewBag.Error = "Este usuario ya está registrado. Elige otro.";
                return View();
            }

            // Agregamos el nuevo usuario a la lista
            usuarios.Add(new UsuarioModel { Username = username, Password = password });

            // Guardamos la lista completa de vuelta en el archivo JSON
            var opcionesJson = new JsonSerializerOptions { WriteIndented = true };
            var jsonNuevo = JsonSerializer.Serialize(usuarios, opcionesJson);
            System.IO.File.WriteAllText(_rutaUsuarios, jsonNuevo);

            // Lo mandamos a la pantalla de Login para que inicie sesión
            return RedirectToAction("Login");
        }

        // ==========================================
        // Lógica interna para leer el JSON
        // ==========================================
        private List<UsuarioModel> LeerUsuariosJson()
        {
            if (!System.IO.File.Exists(_rutaUsuarios)) return new List<UsuarioModel>();
            var json = System.IO.File.ReadAllText(_rutaUsuarios);
            return JsonSerializer.Deserialize<List<UsuarioModel>>(json) ?? new List<UsuarioModel>();
        }
    }

    // Tu modelo de usuario
    public class UsuarioModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}