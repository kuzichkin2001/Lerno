namespace Lerno.Shared.Commands
{
    public class BusUserAction : BusAction
    {
        // Create
        public const string CreateUser = "user.create";
        public const string CreateUsers = "users.create";

        // Read
        public const string GetUser = "user.get";
        public const string GetUserFiltered = "user.get.filtered";
        public const string GetUsers = "users.get";
        public const string GetUsersFiltered = "users.get.filtered";

        // Update
        public const string UpdateUser = "user.update";

        // Delete
        public const string DeleteUser = "user.delete";
        public const string DeleteUsers = "users.delete";
    }
}
