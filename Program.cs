using LabyonetimSistemi.Data;
using LabyonetimSistemi.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Seed: Admin kullanýcýsý yoksa oluþtur
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureCreated();
    if (!db.Users.Any(u => u.Role == "Admin"))
    {
        db.Users.Add(new User { Username = "admin", Password = "admin123", Role = "Admin" });
        db.SaveChanges();
    }
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseDefaultFiles();
app.UseStaticFiles();

// ==================== AUTH ====================
app.MapPost("/api/login", async (User loginUser, AppDbContext db) =>
{
    var user = await db.Users.FirstOrDefaultAsync(
        u => u.Username == loginUser.Username && u.Password == loginUser.Password);
    if (user == null) return Results.BadRequest("Hatalý kullanýcý adý veya þifre!");
    return Results.Ok(new { user.Id, user.Username, user.Role });
});

// ==================== LABS ====================
app.MapGet("/api/labs", async (AppDbContext db) =>
    await db.Labs.Include(l => l.Computers).ToListAsync());

app.MapPost("/api/admin/labs", async (Lab lab, AppDbContext db) =>
{
    db.Labs.Add(lab);
    await db.SaveChangesAsync();
    return Results.Ok(lab);
});

app.MapPut("/api/admin/labs/{id}", async (int id, Lab updated, AppDbContext db) =>
{
    var lab = await db.Labs.FindAsync(id);
    if (lab == null) return Results.NotFound();
    lab.Name = updated.Name;
    await db.SaveChangesAsync();
    return Results.Ok(lab);
});

// ==================== COMPUTERS ====================
app.MapGet("/api/computers", async (AppDbContext db) =>
    await db.Computers.ToListAsync());

app.MapPost("/api/admin/computers", async (Computer pc, AppDbContext db) =>
{
    var pcCount = await db.Computers.CountAsync(x => x.LabId == pc.LabId) + 1;
    pc.AssetCode = $"LAB{pc.LabId}-PC-{pcCount:D2}";
    db.Computers.Add(pc);
    await db.SaveChangesAsync();
    return Results.Ok(pc);
});

app.MapPut("/api/admin/computers/{id}", async (int id, Computer updated, AppDbContext db) =>
{
    var pc = await db.Computers.FindAsync(id);
    if (pc == null) return Results.NotFound();
    pc.Brand = updated.Brand;
    pc.Processor = updated.Processor;
    pc.Ram = updated.Ram;
    pc.HasHdmi = updated.HasHdmi;
    pc.HasInternet = updated.HasInternet;
    pc.HasVeyon = updated.HasVeyon;
    await db.SaveChangesAsync();
    return Results.Ok(pc);
});

// ==================== STUDENTS (Zimmet Atama) ====================
app.MapGet("/api/admin/students", async (AppDbContext db) =>
    await db.Students.ToListAsync());

app.MapPost("/api/admin/assign", async (Student student, AppDbContext db) =>
{
    // 1. Öðrenciyi kaydet
    db.Students.Add(student);
    await db.SaveChangesAsync();

    // 2. Otomatik kullanýcý hesabý oluþtur
    var user = new User
    {
        Username = student.StudentNumber,
        Password = student.StudentNumber, // Ýlk þifre = öðrenci no
        Role = "Student"
    };
    db.Users.Add(user);
    await db.SaveChangesAsync();

    // 3. Öðrenciye userId baðla
    student.UserId = user.Id;
    await db.SaveChangesAsync();

    return Results.Ok(new { student, user.Username });
});

// ==================== ÖÐRENCÝ PORTALÝ ====================
app.MapGet("/api/student/my-pc/{username}", async (string username, AppDbContext db) =>
{
    var student = await db.Students.FirstOrDefaultAsync(s => s.StudentNumber == username);
    if (student == null) return Results.NotFound("Zimmet bulunamadý.");
    var pc = await db.Computers.FindAsync(student.ComputerId);
    return pc != null ? Results.Ok(pc) : Results.NotFound("Bilgisayar bulunamadý.");
});

app.Run();