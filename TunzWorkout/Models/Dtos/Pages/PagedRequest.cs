﻿namespace TunzWorkout.Api.Models.Dtos.Pages
{
    public class PagedRequest
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
