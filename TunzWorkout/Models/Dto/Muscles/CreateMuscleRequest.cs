﻿namespace TunzWorkout.Api.Models.Dto.Muscles
{
    public class CreateMuscleRequest
    {
        public string Name { get; set; }
        public IFormFile? ImageFile { get; set; }
    }
}