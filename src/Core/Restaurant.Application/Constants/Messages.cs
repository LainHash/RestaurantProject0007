namespace Restaurant.Application.Constants
{
    public static class Messages<TEntity> where TEntity : class
    {
        public static string AddSuccess => $"{typeof(TEntity).Name} added successfully.";
        public static string AddError => $"An error occurred while adding the {typeof(TEntity).Name}.";
        public static string GetAllSuccess => $"{typeof(TEntity).Name} listed successfully.";
        public static string GetAllError => $"An error occurred while listing the {typeof(TEntity).Name}.";
        public static string GetByIdSuccess => $"{typeof(TEntity).Name} details listed successfully.";
        public static string GetByIdError => $"An error occurred while listing the {typeof(TEntity).Name} details.";
        public static string UpdateSuccess => $"{typeof(TEntity).Name} updated successfully.";
    }
}
