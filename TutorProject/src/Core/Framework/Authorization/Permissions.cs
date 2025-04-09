namespace Framework.Authorization;

public class Permissions
{
    public static class Users
    {
        public const string CREATE = "users.create";
        public const string READ = "users.read";
        public const string UPDATE = "accounts.update";
        public const string DELETE = "accounts.delete";
    }

    public static class Students
    {
        public const string CREATE = "students.create";
        public const string READ = "students.read";
        public const string UPDATE = "students.update";
        public const string DELETE = "students.delete";
    }
}