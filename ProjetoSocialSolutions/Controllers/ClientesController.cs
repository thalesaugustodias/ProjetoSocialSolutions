using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjetoSocialSolutions.Data;
using ProjetoSocialSolutions.Models;
using ProjetoSocialSolutions.Services;
using ProjetoSocialSolutions.Models.ViewModel;
using ProjetoSocialSolutions.Services.Exceptions;
using System.Diagnostics;

namespace ProjetoSocialSolutions.Controllers
{
    public class ClientesController : Controller
    {
        private readonly ClienteService _clienteService;
        private readonly ImovelService _imovelService;

        public ClientesController(ClienteService clienteService, ImovelService imovelService)
        {
            _clienteService = clienteService;
            _imovelService = imovelService;
        }

        // GET: Clientes
        public IActionResult Index()
        {
            var list = _clienteService.FindAll();
            return View(list);
        }

        public IActionResult Create()
        {
            var imovel = _imovelService.FindAll();
            var viewModel = new ClientesFormViewModel { Imovels = imovel };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Clientes clientes)
        {

            await _clienteService.InsertAsync(clientes);

            return RedirectToAction(nameof(Index));

            //if (ModelState.IsValid)
            //{
            //    _context.Add(clientes);
            //    await _context.SaveChangesAsync();
            //    return RedirectToAction(nameof(Index));
            //}
            //return View(clientes);
        }

        //// GET: Clientes/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não fornecido" });
            }
            var obj = _clienteService.FindById(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não encontrado" });
            }
            return View(obj);
        }

        // GET: Clientes/Create


        //// GET: Clientes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não fornecido" });
            }

            var obj = _clienteService.FindById(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não encontrado" });
            }
            List<Imovel> imovels = _imovelService.FindAll();
            ClientesFormViewModel viewModel = new ClientesFormViewModel { Clientes = obj, Imovels = imovels };
            return View(viewModel);
        }

        //// POST: Clientes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Clientes clientes)
        {
            if (id != clientes.Id)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não correspondente" });
            }

            try
            {
                _clienteService.Update(clientes);
                return RedirectToAction(nameof(Index));
            }
            catch (ApplicationException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });

            }            
        }

        //// GET: Clientes/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não fornecido" });
            }
            var obj = _clienteService.FindById(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não encontrado" });
            }
            return View(obj);
        }

        //// POST: Clientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _clienteService.Remove(id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Error(string message)
        {
            var viewModel = new ErrorViewModel
            {
                Message = message,
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };
            return View(viewModel);
        }
    }
}
