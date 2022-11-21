using EkaterinburgDesign.Guides.Api.Common.ApplicationOptions;
using EkaterinburgDesign.Guides.Api.Database;
using EkaterinburgDesign.Guides.Api.Database.models;
using EkaterinburgDesign.Guides.Api.Notion;
using Microsoft.AspNetCore.Mvc;

namespace EkaterinburgDesign.Guides.Api.Controllers;

[ApiController]
[Route("api/tree")]
public class TreeController : ControllerBase
{
    private readonly PostgresContextProvider postgresContextProvider;
    private readonly INotionCacher notionCacher;
    private readonly NotionCredentials notionCredentials;

    public TreeController(PostgresContextProvider postgresContextProvider, INotionCacher notionCacher,
        NotionCredentials notionCredentials)
    {
        this.postgresContextProvider = postgresContextProvider;
        this.notionCacher = notionCacher;
        this.notionCredentials = notionCredentials;
    }

    [HttpGet(Name = "GetPagesTree")]
    public async Task<IActionResult> Get()
    {
        try
        {
            await notionCacher.CachePageAsync(notionCredentials.Pages.First().ToString());

            return Ok(CreateEntity());
        }
        catch (Exception e)
        {
            return BadRequest(e.ToString());
        }
    }

    private List<PageElement> CreateEntity()
    {
        using var db = postgresContextProvider();

        var test = new PageElement
        {
            Content = new Random().NextInt64().ToString()
        };

        db.PageElements.Add(test);

        db.SaveChanges();

        return db.PageElements.ToList();
    }
}