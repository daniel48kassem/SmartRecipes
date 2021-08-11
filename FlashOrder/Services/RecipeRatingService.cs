using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FlashOrder.Data;
using FlashOrder.DTOs;
using FlashOrder.IRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace FlashOrder.Services
{
    public class RecipeRatingService : BackgroundService
    {
        // private readonly UserManager<ApiUser> _userManager;
        // private readonly IUnitOfWork _unitOfWork;

        private readonly IServiceProvider _provider;


        public RecipeRatingService(IServiceProvider serviceProvider)
        {
            _provider = serviceProvider;
        }


        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var nextTimeWillRun = TimeSpan.FromMinutes(1);

                await RateUsers();
                await Task.Delay((int) nextTimeWillRun.TotalMilliseconds, stoppingToken);
            }
        }

        private async Task RateUsers()
        {
            Console.WriteLine("Rating Users begins...");

            using (IServiceScope scope = _provider.CreateScope())
            {
                var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
                var mapper = scope.ServiceProvider.GetRequiredService<IMapper>();

                var shouldRatedRecipes = await unitOfWork.Recipes.GetAll(r => r.IsRatingUpdated == true, null,
                    new List<string> {"Raters"});

                foreach (Recipe recipe in shouldRatedRecipes)
                {
                    float currentRecipeRating = 0;
                    float ratingSum = recipe.Raters.Sum(r => r.Value);
                    int ratingCount = recipe.Raters.Count;

                    if (ratingCount < 1)
                    {
                        continue;
                    }

                    float meanRating = ratingSum / ratingCount;
                    // float starRating = meanRating / 20;

                    // Recipe tmp = new Recipe()
                    // {
                    //     Chef = recipe.Chef, Description = recipe.Description,
                    //     ChefId = recipe.ChefId, Ingredients = recipe.Ingredients, Raters = recipe.Raters,
                    //     Steps = recipe.Steps, Title = recipe.Title
                    // };
                    //
                    // tmp.Rating = starRating;
                    // tmp.IsRatingUpdated = false;
                    
                    recipe.Rating = meanRating;
                    recipe.IsRatingUpdated = false;
                    
                    unitOfWork.Recipes.Update(recipe);
                    await unitOfWork.save();
                }
            }
        }
    }
}