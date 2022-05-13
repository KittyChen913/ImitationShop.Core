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
    public ActionResult<List<Item>> Get()
    {
        return Ok(itemsService.GetItemList());
    }

    [HttpGet("{itemId}")]
    public ActionResult<Item> Get(int itemId)
    {
        return Ok(itemsService.GetItem(itemId));
    }
}
