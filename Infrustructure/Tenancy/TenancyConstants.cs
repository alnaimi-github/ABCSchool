﻿namespace Infrastructure.Tenancy;

public class TenancyConstants
{
    public static class Root
    {
        public const string Id = "root";
        public const string Name = "Root";
        public const string Email = "admin.root@school.com";
    }

    public const string DefaultPassword = "admin123#";
    public const string TenantIdName = "tenant";
}