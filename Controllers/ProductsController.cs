
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProductSupplierMVC.Data;
using ProductSupplierMVC.Models;

namespace ProductSupplierMVC.Controllers
{
    public class ProductsController : Controller
    {
        private readonly AppDbContext _context;

        public ProductsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            var products = await _context.Products
                .Include(p => p.Supplier)
                .AsNoTracking()
                .ToListAsync();

            return View(products);
        }

        // Lấy danh sách nhà cung cấp cho dropdown
        private async Task LoadSuppliersAsync(
            int? selectedId = null)
        {
            var suppliers = await _context.Suppliers
                .OrderBy(s => s.Name)
                .ToListAsync();

            ViewBag.SupplierId = new SelectList(
                suppliers, "Id", "Name", selectedId);
        }

        // GET: Products/Create
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            await LoadSuppliersAsync();
            return View(new Product());
        }

        // POST: Products/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("Name,Price,Quantity,Category,SupplierId")]
            Product product)
        {
            if (product.SupplierId > 0 &&
                !await _context.Suppliers.AnyAsync(
                    s => s.Id == product.SupplierId))
            {
                ModelState.AddModelError(
                    "SupplierId", "Nhà cung cấp không tồn tại");
            }

            if (ModelState.IsValid)
            {
                _context.Products.Add(product);
                await _context.SaveChangesAsync();

                TempData["Success"] =
                    "Thêm sản phẩm thành công!";

                return RedirectToAction(nameof(Index));
            }

            await LoadSuppliersAsync(product.SupplierId);
            return View(product);
        }

        // GET: Products/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var product = await _context.Products.FindAsync(id);

            if (product == null)
                return NotFound();

            await LoadSuppliersAsync(product.SupplierId);
            return View(product);
        }

        // POST: Products/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            int id,
            [Bind("Id,Name,Price,Quantity,Category,SupplierId")]
            Product product)
        {
            if (id != product.Id)
                return NotFound();

            if (product.SupplierId > 0 &&
                !await _context.Suppliers.AnyAsync(
                    s => s.Id == product.SupplierId))
            {
                ModelState.AddModelError(
                    "SupplierId", "Nhà cung cấp không tồn tại");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Products.Update(product);
                    await _context.SaveChangesAsync();

                    TempData["Success"] =
                        "Cập nhật sản phẩm thành công!";
                }
                catch (DbUpdateConcurrencyException)
                {
                    bool exists = await _context.Products
                        .AnyAsync(p => p.Id == product.Id);

                    if (!exists)
                        return NotFound();

                    throw;
                }

                return RedirectToAction(nameof(Index));
            }

            await LoadSuppliersAsync(product.SupplierId);
            return View(product);
        }

        // GET: Products/Delete/5
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var product = await _context.Products
                .Include(p => p.Supplier)
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == id);

            if (product == null)
                return NotFound();

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null)
                return NotFound();

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            TempData["Success"] =
                "Xóa sản phẩm thành công!";

            return RedirectToAction(nameof(Index));
        }
    }
}
