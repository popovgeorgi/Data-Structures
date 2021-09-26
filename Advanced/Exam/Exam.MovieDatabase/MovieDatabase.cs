using System;
using System.Collections.Generic;

namespace Exam.MovieDatabase
{
    using System.Linq;

    public class MovieDatabase : IMovieDatabase
    {
        private IDictionary<string, Movie> byId;

        private HashSet<Movie> movies;

        private IDictionary<string, HashSet<Movie>> byActor;

        public MovieDatabase()
        {
            this.byId = new Dictionary<string, Movie>();
            this.movies = new HashSet<Movie>();
            this.byActor = new Dictionary<string, HashSet<Movie>>();
        }

        public int Count => this.byId.Count;

        public void AddMovie(Movie movie)
        {
            foreach (var actor in movie.Actors)
            {
                if (!this.byActor.ContainsKey(actor))
                {
                    this.byActor.Add(actor, new HashSet<Movie>());
                }

                this.byActor[actor].Add(movie);
            }
            this.movies.Add(movie);
            this.byId.Add(movie.Id, movie);
        }

        public bool Contains(Movie movie)
        {
            return this.movies.Contains(movie);
        }

        public IEnumerable<Movie> GetMoviesByActor(string actorName)
        {
            return this.byActor[actorName].OrderByDescending(x => x.Rating).ThenBy(x => x.ReleaseYear).ToList();
        }

        public IEnumerable<Movie> GetMoviesByActors(List<string> actors)
        {
            var moviesWithActors = new HashSet<Movie>();

            foreach (var actor in actors)
            {
                foreach (var movie in this.byActor[actor])
                {
                    moviesWithActors.Add(movie);
                }
            }

            return moviesWithActors.OrderByDescending(x => x.Rating).ThenByDescending(x => x.ReleaseYear).ToList();
        }

        public IEnumerable<Movie> GetMoviesByYear(int releaseYear)
        {
            var movies = this.movies.Where(x => x.ReleaseYear == releaseYear).OrderByDescending(x => x.Rating).ToList();

            if (movies.Count() == 0)
            {
                return new List<Movie>();
            }

            return movies;
        }

        public IEnumerable<Movie> GetMoviesInRatingRange(double lowerBound, double upperBound)
        {
            var movies = this.movies.Where(x => x.Rating >= lowerBound && x.Rating <= upperBound)
                .OrderByDescending(x => x.Rating).ToList();

            if (movies.Count() == 0)
            {
                return new List<Movie>();
            }

            return movies;
        }

        public void RemoveMovie(string movieId)
        {
            if (!this.byId.ContainsKey(movieId))
            {
                throw new ArgumentException();
            }
            // does not remove normal movie
            //does not remove byActor
            this.byId.Remove(movieId);
        }
    }
}
