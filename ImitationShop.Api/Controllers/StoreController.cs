namespace ImitationShop.Api.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class StoreController : ControllerBase
{
    private readonly IStoreService storeService;

    public StoreController(IStoreService storeService)
    {
        this.storeService = storeService;
    }

    [HttpGet("{userId}")]
    public async Task<ActionResult<IEnumerable<Store>>> Get(int userId)
    {
        return Ok(await storeService.GetStoreItemList(userId));
    }
}
