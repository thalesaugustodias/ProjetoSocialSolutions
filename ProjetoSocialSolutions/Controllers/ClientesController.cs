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
        public async Task<IActionResult> Index()
        {
            var list = await _clienteService.FindAllAsync();
            return View(list);                      
        }

        public async Task<IActionResult> Create()
        {
            var imovel = await _imovelService.FindAllAsync();           
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
        public async Task <IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var obj = await _clienteService.FindByIdAsync(id.Value);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }

        // GET: Clientes/Create


        //// GET: Clientes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var obj = await _clienteService.FindByIdAsync(id.Value);
            if (obj == null)
            {
                return NotFound();
            }
            List<Imovel> imovels = await _imovelService.FindAllAsync();
            ClientesFormViewModel viewModel = new ClientesFormViewModel { Clientes = obj, Imovels = imovels };
            return View(viewModel);
        }

        //// POST: Clientes/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Clientes clientes)
        {
            if (id != clientes.Id)
            {
                return NotFound();
            }

            try
            {
                await _clienteService.UpdateAsync(clientes);
                return RedirectToAction(nameof(Index));
            }
            catch (NotFoundException)
            {
                return NotFound();
            } catch(DbConcurrencyException)
            {
                return BadRequest();
            }
    }

    //// GET: Clientes/Delete/5
    public async Task <IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }
        var obj = await _clienteService.FindByIdAsync(id.Value);
        if (obj == null)
        {
            return NotFound();
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
    }
}
