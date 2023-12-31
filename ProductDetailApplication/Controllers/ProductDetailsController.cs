﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductDetailApplication.Models;

namespace ProductDetailApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductDetailsController : ControllerBase
    {
        private readonly ProductDBContext _context;

        public ProductDetailsController(ProductDBContext context)
        {
            _context = context;
        }

        // GET: api/ProductDetails
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDetails>>> GetProductDetails()
        {
          if (_context.ProductDetails == null)
          {
              return NotFound();
          }
            return await _context.ProductDetails.ToListAsync();
        }

        // GET: api/ProductDetails/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDetails>> GetProductDetails(int id)
        {
          if (_context.ProductDetails == null)
          {
              return NotFound();
          }
            var productDetails = await _context.ProductDetails.FindAsync(id);

            if (productDetails == null)
            {
                return NotFound();
            }

            return productDetails;
        }


        // PUT: api/ProductDetails/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProductDetails(int id, [FromForm] ProductDetails productDetails)
        {

            _context.Entry(productDetails).State = EntityState.Modified;

            try
            {
                _context.ChangeTracker.Clear(); 
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductDetailsExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/ProductDetails
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ProductDetails>> PostProductDetails([FromForm] ProductDetails productDetails)
        {
          if (_context.ProductDetails == null)
          {
              return Problem("Entity set 'ProductDBContext.ProductDetails'  is null.");
          }
            string wwwRootPath = @"C:\Users\dell\Desktop\ProductImages";
            string fileName = Path.GetFileNameWithoutExtension(productDetails.ImageFile.FileName);
            string extension = Path.GetExtension(productDetails.ImageFile.FileName);
            productDetails.Image  = fileName + DateTime.Now.ToString("yymmssfff") + extension;
            string path = Path.Combine(wwwRootPath + "/Image/", productDetails.Image);
            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                await productDetails.ImageFile.CopyToAsync(fileStream);
            }
            _context.ProductDetails.Add(productDetails);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProductDetails", new { id = productDetails.ProductId }, productDetails);
        }

        // DELETE: api/ProductDetails/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductDetails(int id)
        {
            if (_context.ProductDetails == null)
            {
                return NotFound();
            }
            var productDetails = await _context.ProductDetails.FindAsync(id);
            if (productDetails == null)
            {
                return NotFound();
            }

            _context.ProductDetails.Remove(productDetails);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductDetailsExists(int id)
        {
            return (_context.ProductDetails?.Any(e => e.ProductId == id)).GetValueOrDefault();
        }
    }
}
