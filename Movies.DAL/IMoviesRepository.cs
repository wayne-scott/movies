namespace Movies.DAL
{
    /// <summary>
    /// Provide access to Movies data.
    /// </summary>
    public interface IMoviesRepository
    {
        /// <summary>
        /// Provide access to all movies.
        /// </summary>
        /// <returns>A queryable list of movies.</returns>
        DomainModel.Movies GetAllMovies();
    }
}
