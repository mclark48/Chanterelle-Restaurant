using restaurant_project.Components;
using restaurant_project.Services;

var builder = WebApplication.CreateBuilder(args);

// adds services to container -@mclark48
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// adds custom services. notice they are different. i forgot and it broke things.-@mclark48
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<UserSession>();

//actually builds the app - do not alter -@mclark48
var app = builder.Build();

// HTTP request - @mclark48
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseAntiforgery();
app.MapStaticAssets();

//Runs the program. -@mclark48
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();