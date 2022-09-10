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
    public async Task<ActionResult<IEnumerable<Item>>> Get()
    {
        return Ok(await itemsService.GetItemList());
    }

    [HttpGet("{itemId}")]
    public async Task<ActionResult<Item>> Get(int itemId)
    {
        return Ok(await itemsService.GetItem(itemId));
    }

    [HttpPost]
    public async Task<ActionResult<BaseResponseModel<int>>> Post(BaseRequestModel<AddItemModel> model)
    {
        var itemId = await itemsService.AddItem(model.Data);

        return Ok(new BaseResponseModel<int>
        {
            RequestId = model.RequestId,
            ErrorCode = ErrorCodeEnum.Success.ToDescription(),
            Data = itemId
        });
    }

    [HttpPut("{itemId}")]
    public async Task<IActionResult> Put(int itemId, BaseRequestModel<Item> model)
    {
        if (await itemsService.UpdateItem(model.Data))
        {
            return Ok(new BaseResponseModel<object>
            {
                RequestId = model.RequestId,
                ErrorCode = ErrorCodeEnum.Success.ToDescription(),
                Data = itemId
            });
        }
        else
        {
            return BadRequest(new BaseResponseModel<object>
            {
                RequestId = model.RequestId,
                ErrorCode = ErrorCodeEnum.OtherSystemError.ToDescription(),
                ErrorMessage = "This item update failed."
            });
        }
    }
}
