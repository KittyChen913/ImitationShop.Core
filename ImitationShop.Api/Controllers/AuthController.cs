namespace ImitationShop.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService authService;
    private readonly IUserService userService;

    public AuthController(IAuthService authService, IUserService userService)
    {
        this.authService = authService;
        this.userService = userService;
    }

    [HttpPost("UserRegister")]
    public async Task<ActionResult<BaseResponseModel<int>>> UserRegister(BaseRequestModel<UserRegisterModel> model)
    {
        var userInfo = await userService.GetUserByName(model.Data!.UserName!);

        if (userInfo != null)
            return BadRequest(new BaseResponseModel<object>
            {
                RequestId = model.RequestId,
                ErrorCode = ErrorCodeEnum.UserAlreadyExist.ToDescription(),
                Data = 0
            });

        var userId = await authService.StorageUser(model.Data!);

        return Ok(new BaseResponseModel<int>
        {
            RequestId = model.RequestId,
            ErrorCode = ErrorCodeEnum.Success.ToDescription(),
            Data = userId
        });
    }

    [HttpPost("UserLogin")]
    public async Task<ActionResult<BaseResponseModel<UserInfoModel>>> UserLogin(BaseRequestModel<UserLoginModel> model)
    {
        var userInfo = await userService.GetUserByName(model.Data!.UserName!);

        if (userInfo == null)
            return BadRequest(new BaseResponseModel<object>
            {
                RequestId = model.RequestId,
                ErrorCode = ErrorCodeEnum.UserNotExist.ToDescription(),
                Data = null
            });

        if (!authService.VerifyPassword(model.Data!.Password!, userInfo.Password))
            return BadRequest(new BaseResponseModel<object>
            {
                RequestId = model.RequestId,
                ErrorCode = ErrorCodeEnum.IncorrectPassword.ToDescription(),
                Data = null
            });

        var apiToken = authService.IssueJwtToken(new TokenInfoModel { UserName = userInfo.UserName });

        return Ok(new BaseResponseModel<UserInfoModel>
        {
            RequestId = model.RequestId,
            ErrorCode = ErrorCodeEnum.Success.ToDescription(),
            Data = new UserInfoModel
            {
                UserName = userInfo.UserName,
                CreateDate = userInfo.CreateDate,
                MailAddress = userInfo.MailAddress,
                UserId = userInfo.UserId,
                Token = apiToken
            }
        });
    }
}
