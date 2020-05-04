using AutoMapper;

namespace SFF.Models
{
    public class AutomapperConfig : Profile
    {
        public AutomapperConfig()
        {
            CreateMap<MovieDto, Movie>();
            CreateMap<RatingDto, Rating>();
            CreateMap<RentalDto, Rental>();
            CreateMap<StudioDto, Studio>();
            CreateMap<TriviaDto, Trivia>();

            CreateMap<Movie, MovieDto>();
            CreateMap<Rating, RatingDto>();
            CreateMap<Rental, RentalDto>();
            CreateMap<Studio, StudioDto>();
            CreateMap<Trivia, TriviaDto>();
        }
    }
}