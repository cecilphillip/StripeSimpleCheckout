using Stripe;
using Stripe.Checkout;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors(
    opts => opts.AddDefaultPolicy(policyBuilder => policyBuilder.AllowAnyOrigin().AllowAnyMethod())
);
var app = builder.Build();

StripeConfiguration.ApiKey = app.Configuration["Stripe:STRIPE_SECRET_KEY"];

var userShoppingCart = new Product[]
{
    new(1, "price_1KS3twBV4FWmvbdsJylhx5fK", new Uri("https://fakestoreapi.com/img/81fPKd-2AYL._AC_SL1500_.jpg"),
        "Fjallraven - Foldsack No. 1 Backpack",
        "Your perfect pack for everyday use and walks in the forest. Stash your laptop (up to 15 inches) in the padded sleeve, your everyday backpack.",
        "Navy", 1, 10995),

    new(2, "price_1KS3vABV4FWmvbds4l4XVgwK", new Uri("https://fakestoreapi.com/img/71z3kpMAYsL._AC_UY879_.jpg"),
        "MBJ Women's Solid Short Sleeve Boat Neck V",
        "Made in USA. Lightweight fabric with great stretch for comfort. Ribbed on sleeves and neckline. Double stitching on bottom hem.",
        "White", 2, 985
    )
};

app.UseCors();

if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}

app.MapGet("/shopping-cart", () => Results.Ok(userShoppingCart));

app.MapPost("/create-checkout-session", async (HttpRequest request) =>
{
    var baseUrl = $"{request.Scheme}://{request.Host}";

    var lineItems = userShoppingCart.Select(p => new {Price = p.StripePriceId, Quantity = p.Quantity});

    var sessionOptions = new SessionCreateOptions
    {
        Mode = "payment",
        LineItems = new()
        {
            new() {Price = userShoppingCart[0].StripePriceId, Quantity = userShoppingCart[0].Quantity},
            new() {Price = userShoppingCart[1].StripePriceId, Quantity = userShoppingCart[1].Quantity},
        },
        SuccessUrl = baseUrl + "/success?session_id={CHECKOUT_SESSION_ID}",
        CancelUrl = baseUrl + "/cancel"
    };
    var checkoutSerice = new SessionService();
    var session = await checkoutSerice.CreateAsync(sessionOptions);
    return Results.Ok(new {session.Url});
});


app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();
app.MapFallbackToFile("index.html");
app.Run();