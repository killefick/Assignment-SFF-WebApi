using AutoMapper;

namespace SFF.Models
{
    public class SFFProfile : Profile
    {
        public SFFProfile()
        {
            CreateMap<Movie, MovieDto>();
            CreateMap<Rating, RatingDto>();
            CreateMap<Rental, RentalDto>();
            CreateMap<Studio, StudioDto>();
            CreateMap<Trivia, TriviaDto>();

            CreateMap<MovieDto, Movie>();
            CreateMap<RatingDto, Rating>();
            CreateMap<RentalDto, Rental>();
            CreateMap<StudioDto, Studio>();
            CreateMap<TriviaDto, Trivia>();
        }
    }
}