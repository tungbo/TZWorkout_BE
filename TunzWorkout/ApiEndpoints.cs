namespace TunzWorkout.Api
{
    public static class ApiEndpoints
    {
        private const string ApiBase = "api";

        public static class Muscles
        {
            public const string Base = $"{ApiBase}/muscles";

            public const string Create = Base;
            public const string Update = $"{Base}/{{id:Guid}}";
            public const string Delete = $"{Base}/{{id:Guid}}";
            public const string Get = $"{Base}/{{id:Guid}}";
            public const string GetAll = Base;
        }
    }
}
