# ?? Troubleshooting & FAQ

## Common Issues & Solutions

### 1. Database Connection Issues

#### Problem: "Cannot connect to (localdb)\mssqllocaldb"

**Solution:**
```bash
# Check if LocalDB is installed
sqllocaldb info

# Start LocalDB
sqllocaldb start mssqllocaldb

# Create LocalDB instance if needed
sqllocaldb create mssqllocaldb
```

#### Problem: Database not created on startup

**Solution:**
1. Verify connection string in `appsettings.json`
2. Check Program.cs has migration logic
3. Run migrations manually: `dotnet ef database update`

### 2. Migration Issues

#### Problem: "Add-Migration: Cannot find the 'obj/Release' directory"

**Solution:**
```bash
dotnet clean
dotnet build
dotnet ef migrations add InitialCreate
```

#### Problem: "Migration already applied"

**Solution:**
```bash
dotnet ef migrations list
dotnet ef migrations remove
dotnet ef migrations add NameOfMigration
dotnet ef database update
```

### 3. Build & Compilation Issues

#### Problem: "The name 'NotesDbContext' does not exist"

**Solution:**
1. Check using statement: `using Notes.Data;`
2. Verify DbContext registered in Program.cs

#### Problem: "Package not found"

**Solution:**
```bash
dotnet restore
```

### 4. UI/UX Issues

#### Problem: Styling not applied

**Solution:**
1. Check Bootstrap CDN in _Layout.cshtml
2. Verify wwwroot/lib exists
3. Ensure staticfiles middleware: `app.UseStaticFiles();`

#### Problem: Form not submitting

**Solution:**
1. Verify form method is POST
2. Check anti-forgery token
3. Verify form fields match model properties

## Frequently Asked Questions

### Q: How do I change the port number?

**A:** Edit `Properties/launchSettings.json`

### Q: How do I use a different database?

**A:** Install appropriate EF Core provider and update Program.cs:
```bash
# PostgreSQL
dotnet add package Npgsql.EntityFrameworkCore.PostgreSQL

# MySQL
dotnet add package Pomelo.EntityFrameworkCore.MySql

# SQLite
dotnet add package Microsoft.EntityFrameworkCore.Sqlite
```

### Q: How do I reset the database?

**A:**
```bash
dotnet ef database drop
dotnet ef database update
```

### Q: Why is my application slow?

**A:**
1. Check database indexes
2. Use async/await for I/O
3. Profile your application
4. Enable query logging to see SQL

### Q: How do I add validation rules?

**A:** Add DataAnnotations to model:
```csharp
public class Note
{
    [Required]
    [StringLength(200, MinimumLength = 3)]
    public string Title { get; set; } = string.Empty;
}
```

### Q: Can I customize the colors?

**A:** Yes! Edit in Note.cs GetPriorityColor() method

### Q: How do I enable detailed logging?

**A:** Edit `appsettings.json`:
```json
"Logging": {
  "LogLevel": {
    "Default": "Debug",
    "Microsoft.EntityFrameworkCore": "Debug"
  }
}
```

---

## Debugging Tips

### 1. Use Breakpoints
- Set breakpoint in Visual Studio
- Press F5 to debug

### 2. Watch Variables
- Monitor values during execution
- Identify incorrect values

### 3. View Database
- Start SQL Server Management Studio
- Connect to LocalDB
- Query data directly

### 4. Enable Application Logging
```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Debug"
    }
  }
}
```

### 5. View HTTP Requests
- Press F12 in browser
- Go to Network tab
- Monitor all requests/responses

---

**Happy debugging! ??**
