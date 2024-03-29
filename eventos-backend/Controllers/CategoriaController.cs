﻿using eventos_backend.Exceptions;
using eventos_backend.Models;
using eventos_backend.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eventos_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private readonly ICategoriaService _categoriaService;
        public CategoriaController(ICategoriaService categoriaService) { 
        
            _categoriaService = categoriaService;
        }

        [HttpGet] 
        public async Task<IActionResult> GetAll()
        {
            List<Categoria> categorias = await _categoriaService.GetAll();
            return Ok(categorias);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            Categoria categoria = await _categoriaService.GetById(id);
            return Ok(categoria);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Categoria categoria)
        {
            ValidaleModel();
            categoria = await _categoriaService.CreateCategoria(categoria);

            return Ok(categoria);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody]Categoria categoria)
        {
            ValidaleModel();
            categoria = await _categoriaService.UpdateCategoria(categoria);
            return Ok(categoria);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            ValidaleModel();
            string msg = await _categoriaService.DeleteCategoria(id);
            return Ok(msg);
        }

        private void ValidaleModel()
        {
            if (!ModelState.IsValid)
            {
                List<string> errors = ModelState.Select(x => x.Value.Errors.FirstOrDefault().ErrorMessage).ToList();
                throw new AppException(errors, 400);
            }
        }

    }
}
