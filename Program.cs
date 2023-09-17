using System.Net.NetworkInformation;
using System.Net;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.MapGet("/{ip}", async (string ip) =>
{
    using Ping ping = new Ping();
    try
    {
        var reply = await ping.SendPingAsync(ip, 5000);
        if (reply.Status == IPStatus.Success)
        {
            return 1;
        }
        else
        {
            return 0;
        }
    }
    catch (PingException)
    {
        return 0;
    }

});
app.Run();
