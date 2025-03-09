using HistoryTable.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Npgsql;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddScoped<EntityChangedInterceptor>();
var source = new NpgsqlDataSourceBuilder(builder.Configuration.GetConnectionString("Postgres"));
source.MapEnum<ArchiveEvent>();
builder.Services.AddDbContext<DataContext>((sp, builder) =>
 {
     builder.UseNpgsql(source.Build(), options =>
     {

     }).AddInterceptors(sp.GetRequiredService<EntityChangedInterceptor>());

 });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();


app.MapGet("/archives", async (DataContext dataContext, CancellationToken cancellationToken) =>
{

    var achives = await dataContext.Archives.ToListAsync(cancellationToken);
    return Results.Ok(achives);
});

app.MapGet("/usercourses", async (DataContext dataContext, CancellationToken cancellationToken) =>
{

    var UserCourses = await dataContext.UserCourse.ToListAsync(cancellationToken);
    return Results.Ok(UserCourses);
});

app.MapGet("/courses", async (DataContext dataContext, CancellationToken cancellationToken) =>
{

    var courses = await dataContext.Courses.ToListAsync(cancellationToken);
    return Results.Ok(courses);
});

app.MapPost("/courses", async ([FromBody] AddCourseRequest request, DataContext dataContext, CancellationToken cancellationToken) =>
{
    var course = new Course()
    {
        Title = request.Title,
        Description = request.Description,
        AvailableSpots = request.AvailableSpots,
        DownloadingEnabled = request.DownloadingEnabled,
        CreatedAt = DateTime.UtcNow
    };
    dataContext.Courses.Add(course);
    await dataContext.SaveChangesAsync(cancellationToken);
    return Results.Created($"/orders/{course.Id}", course);
});
app.MapPut("/courses/{courseId:int}", async (int courseId, [FromBody] AddCourseRequest request, DataContext dataContext, CancellationToken cancellationToken) =>
{
    var course = await dataContext.Courses.FindAsync([courseId], cancellationToken);
    if (course is null)
    {
        return Results.NotFound();
    }
    course.Title = request.Title;
    course.Description = request.Description;
    course.DownloadingEnabled = request.DownloadingEnabled;
    course.AvailableSpots = request.AvailableSpots;

    await dataContext.SaveChangesAsync(cancellationToken);
    return Results.Ok(course);
});
app.MapPost("/user/courses/{courseId:int}", async (int courseId, DataContext dataContext, CancellationToken cancellationToken) =>
{

    var course = await dataContext.Courses.FindAsync([courseId], cancellationToken);
    if (course is null)
    {
        return Results.NotFound();
    }
    UserCourse userCourse = new()
    {
        UserId = 1,
        CourseId = course.Id,
        DateEnrolled = DateTime.UtcNow,
    };
    dataContext.UserCourse.Add(userCourse);
    await dataContext.SaveChangesAsync(cancellationToken);
    return Results.Created($"/orders/courses/{course.Id}", userCourse);
});
app.MapDelete("/user/courses/{courseId:int}", async (int courseId, DataContext dataContext, CancellationToken cancellationToken) =>
{

    var userCourse = await dataContext.UserCourse.FindAsync([1, courseId], cancellationToken);
    if (userCourse is null)
    {
        return Results.NotFound();
    }
    dataContext.UserCourse.Remove(userCourse);

    await dataContext.SaveChangesAsync(cancellationToken);
    return Results.Ok("Removed");
});
app.Run();


record AddCourseRequest(string Title, string Description, int AvailableSpots, bool DownloadingEnabled);
