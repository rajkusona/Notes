using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Notes.Data;
using Notes.Models;

namespace Notes.Controllers
{
    public class NotesController : Controller
    {
        private readonly NotesDbContext _dbContext;
        private readonly ILogger<NotesController> _logger;

        public NotesController(NotesDbContext dbContext, ILogger<NotesController> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        // GET: Notes
        public async Task<IActionResult> Index()
        {
            var notes = await _dbContext.Notes
                .OrderByDescending(n => n.UpdatedAt)
                .ToListAsync();

            return View(notes);
        }

        // GET: Notes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Notes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,Content,Priority")] Note note)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    note.CreatedAt = DateTime.UtcNow;
                    note.UpdatedAt = DateTime.UtcNow;

                    _dbContext.Add(note);
                    await _dbContext.SaveChangesAsync();

                    _logger.LogInformation("Note created with ID: {NoteId}", note.Id);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error creating note");
                    ModelState.AddModelError("", "An error occurred while creating the note.");
                }
            }

            return View(note);
        }

        // GET: Notes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var note = await _dbContext.Notes.FindAsync(id);
            if (note == null)
            {
                return NotFound();
            }

            return View(note);
        }

        // POST: Notes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Content,Priority,CreatedAt")] Note note)
        {
            if (id != note.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var existingNote = await _dbContext.Notes.FindAsync(id);
                    if (existingNote == null)
                    {
                        return NotFound();
                    }

                    existingNote.Title = note.Title;
                    existingNote.Content = note.Content;
                    existingNote.Priority = note.Priority;
                    existingNote.UpdatedAt = DateTime.UtcNow;

                    _dbContext.Update(existingNote);
                    await _dbContext.SaveChangesAsync();

                    _logger.LogInformation("Note updated with ID: {NoteId}", id);
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    _logger.LogError(ex, "Concurrency error updating note");
                    ModelState.AddModelError("", "The note was modified by another user. Please refresh and try again.");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error updating note");
                    ModelState.AddModelError("", "An error occurred while updating the note.");
                }
            }

            return View(note);
        }

        // GET: Notes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var note = await _dbContext.Notes.FirstOrDefaultAsync(m => m.Id == id);
            if (note == null)
            {
                return NotFound();
            }

            return View(note);
        }

        // POST: Notes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var note = await _dbContext.Notes.FindAsync(id);
            if (note != null)
            {
                _dbContext.Notes.Remove(note);
                await _dbContext.SaveChangesAsync();

                _logger.LogInformation("Note deleted with ID: {NoteId}", id);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
