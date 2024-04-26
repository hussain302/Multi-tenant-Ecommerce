namespace API.Contracts;

public class ApiRoutes
{
    private const string BaseUrl = "/ecommerce-store";

    /// <summary>
    /// These categories screen routies will be used by admin
    /// </summary>
    public static class Categories
    {
        public const string Post = $"{BaseUrl}/api/v{{version:apiVersion}}/Categories/create";
        public const string GetAll = $"{BaseUrl}/api/v{{version:apiVersion}}/Categories/list";
        public const string GetById = $"{BaseUrl}/api/v{{version:apiVersion}}/Categories/search";
        public const string Update = $"{BaseUrl}/api/v{{version:apiVersion}}/Categories/update";
        public const string Delete = $"{BaseUrl}/api/v{{version:apiVersion}}/Categories/remove";
    }

    /// <summary>
    /// These role screen routies will be used by admin
    /// </summary>
    public static class Role
    {
        public const string Post = $"{BaseUrl}/api/v{{version:apiVersion}}/roles/create";
        public const string GetAll = $"{BaseUrl}/api/v{{version:apiVersion}}/roles/list";
        public const string GetById = $"{BaseUrl}/api/v{{version:apiVersion}}/roles/search";
        public const string Update = $"{BaseUrl}/api/v{{version:apiVersion}}/roles/update";
        public const string Delete = $"{BaseUrl}/api/v{{version:apiVersion}}/roles/remove";
    }

    /// <summary>
    /// These user screen routies will be creating users, and other user operations
    /// </summary>
    public static class User
    {
        public const string PostAdmin = $"{BaseUrl}/api/v{{version:apiVersion}}/user/Administrator/create";
        public const string PostCustomer = $"{BaseUrl}/api/v{{version:apiVersion}}/user/customer/create";
        public const string PostVendor = $"{BaseUrl}/api/v{{version:apiVersion}}/user/vendor/create";
        public const string GetAll = $"{BaseUrl}/api/v{{version:apiVersion}}/user/list";
        public const string GetById = $"{BaseUrl}/api/v{{version:apiVersion}}/user/search";
        public const string Update = $"{BaseUrl}/api/v{{version:apiVersion}}/user/update";
        public const string Delete = $"{BaseUrl}/api/v{{version:apiVersion}}/user/remove";
    }

    /// <summary>
    /// These authentication routies will be used by all type of users
    /// </summary>
    public static class Auth
    {
        public const string Login = $"{BaseUrl}/api/v{{version:apiVersion}}/auth/user/login";
        public const string Logout = $"{BaseUrl}/api/v{{version:apiVersion}}/auth/user/logout";
        public const string LoginWithGoogle = $"{BaseUrl}/api/v{{version:apiVersion}}/auth/login/google";
        public const string LoginWithMicrosoft = $"{BaseUrl}/api/v{{version:apiVersion}}/auth/login/microsoft";
        public const string LoginWithFacebook = $"{BaseUrl}/api/v{{version:apiVersion}}/auth/login/facebook";
    }
}
