# Quick Reference Guide - Notes Application

## ?? Getting Started (5 Minutes)

### 1. First Time Setup
```bash
cd Notes
dotnet restore
dotnet run
```
The database is automatically created on first run!

### 2. Access the Application
- URL: `https://localhost:5001`
- The app redirects to `/notes` automatically

### 3. Create Your First Note
- Click "New Note" button
- Fill in Title, Content, and Priority
- Click "Create Note"
- Done! Your note appears in the list

## ?? Feature Overview

### Creating Notes
- **Location**: `View/Notes/Create.cshtml`
- **Route**: `POST /notes/create`
- **Fields**: Title (required), Content (required), Priority (Low/Medium/High)

### Viewing Notes
- **Location**: `Views/Notes/Index.cshtml`
- **Route**: `GET /notes`
- **Features**: 
  - Grid layout with cards
  - Color-coded by priority
  - Sorted by most recent first
  - Shows preview of content
  - Creation and update timestamps

### Editing Notes
- **Location**: `Views/Notes/Edit.cshtml`
- **Route**: `POST /notes/edit/{id}`
- **Note**: Creation date preserved, only update date changes

### Deleting Notes
- **Location**: `Views/Notes/Delete.cshtml`
- **Route**: `POST /notes/delete/{id}`
- **Safety**: Confirmation required before deletion

## ?? Priority Color System

```
Low     ? ?? Green   (#90EE90)  ? badge-success
Medium  ? ?? Yellow  (#FFD700)  ? badge-warning
High    ? ?? Red     (#FF6B6B)  ? badge-danger
```

These colors appear as:
1. Left border of the note card
2. Priority badge in the header
3. Visual distinction in the list

## ?? Database Commands

### View Database
```bash
# Connect to LocalDB
sqlcmd -S (localdb)\mssqllocaldb

# List databases
SELECT name FROM sys.databases;

# Connect to NotesDb
USE NotesDb;

# View notes
SELECT * FROM Notes ORDER BY UpdatedAt DESC;
```

### Create New Migration (after model changes)
```bash
cd Notes
dotnet ef migrations add DescriptiveNameOfChange
dotnet ef database update
```

### View Migration History
```bash
dotnet ef migrations list
```

## ?? Code Structure

```
Controllers/
??? NotesController.cs
?   ??? Index()           ? GET /notes
?   ??? Create()          ? GET/POST /notes/create
?   ??? Edit()            ? GET/POST /notes/edit/{id}
?   ??? Delete()          ? GET/POST /notes/delete/{id}
??? HomeController.cs
    ??? Index()           ? Redirects to Notes

Models/
??? Note.cs
?   ??? Priority enum
?   ??? GetPriorityColor()
?   ??? GetPriorityBadgeClass()
??? ErrorViewModel.cs

Data/
??? NotesDbContext.cs
    ??? DbSet<Note> Notes

Views/
??? Notes/
    ??? Index.cshtml      ? List all
    ??? Create.cshtml     ? Create form
    ??? Edit.cshtml       ? Edit form
    ??? Delete.cshtml     ? Delete confirm
```

## ??? Common Tasks

### Add a new property to Note
1. Edit `Models/Note.cs` - Add property
2. Edit Views as needed
3. Create migration: `dotnet ef migrations add AddPropertyName`
4. Update database: `dotnet ef database update`

### Change priority colors
1. Edit `Models/Note.cs` - Update `GetPriorityColor()` method
2. Edit `wwwroot/css/site.css` - Update badge colors
3. Save and refresh browser

### Add note categories (future)
1. Add new properties to `Note.cs`
2. Update `NotesDbContext.cs` with new configuration
3. Create migration and update database
4. Add UI elements to views

## ?? Form Validation

**Title**: Required, max 200 characters
**Content**: Required, any length
**Priority**: Must be Low, Medium, or High

Validation occurs:
- Client-side: Browser validation before submission
- Server-side: Controller checks before saving
- Database: Constraints enforced at SQL level

## ?? Debugging

### Enable Detailed Logging
Edit `appsettings.json`:
```json
"Logging": {
  "LogLevel": {
    "Default": "Debug",
    "Microsoft.EntityFrameworkCore": "Debug"
  }
}
```

### View Application Logs
- Check Output window in Visual Studio
- Look for timestamps and error messages
- Check `ILogger` output in console

## ?? Security Features

? Anti-forgery tokens on all forms
? Server-side validation
? Parameterized queries (via EF Core)
? Safe date/time handling
? Not Found responses for invalid IDs

## ?? Responsive Design

The application works on:
- Desktop (1200px+)
- Tablet (768px - 1199px)
- Mobile (< 768px)

Bootstrap 5 grid system ensures proper layout on all devices.

## ? Performance Tips

- Notes are sorted by UpdatedAt index (fast queries)
- Async/await prevents blocking
- Database indexes for frequently queried columns
- Consider pagination for large note lists

## ?? Deployment Checklist

- [ ] Change connection string to production database
- [ ] Enable HTTPS only
- [ ] Set up error logging/monitoring
- [ ] Configure CORS if needed
- [ ] Add authentication/authorization
- [ ] Test database backups
- [ ] Configure automatic migrations on deploy
- [ ] Review security headers
- [ ] Set up CDN for static files
- [ ] Monitor application performance

## ?? Key Files Reference

| File | Purpose |
|------|---------|
| Program.cs | Configuration, DI, middleware |
| NotesDbContext.cs | Database configuration |
| NotesController.cs | Business logic |
| Note.cs | Data model |
| Index.cshtml | List view |
| Create.cshtml | Create form |
| Edit.cshtml | Edit form |
| Delete.cshtml | Delete confirmation |

## ?? Pro Tips

1. **Always use async/await** for database operations
2. **Add logging** to understand application flow
3. **Use migrations** for all schema changes
4. **Test locally first** before deploying
5. **Keep timestamps immutable** for audit trail
6. **Use color coding** for better UX
7. **Provide confirmation** for destructive actions
8. **Handle errors gracefully** with user messages

## ?? Troubleshooting

**Database won't create?**
- Check LocalDB is installed: `sqllocaldb info`
- Update connection string in appsettings.json
- Delete previous migrations and try again

**Port 5001 already in use?**
- Change port in launchSettings.json
- Or kill process: `netstat -ano | findstr :5001`

**Migrations fail?**
- Ensure database is accessible
- Check connection string
- Use `dotnet ef database drop` to reset

**Can't see changes?**
- Rebuild solution: `dotnet clean && dotnet build`
- Clear browser cache: Ctrl+Shift+Del
- Restart the application

---

**Happy note-taking! ??**
