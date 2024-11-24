using TunzWorkout.Api.Models.Dtos.Authentication;
using TunzWorkout.Api.Models.Dtos.Equipments;
using TunzWorkout.Api.Models.Dtos.Exercises;
using TunzWorkout.Api.Models.Dtos.FitnessProfiles;
using TunzWorkout.Api.Models.Dtos.Goals;
using TunzWorkout.Api.Models.Dtos.Levels;
using TunzWorkout.Api.Models.Dtos.Muscles;
using TunzWorkout.Api.Models.Dtos.RoundExercises;
using TunzWorkout.Api.Models.Dtos.Rounds;
using TunzWorkout.Api.Models.Dtos.Users;
using TunzWorkout.Api.Models.Dtos.Workouts;
using TunzWorkout.Application.Commands.Authentication;
using TunzWorkout.Domain.Entities.Equipments;
using TunzWorkout.Domain.Entities.Exercises;
using TunzWorkout.Domain.Entities.FitnessProfiles;
using TunzWorkout.Domain.Entities.Goals;
using TunzWorkout.Domain.Entities.Levels;
using TunzWorkout.Domain.Entities.Muscles;
using TunzWorkout.Domain.Entities.RoundExercises;
using TunzWorkout.Domain.Entities.Rounds;
using TunzWorkout.Domain.Entities.Users;
using TunzWorkout.Domain.Entities.Workouts;

namespace TunzWorkout.Api.Mapping
{
    public static class DtoMapping
    {
        #region Muscle
        public static Muscle MapToMuscle(this CreateMuscleRequest request)
        {
            return new Muscle
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                ImageFile = request.ImageFile,
            };
        }
        public static Muscle MapToMuscle(this UpdateMuscleRequest request, Guid id)
        {
            return new Muscle
            {
                Id = id,
                Name = request.Name,
                ImageFile = request.ImageFile,
            };
        }
        public static MuscleDTO MapToMuscleDTO(this Muscle muscle)
        {
            return new MuscleDTO
            {
                Name = muscle.Name,
            };
        }
        public static MuscleResponse MapToResponse(this Muscle muscle)
        {
            return new MuscleResponse
            {
                Id = muscle.Id,
                Name = muscle.Name,
                ImageUrl = (muscle.MuscleImages != null) ? (string.Join(",", muscle.MuscleImages.Select(x => x.ImagePath))) : (string.Empty),
            };
        }
        public static MusclesResponse MapToResponse(this IEnumerable<Muscle> muscles)
        {
            return new MusclesResponse
            {
                Items = muscles.Select(MapToResponse)
            };
        }
        #endregion

        #region Equipment

        public static Equipment MapToEquipment(this CreateEquipmentRequest request)
        {
            return new Equipment
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                ImageFile = request.ImageFile
            };
        }
        public static Equipment MapToEquipment(this UpdateEquipmentRequest request, Guid id)
        {
            return new Equipment
            {
                Id = id,
                Name = request.Name,
                ImageFile = request.ImageFile
            };
        }
        public static EquipmentResponse MapToResponse(this Equipment equipment)
        {
            return new EquipmentResponse
            {
                Id = equipment.Id,
                Name = equipment.Name,
                ImageUrl = (equipment.EquipmentImages != null) ? (string.Join(",", equipment.EquipmentImages.Select(x => x.ImagePath))) : (string.Empty),
            };
        }
        public static EquipmentsResponse MapToResponse(this IEnumerable<Equipment> equipments)
        {
            return new EquipmentsResponse
            {
                Items = equipments.Select(MapToResponse)
            };
        }

        #endregion

        #region Level

        public static Level MapToLevel(this CreateLevelRequest request)
        {
            return new Level
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Description = request.Description,
            };
        }

        public static Level MapToLevel(this UpdateLevelRequest request, Guid id)
        {
            return new Level
            {
                Id = id,
                Name = request.Name,
                Description = request.Description,
            };
        }

        public static LevelResponse MapToResponse(this Level level)
        {
            return new LevelResponse
            {
                Id = level.Id,
                Name = level.Name,
                Description = level.Description,
            };
        }

        public static LevelsResponse MapToResponse(this IEnumerable<Level> levels)
        {
            return new LevelsResponse
            {
                Items = levels.Select(MapToResponse),
            };
        }

        #endregion

        #region Exercise

        public static Exercise MapToExercise(this CreateExerciseRequest request)
        {
            return new Exercise
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                LevelId = request.LevelId,
                HasEquipment = request.HasEquipment,
                VideoFile = request.VideoFile,
                SelectedEquipmentIds = (request.SelectedEquipmentIds is null) ? new List<Guid>() : request.SelectedEquipmentIds,
                SelectedMuscleIds = request.SelectedMuscleIds,
            };
        }
        public static Exercise MapToExercise(this UpdateExerciseRequest request, Guid id)
        {
            return new Exercise
            {
                Id = id,
                Name = request.Name,
                LevelId = request.LevelId,
                HasEquipment = request.HasEquipment,
                VideoFile = request.VideoFile,
                SelectedEquipmentIds = (request.SelectedEquipmentIds is null) ? new List<Guid>() : request.SelectedEquipmentIds,
                SelectedMuscleIds = request.SelectedMuscleIds,
            };
        }
        public static ExerciseResponse MapToResponse(this Exercise exercise)
        {
            return new ExerciseResponse
            {
                Id = exercise.Id,
                Name = exercise.Name,
                LevelName = exercise.Level.Name,
                HasEquipment = exercise.HasEquipment,
                VideoUrl = (exercise.Videos != null) ? string.Join(",", exercise.Videos.Select(x => x.VideoPath)) : string.Empty,

                SelectedMuscles = exercise.ExerciseMuscles.Select(el => new MuscleResponse
                {
                    Id = el.MuscleId,
                    Name = el.Muscle.Name,
                    ImageUrl = el.Muscle.MuscleImages != null ? string.Join(",", el.Muscle.MuscleImages.Select(x => x.ImagePath)) : string.Empty,
                }).ToList(),

                SelectedEquipments = exercise.ExerciseEquipments.Select(el => new EquipmentResponse
                {
                    Id = el.EquipmentId,
                    Name = el.Equipment.Name,
                    ImageUrl = el.Equipment.EquipmentImages != null ? string.Join(",", el.Equipment.EquipmentImages.Select(x => x.ImagePath)) : string.Empty,
                }).ToList(),
            };
        }

        public static ExercisesResponse MapToResponse(this IEnumerable<Exercise> exercises)
        {
            return new ExercisesResponse
            {
                Items = exercises.Select(MapToResponse),
            };
        }

        #endregion

        #region Authentication

        public static RegisterCommand MapToRegisterCommand(this RegisterRequest request)
        {
            return new RegisterCommand
            {
                Username = request.Username,
                Email = request.Email,
                Password = request.Password,
                FirstName = request.FirstName,
                LastName = request.LastName,
                LevelId = request.LevelId,
                Gender = request.Gender,
                Height = request.Height,
                Weight = request.Weight,
                SelectedGoalIds = request.SelectedGoalIds,
            };
        }

        public static RegisterRequest MapToRegisterRequest(this RegisterCommand command)
        {
            return new RegisterRequest
            {
                Username = command.Username,
                Email = command.Email,
                Password = command.Password,
                FirstName = command.FirstName,
                LastName = command.LastName,
            };
        }

        public static LoginCommand MapToLoginCommand(this LoginRequest request)
        {
            return new LoginCommand
            {
                Email = request.Email,
                Password = request.Password,
            };
        }

        public static LoginRequest MapToLoginRequest(this LoginCommand command)
        {
            return new LoginRequest
            {
                Email = command.Email,
                Password = command.Password,
            };
        }

        public static AuthenticationResponse MapToAuthenticationResponse(this AuthenticationCommand authenticationCommand)
        {
            return new AuthenticationResponse
            {
                Email = authenticationCommand.Email,
                Token = authenticationCommand.Token,
                Expiration = authenticationCommand.Expiration,
                RefreshToken = authenticationCommand.RefreshToken,
                RefreshTokenExpiry = authenticationCommand.RefreshTokenExpiry,
            };
        }

        public static TokenModelCommand MapToTokenModelCommand(this TokenModelRequest request)
        {
            return new TokenModelCommand
            {
                Token = request.Token,
                RefreshToken = request.RefreshToken,
            };
        }

        public static TokenModelRequest MapToTokenModelRequest(this TokenModelCommand command)
        {
            return new TokenModelRequest
            {
                Token = command.Token,
                RefreshToken = command.RefreshToken,
            };
        }

        #endregion

        #region User

        public static User MapToUser(this UpdateUserRequest request, Guid id)
        {
            return new User
            {
                Id = id,
                Username = request.Username,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Role = request.Role,
                IsActive = request.IsActive,
                IsDeleted = request.IsDeleted,
            };
        }

        public static UserResponse MapToResponse(this User user)
        {
            return new UserResponse
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                PasswordHash = user.PasswordHash,
                CreateAt = user.CreateAt,
                RefreshToken = user.RefreshToken,
                RefreshTokenExpiry = user.RefreshTokenExpiry,
                Role = user.Role,
                IsActive = user.IsActive,
                IsDeleted = user.IsDeleted,
            };
        }
        public static UsersResponse MapToResponse(this IEnumerable<User> users)
        {
            return new UsersResponse
            {
                Items = users.Select(MapToResponse),
            };
        }

        #endregion

        #region Goal

        public static Goal MapToGoal(this CreateGoalRequest request)
        {
            return new Goal
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Description = request.Description
            };
        }
        public static Goal MapToGoal(this UpdateGoalRequest request, Guid id)
        {
            return new Goal
            {
                Id = id,
                Name = request.Name,
                Description = request.Description
            };
        }
        public static GoalResponse MapToResponse(this Goal goal)
        {
            return new GoalResponse
            {
                Id = goal.Id,
                Name = goal.Name,
                Description = goal.Description
            };
        }
        public static GoalsResponse MapToResponse(this IEnumerable<Goal> goals)
        {
            return new GoalsResponse
            {
                Items = goals.Select(MapToResponse)
            };
        }

        #endregion

        #region FitnessProfile

        public static FitnessProfileResponse MapToResponse(this FitnessProfile fitnessProfile)
        {
            return new FitnessProfileResponse
            {
                Id = fitnessProfile.Id,
                UserId = fitnessProfile.UserId,
                LevelId = fitnessProfile.LevelId,
                Height = fitnessProfile.Height,
                Weight = fitnessProfile.Weight,
                Gender = fitnessProfile.Gender,
                SelectedGoalIds = fitnessProfile.UserGoals.Select(el => new GoalResponse
                {
                    Id = el.GoalId,
                    Name = el.Goal.Name,
                    Description = el.Goal.Description
                }).ToList()
            };
        }

        public static FitnessProfile MapToFitnessProfile(this UpdateFitnessProfileRequest request, Guid id)
        {
            return new FitnessProfile
            {
                Id = id,
                UserId = request.UserId,
                LevelId = request.LevelId,
                Gender = request.Gender,
                Height = request.Height,
                Weight = request.Weight,
                SelectedGoalIds = request.SelectedGoalIds
            };
        }

        public static FitnessProfilesResponse MapToResponse(this IEnumerable<FitnessProfile> fitnessProfiles)
        {
            return new FitnessProfilesResponse
            {
                Items = fitnessProfiles.Select(MapToResponse)
            };
        }
        #endregion

        #region Workout

        public static Workout MapToWorkout(this CreateWorkoutRequest request)
        {
            return new Workout
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                LevelId = request.LevelId,
                GoalId = request.GoalId,
                Rounds = request.RoundRequests.Select(r => new Round
                {
                    Id = Guid.NewGuid(),
                    Name = r.Name,
                    Set = r.Set,
                    Rest = r.Rest,
                    Order = r.Order,
                    RoundExercises = r.RoundExerciseRequests.Select(re => new RoundExercise
                    {
                        Id = Guid.NewGuid(),
                        ExerciseId = re.ExerciseId,
                        Reps = re.Reps,
                        Order = re.Order,
                        Rest = re.Rest,
                    }).ToList(),
                }).ToList(),
            };
        }

        public static Workout MapToWorkout(this UpdateWorkoutRequest request, Guid id)
        {
            return new Workout
            {
                Id = id,
                Name = request.Name,
                LevelId = request.LevelId,
                GoalId = request.GoalId,
                Rounds = request.RoundRequests.Select(r => new Round
                {
                    Id = r.Id,
                    Name = r.Name,
                    Set = r.Set,
                    Rest = r.Rest,
                    Order = r.Order,
                    WorkoutId = id,
                    RoundExercises = r.RoundExerciseRequests.Select(re => new RoundExercise
                    {
                        Id = re.Id,
                        RoundId = re.RoundId,
                        ExerciseId = re.ExerciseId,
                        Reps = re.Reps,
                        Order = re.Order,
                        Rest = re.Rest,
                    }).ToList(),
                }).ToList(),
            };
        }

        public static WorkoutResponse MapToResponse(this Workout workout)
        {
            return new WorkoutResponse
            {
                Id = workout.Id,
                Name = workout.Name,
                LevelId = workout.LevelId,
                GoalId = workout.GoalId,
                RoundResponses = workout.Rounds.Select(r => new RoundResponse
                {
                    Name = r.Name,
                    Set = r.Set,
                    Rest = r.Rest,
                    Order = r.Order,
                    RoundExerciseResponses = r.RoundExercises.Select(re => new RoundExerciseResponse
                    {
                        RoundId = re.RoundId,
                        ExerciseId = re.ExerciseId,
                        Order = re.Order,
                        Reps = re.Reps,
                        Rest = re.Rest,
                    }).ToList(),
                }).ToList(),
            };
        }

        public static WorkoutsResponse MapToResponse(this IEnumerable<Workout> workouts)
        {
            return new WorkoutsResponse
            {
                Workouts = workouts.Select(MapToResponse),
            };
        }

        #endregion

        #region Round
        public static Round MapToRound(this CreateRoundRequest request)
        {
            return new Round
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Set = request.Set,
                Rest = request.Rest,
            };
        }
        #endregion
    }
}
