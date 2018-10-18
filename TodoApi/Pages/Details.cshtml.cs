﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TodoApi.Models;

namespace TodoApi.Pages
{
    public class DetailsModel : PageModel
    {
        private readonly TodoApi.Models.TodoContext _context;

        public DetailsModel(TodoApi.Models.TodoContext context)
        {
            _context = context;
        }

        public TodoItem TodoItem { get; set; }

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            TodoItem = await _context.TodoItems.FirstOrDefaultAsync(m => m.Id == id);

            if (TodoItem == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
