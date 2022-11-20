using EkaterinburgDesign.Guides.Api.Database;
using EkaterinburgDesign.Guides.Api.Database.models;
using Microsoft.AspNetCore.Mvc;

namespace EkaterinburgDesign.Guides.Api.Controllers;

[ApiController]
[Route("api/tree")]
public class TreeController : ControllerBase
{
    private readonly PostgresContextProvider postgresContextProvider;

    public TreeController(PostgresContextProvider postgresContextProvider)
    {
        this.postgresContextProvider = postgresContextProvider;
    }

    [HttpGet(Name = "GetPagesTree")]
    public IActionResult Get()
    {
        try
        {
            return Ok(CreateEntity());
        }
        catch (Exception e)
        {
            return BadRequest(e.ToString());
        }
    }

    private List<TestEntity> CreateEntity()
    {
        using var db = postgresContextProvider();

        var test = new TestEntity
        {
            Data = new Random().NextInt64().ToString()
        };

        db.TestEntities.Add(test);

        db.SaveChanges();
        
        return db.TestEntities.ToList();
    }
}