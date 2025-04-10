using Users.Application.Commands.Login;
using Users.Contracts.Responses;

namespace Users.Presentation;

public static class ResponseExtensions
{
    public static LoginResponse ToLoginResponse(this LoginResponseModel model)
    {
        return new LoginResponse(model.AccessToken, model.RefreshToken);
    }
}