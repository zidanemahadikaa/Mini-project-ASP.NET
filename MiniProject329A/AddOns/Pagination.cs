namespace MiniProject329A.AddOns


{
    public class Pagination<T> : List<T>
    {
        public int PageIndex { get; private set; }
        public int TotalPages { get; private set; }
        public int TotalData { get; set; }

        public Pagination(List<T> pageData, int totalData, int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            TotalPages = (int)Math.Ceiling(totalData / (double)pageSize);
            TotalData = totalData;
            AddRange(pageData);
        }

        public bool HasPreviousPage => PageIndex > 1;

        public bool HasNextPage => PageIndex < TotalPages;

        public static Pagination<T> Create(List<T> source, int pageIndex, int pageSize)
        {
            int totalData = source.Count();
            var pageData = source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            return new Pagination<T>(pageData, totalData, pageIndex, pageSize);
        }
    }
}