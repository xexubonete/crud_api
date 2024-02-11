using ASP.NET_WebApi.Controllers;
using ASP.NET_WebApi.Entities;
using ASP.NET_WebApi.Interfaces;
using ASP.NET_WebApi.Persistence;
using ASP.NET_WebApi.Repositories;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.Identity.Client;
using static System.Reflection.Metadata.BlobBuilder;

namespace ASP.NET_WebApi.Tests.UnitTests;
public class UpdateClientByIdTest
{
    private ApiDbContext _context;

    private IClientRepository _clients;

    Client client = new Client()
    {
        Id = Guid.Parse("5a105781-ebb5-4dfd-a6c3-2727c20717a8"),
        Name = "Test",
        SurName = "One"
    };

    Client? nullClient;

    [SetUp]
    public void Init()
    {
        var options = new DbContextOptionsBuilder<ApiDbContext>().UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
        _context = new ApiDbContext(options);
        _clients = new ClientRepository(_context);
    }

    [Test]
    public void Should_Update_Client_By_Id()
    {
        var clientController = new ClientController(_clients);
        ;
        clientController.CreateClient(client);

        var result = clientController.UpdateClient(client);

        Assert.IsNotNull(result);
    }

    [Test]
    public void Should_Update_Client_ById_Return_Bad_Request()
    {
        var clientController = new ClientController(_clients);

        var result = clientController.CreateClient(nullClient);

        AssertionRequestOptions.Equals(result, typeof(BadRequest));
    }
}