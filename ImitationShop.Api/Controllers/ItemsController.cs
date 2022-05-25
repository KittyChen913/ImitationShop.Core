namespace ImitationShop.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ItemsController : ControllerBase
{
    private readonly IItemsService itemsService;

    public ItemsController(IItemsService itemsService)
    {
        this.itemsService = itemsService;
    }

    [HttpGet]
    public async Task<ActionResult<List<Item>>> Get()
    {
        return Ok(await itemsService.GetItemList());
    }

    [HttpGet("{itemId}")]
    public async Task<ActionResult<Item>> Get(int itemId)
    {
        return Ok(await itemsService.GetItem(itemId));
    }
}
