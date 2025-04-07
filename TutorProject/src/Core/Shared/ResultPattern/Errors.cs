namespace Shared.ResultPattern;

public static class Errors
{
    public static class General
    {
        public static Error ValueIsInvalid(string? name = null)
        {
            string label = name ?? "value";
            return Error.Validation("value.is.invalid", $"{label} is invalid");
        }

        public static Error NotFound(Guid? id = null, string? name = null)
        {
            string forId = id == null ? string.Empty : $"by Id '{id}'";
            return Error.NotFound("record.not.found", $"{name ?? "record"} not found{forId}");
        }

        public static Error NotFound(string? name = null)
        {
            return Error.NotFound("record.not.found", $"{name} not found");
        }

        public static Error ValueIsRequired(string? name = null)
        {
            string label = name ?? string.Empty;
            return Error.Validation("length.is.invalid", $"Field {label} required");
        }

        public static Error AlreadyExist()
        {
            return Error.Validation("record.already.exist", "Record already exist");
        }

        public static Error Failure()
        {
            return Error.Failure("server.failure", "Server Error");
        }
    }

    public static class Auth
    {
        public static Error InvalidCredentials()
        {
            return Error.Validation("credentials.is.invalid", "User credentials is invalid");
        }

        public static Error InvalidRole()
        {
            return Error.Failure("role.is.invalid", "User role is invalid");
        }

        public static Error InvalidToken()
        {
            return Error.Validation("token.is.invalid", "Token is invalid");
        }

        public static Error ExpiredToken()
        {
            return Error.Validation("token.is.expired", "Token expired");
        }
    }
}