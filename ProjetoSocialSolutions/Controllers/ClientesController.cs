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
        public async Task <IActionResult> Index()
        {
            var list = _clienteService.FindAllAsync();
            return View(list);
        }

        public async Task<IActionResult>  Create()
        {
            var imovel = await _imovelService.FindAllAsync();
            var viewModel = new ClientesFormViewModel { Imovels = imovel };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Clientes clientes)
        {
            if (!ModelState.IsValid)
            {
                var imovel = await _imovelService.FindAllAsync();
                var viewModel = new ClientesFormViewModel { Clientes = clientes, Imovels = imovel };
                return View(clientes);
            }
            await _clienteService.InsertAsync(clientes);

            return RedirectToAction(nameof(Index));          
        }

        //// GET: Clientes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não fornecido" });
            }
            var obj = await _clienteService.FindByIdAsync(id.Value);
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

            var obj = await _clienteService.FindByIdAsync(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não encontrado" });
            }
            List<Imovel> imovels = await _imovelService.FindAllAsync();
            ClientesFormViewModel viewModel = new ClientesFormViewModel { Clientes = obj, Imovels = imovels };
            return View(viewModel);
        }

        //// POST: Clientes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Clientes clientes)
        {
            if (!ModelState.IsValid)
            {
                var imovel = await _imovelService.FindAllAsync();
                var viewModel = new ClientesFormViewModel { Clientes= clientes, Imovels = imovel };
                return View(clientes);
            }
            if (id != clientes.Id)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não correspondente" });
            }

            try
            {
                 await _clienteService.UpdateAsync(clientes);
                return RedirectToAction(nameof(Index));
            }
            catch (ApplicationException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });

            }            
        }

        //// GET: Clientes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não fornecido" });
            }
            var obj = await _clienteService.FindByIdAsync(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não encontrado" });
            }
            return View(obj);
        }

        //// POST: Clientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _clienteService.RemoveAsync(id);
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
