namespace MediMax.Data.ApplicationModels
{
    public class PaginatedList<T> where T : class
    {
        public Pagination Pagination { get; set; }
        public List<T> List { get; set; }

        public static bool IsEmpty<TG>(PaginatedList<TG> paginatedList) where TG : class
        {
            return paginatedList == null || paginatedList.Pagination == null || paginatedList.List == null ||
                   paginatedList.List.Count == 0;
        }
    }
}