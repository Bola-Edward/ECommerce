using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Domain.Constants
{
    // This class contains constants for user roles in the e-commerce application.
    // SuperAdmin: Has full access to all features and settings.
    // Admin: Can manage products, orders, and users.
    // User: Can browse products, place orders, and manage their own account.
    public static class Roles
    {
        public const string SuperAdmin = "SuperAdmin";
        public const string Admin = "Admin";
        public const string User = "User";

        public static readonly IReadOnlyList<string> All = [SuperAdmin, Admin, User];
    }
}
