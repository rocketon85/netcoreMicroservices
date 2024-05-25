namespace NetCoreMicroservices.Commons.Structures.SecurityStructure
{
    public static class PermissionStructure
    {
     
        public static class Users
        {
            public const string View = "users.view";
            public const string Add = "users.add";
            public const string Edit = "users.edit";
            public const string Delete = "users.delete";
        }
        public static class Cars
        {
            public const string ViewDetail = "cars.view.detail";
            public const string ViewList = "cars.view.list";
            public const string Add = "cars.add";
            public const string Edit = "cars.edit";
            public const string Delete = "cars.delete";
        }

    }
}
