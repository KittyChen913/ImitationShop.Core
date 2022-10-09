namespace ImitationShop.Api.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class StoreController : ControllerBase
{
    private readonly IStoreService storeService;
    private readonly IItemsService itemsService;

    public StoreController(IStoreService storeService, IItemsService itemsService)
    {
        this.storeService = storeService;
        this.itemsService = itemsService;
    }

    [HttpGet("{userId}")]
    public async Task<ActionResult<IEnumerable<Store>>> Get(int userId)
    {
        return Ok(await storeService.GetStoreItemList(userId));
    }


    [HttpPost]
    [Route("AddStoreItem")]
    public async Task<ActionResult<BaseResponseModel<int>>> AddStoreItem(BaseRequestModel<AddStoreItemModel> model)
    {
        var itemId = await itemsService.AddItem(model.Data.Item);
        model.Data.StoreMappingInfo.ItemId = itemId;
        var storeId = await storeService.AddStoreItem(model.Data.StoreMappingInfo);

        return Ok(new BaseResponseModel<int>
        {
            RequestId = model.RequestId,
            ErrorCode = ErrorCodeEnum.Success.ToDescription(),
            Data = storeId
        });
    }

    [HttpPost]
    [Route("DeleteStoreItem")]
    public async Task<ActionResult<BaseResponseModel<int>>> DeleteStoreItem(BaseRequestModel<Store> model)
    {
        if (await storeService.DeleteStoreItem(model.Data))
        {
            if (await itemsService.DeleteItem(model.Data.ItemId))
            {
                return Ok(new BaseResponseModel<int>
                {
                    RequestId = model.RequestId,
                    ErrorCode = ErrorCodeEnum.Success.ToDescription(),
                    Data = model.Data.ItemId
                });
            }
        }

        return BadRequest(new BaseResponseModel<object>
        {
            RequestId = model.RequestId,
            ErrorCode = ErrorCodeEnum.OtherSystemError.ToDescription(),
            ErrorMessage = "This item deletion failed, please try again.",
            Data = 0
        });
    }
}
