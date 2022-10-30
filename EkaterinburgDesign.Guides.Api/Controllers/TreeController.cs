using EkaterinburgDesign.Guides.Api.Database;
using Microsoft.AspNetCore.Mvc;

namespace EkaterinburgDesign.Guides.Api.Controllers;

[ApiController]
[Route("api/tree")]
public class TreeController : ControllerBase
{
    private readonly PostgresDb postgresDb;

    public TreeController(PostgresDb postgresDb)
    {
        this.postgresDb = postgresDb;
    }

    [HttpGet(Name = "GetPagesTree")]
    public IActionResult Get()
    {
        try
        {
            postgresDb.TestMethod();
        }
        catch (Exception e)
        {
            return BadRequest(e.ToString());
        }

        return Ok("YASSS");
    }
}