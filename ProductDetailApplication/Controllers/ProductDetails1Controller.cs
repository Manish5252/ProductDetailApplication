using System;
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
    public class ProductDetails1Controller : ControllerBase
    {
        private readonly ProductDBContext _context;

        public ProductDetails1Controller(ProductDBContext context)
        {
            _context = context;
        }

        // GET: api/ProductDetails1
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDetails>>> GetProductDetails()
        {
          if (_context.ProductDetails == null)
          {
              return NotFound();
          }
            return await _context.ProductDetails.ToListAsync();
        }

        // GET: api/ProductDetails1/5
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

        // PUT: api/ProductDetails1/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProductDetails(int id, ProductDetails productDetails)
        {
            if (id != productDetails.ProductId)
            {
                return BadRequest();
            }

            _context.Entry(productDetails).State = EntityState.Modified;

            try
            {
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

        // POST: api/ProductDetails1
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ProductDetails>> PostProductDetails(ProductDetails productDetails)
        {
          if (_context.ProductDetails == null)
          {
              return Problem("Entity set 'ProductDBContext.ProductDetails'  is null.");
          }
            _context.ProductDetails.Add(productDetails);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProductDetails", new { id = productDetails.ProductId }, productDetails);
        }

        // DELETE: api/ProductDetails1/5
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
