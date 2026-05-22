using Catalogo.Domain.Models;
using Catalogo.Application.Services;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Catalogo.Presentation.Models;
// ------------------------------

namespace CatalogoApp.Presentation.Controllers
{
    public class CatalogoController : Controller
    {
        private readonly ItemService _service;

        private readonly string _rutaComentarios;

        // El servicio llega por inyección de dependencias
        public CatalogoController(ItemService service, IWebHostEnvironment env)
        {
            _service = service;
            _rutaComentarios = Path.Combine(env.ContentRootPath, "data", "comentarios.json");
        }

        // Lista con filtro opcional por tipo
        public IActionResult Index(string? tipo)
        {
            var items = string.IsNullOrEmpty(tipo)
                ? _service.ObtenerTodos()
                : _service.ObtenerPorTipo(tipo);

            ViewBag.Tipos = _service.ObtenerTipos();
            ViewBag.TipoActual = tipo;

            return View(items);
        }

        // Detalle de un item
        public IActionResult Detalle(int id)
        {
            var item = _service.ObtenerPorId(id);

            if (item == null)
            {
                return NotFound();
            }

            var todosLosComentarios = LeerComentariosJson();
            ViewBag.Comentarios = todosLosComentarios
                .Where(c => c.PokemonId == id)
                .OrderByDescending(c => c.Fecha)
                .ToList();

            return View(item);
        }

        // Formulario — GET 
        public IActionResult Agregar()
        {
            return View();
        }

        // Formulario — POST 
        [HttpPost]
        public IActionResult Agregar(Item item)
        {
            _service.Agregar(item);
            return RedirectToAction("Index");
        }

        // Eliminar 
        public IActionResult Eliminar(int id)
        {
            _service.Eliminar(id);
            return RedirectToAction("Index");
        }


        [HttpPost]
        public IActionResult AgregarComentario(int pokemonId, string texto)
        {
            // Solo guarda si hay texto y el usuario tiene sesión iniciada por Cookies
            if (!string.IsNullOrEmpty(texto) && User.Identity is { IsAuthenticated: true })
            {
                var todosLosComentarios = LeerComentariosJson();

                todosLosComentarios.Add(new Comentario
                {
                    PokemonId = pokemonId,
                    Usuario = User.Identity.Name,
                    Texto = texto,
                    Fecha = DateTime.Now
                });

                var opciones = new JsonSerializerOptions { WriteIndented = true };
                System.IO.File.WriteAllText(_rutaComentarios, JsonSerializer.Serialize(todosLosComentarios, opciones));
            }

            return RedirectToAction("Detalle", new { id = pokemonId });
        }

        private List<Comentario> LeerComentariosJson()
        {
            if (!System.IO.File.Exists(_rutaComentarios)) return new List<Comentario>();
            var json = System.IO.File.ReadAllText(_rutaComentarios);
            return JsonSerializer.Deserialize<List<Comentario>>(json) ?? new List<Comentario>();
        }
    }

}