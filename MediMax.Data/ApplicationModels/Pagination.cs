namespace MediMax.Data.ApplicationModels
{
    public class Pagination
    {
        public int Total { get; set; }
        public int PerPage { get; set; }
        public int CurrentPage { get; set; }
        public int LastPage { get; set; }
        public int PreviousPage { get; set; }
        public int NextPage { get; set; }

        public static Pagination Builder()
        {
            return new Pagination();
        }

        public void SetTotal(int total)
        {
            decimal result;
            Total = total;
            PreviousPage = CurrentPage > 1 ? CurrentPage - 1 : -1;
            result = Convert.ToDecimal(Total) / Convert.ToDecimal(PerPage);

            LastPage = PerPage > Total ? 1 : Convert.ToInt32(Math.Ceiling(result));
            NextPage = CurrentPage < LastPage ? CurrentPage + 1 : -1;
        }

        public Pagination SetPerPage(int perPage)
        {
            PerPage = perPage;
            return this;
        }

        public Pagination SetCurrentPage(int currentPage)
        {
            CurrentPage = currentPage;
            return this;
        }

        public int Offset()
        {
            return PerPage * (CurrentPage - 1);
        }

        public int Limit()
        {
            return PerPage;
        }
    }
}