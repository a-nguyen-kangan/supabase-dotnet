
using static Supabase.Gotrue.Constants;

var url = "https://uioaemhkajajdgxqskwv.supabase.co";
var key = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6InVpb2FlbWhrYWphamRneHFza3d2Iiwicm9sZSI6ImFub24iLCJpYXQiOjE2OTM4NzM5MjIsImV4cCI6MjAwOTQ0OTkyMn0.UhssrleA4-TDRaeAdWSGM3h2mEeFdjUN9VBprrQ5JyI";


var options = new Supabase.SupabaseOptions
{
    AutoConnectRealtime = true
};

var supabase = new Supabase.Client(url, key, options);
Supabase.Gotrue.Session? session = null; // = await supabase.Auth.SignIn("someone@people.net", "abcd1234");


var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => SupabaseGetTest());
app.MapGet("/logout", () => { supabase.Auth.SignOut(); return "Logged out";  });
app.MapGet("/login", async () => { session = await supabase.Auth.SignIn("someone@people.net", "abcd1234");});
app.MapGet("/getSession", () => supabase.Auth.CurrentSession );
app.MapPost("/setSession", (Supabase.Gotrue.Session session1) =>  supabase.Auth.SetSession(session1.AccessToken, session1.RefreshToken, false) );

app.Run();

async void SupabaseGetTest() {
    try {
        if(supabase.Auth.CurrentSession == null) {
            Console.WriteLine("No current session");
            //session = await supabase.Auth.SignIn("someone@people.net", "abcd1234");
        } 
        var result = await supabase.From<dotnetTest>().Get();
        var dotnetTests = result.Models;
        Console.WriteLine(dotnetTests[0].Name);
    } catch (Exception e) {
        Console.WriteLine("error: " + e.Message);
    }
}