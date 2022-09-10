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
    IncorrectPassword,

    [Description("P0001")]
    ParameterIsIncorrect,

    [Description("S9999")]
    OtherSystemError
}
