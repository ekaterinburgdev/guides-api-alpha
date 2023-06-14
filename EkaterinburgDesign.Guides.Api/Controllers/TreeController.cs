using EkaterinburgDesign.Guides.Api.Common.ApplicationOptions;
using EkaterinburgDesign.Guides.Api.Database;
using EkaterinburgDesign.Guides.Api.Database.models;
using EkaterinburgDesign.Guides.Api.Notion;
using Microsoft.AspNetCore.Mvc;
using Notion.Client;

namespace EkaterinburgDesign.Guides.Api.Controllers;

[ApiController]
[Route("api/tree")]
public class TreeController : ControllerBase
{
    private readonly PostgresContextProvider PostgresContextProvider;
    private readonly INotionCacher NotionCacher;
    private readonly NotionCredentials NotionCredentials;

    public TreeController(
        PostgresContextProvider postgresContextProvider,
        INotionCacher notionCacher,
        NotionCredentials notionCredentials)
    {
        PostgresContextProvider = postgresContextProvider;
        NotionCacher = notionCacher;
        NotionCredentials = notionCredentials;
    }

    [HttpGet(Name = "GetPagesTree")]
    public async Task<IActionResult> Get()
    {
        try
        {
            await NotionCacher.CachePageAsync(NotionCredentials.Pages.First().ToString());

            return Ok(CreateEntity());
        }
        catch (Exception e)
        {
            return BadRequest(e.ToString());
        }
    }

    private List<PageElement> CreateEntity()
    {
        using var db = PostgresContextProvider();

        var test = new PageElement
        {
            Content = new Random().NextInt64().ToString()
        };

        db.PageElements.Add(test);
        db.SaveChanges();

        return db.PageElements.ToList();
    }
}