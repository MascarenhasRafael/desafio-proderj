using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Timers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using desafio_proderj.Data;
using desafio_proderj.Models;
using Microsoft.AspNetCore.Hosting;

namespace desafio_proderj.Controllers
{
    public class NewsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public NewsController(ApplicationDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            this._hostEnvironment = hostEnvironment;
        }

        // GET: News
        public async Task<IActionResult> Index()
        {
            return View(await _context.New.ToListAsync());
        }

        // GET: News/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @new = await _context.New
                .FirstOrDefaultAsync(m => m.ID == id);
            if (@new == null)
            {
                return NotFound();
            }

            return View(@new);
        }

        // GET: News/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: News/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Title,Content,ImageFile, ImageName")] New @new)
        {
            if (ModelState.IsValid)
            {
                if (@new.ImageFile != null) {
                    string assetsPath = _hostEnvironment.WebRootPath;
                    string fileName = Path.GetFileNameWithoutExtension(@new.ImageFile.FileName);
                    string extension = Path.GetExtension(@new.ImageFile.FileName);

                    fileName = fileName + DateTime.Now.ToString("ddmmyyyyssfff") + extension;
                    string imagePath = Path.Combine(assetsPath, "image", fileName);


                    @new.ImageName = fileName;
                    using (var fileStream = new FileStream(imagePath, FileMode.Create))
                    {
                        await @new.ImageFile.CopyToAsync(fileStream);
                    }
                }

                @new.ImageName = @new.ImageName != null ? @new.ImageName : "";

                _context.Add(@new);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(@new);
        }

        // GET: News/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @new = await _context.New.FindAsync(id);
            if (@new == null)
            {
                return NotFound();
            }
            return View(@new);
        }

        // POST: News/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Title,Content,ImageFile,ImageName")] New @new)
        {

            if (id != @new.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (@new.ImageFile != null) {

                        string assetsPath = _hostEnvironment.WebRootPath;
                        string fileName = Path.GetFileNameWithoutExtension(@new.ImageFile.FileName);
                        string extension = Path.GetExtension(@new.ImageFile.FileName);

                        fileName = fileName + DateTime.Now.ToString("ddmmyyyyssfff") + extension;
                        string imagePath = Path.Combine(assetsPath, "image", fileName);

                        if(@new.ImageName != null && System.IO.File.Exists(imagePath)) {
                            string oldFilePath = Path.Combine(assetsPath, "image", @new.ImageName);
                            System.IO.File.Delete(oldFilePath);
                        }

                        @new.ImageName = fileName;
                        using (var fileStream = new FileStream(imagePath, FileMode.Create))
                        {
                            await @new.ImageFile.CopyToAsync(fileStream);
                        }
                    }

                    _context.Update(@new);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NewExists(@new.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(@new);
        }

        // GET: News/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @new = await _context.New
                .FirstOrDefaultAsync(m => m.ID == id);
            if (@new == null)
            {
                return NotFound();
            }

            return View(@new);
        }

        // POST: News/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var @new = await _context.New.FindAsync(id);

            string assetsPath = _hostEnvironment.WebRootPath;
            string imagePath = Path.Combine(assetsPath, "image", @new.ImageName);

            if (System.IO.File.Exists(imagePath)) {
                System.IO.File.Delete(imagePath);
            }

            _context.New.Remove(@new);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NewExists(int id)
        {
            return _context.New.Any(e => e.ID == id);
        }
    }
}
