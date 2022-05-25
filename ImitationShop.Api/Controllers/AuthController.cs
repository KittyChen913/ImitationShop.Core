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
    public async Task<ActionResult<int>> UserRegister(UserRegisterModel model)
    {
        var userInfo = await userService.GetUserByName(model.UserName!);

        if (userInfo != null)
            return BadRequest("This user name already exists.");

        var userId = await authService.StorageUser(model);
        return Ok(userId);
    }
}
