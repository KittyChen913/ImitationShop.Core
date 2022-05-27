namespace ImitationShop.Model.Enum;

public enum ErrorCodeEnum
{
    [Description("0")]
    Success,

    [Description("U0001")]
    UserAlreadyExist,

    [Description("U0002")]
    UserNotExist,

    [Description("U0003")]
    PasswordError,
}
