﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using sistema_vendas_web.Data;
using sistema_vendas_web.Models;

namespace sistema_vendas_web.Pages.Private.Usuarios
{
    public class EditModel : PageModel
    {
        private readonly sistema_vendas_web.Data.VendasContext _context;

        public EditModel(sistema_vendas_web.Data.VendasContext context)
        {
            _context = context;
        }

         
        [BindProperty]
        public Usuario Usuario { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            
            Usuario = await _context.Usuarios.FirstOrDefaultAsync(m => m.COD_USUARIO == id);

            if (Usuario == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Usuario).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsuarioExists(Usuario.COD_USUARIO))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool UsuarioExists(int id)
        {
            return _context.Usuarios.Any(e => e.COD_USUARIO == id);
        }
    }
}
