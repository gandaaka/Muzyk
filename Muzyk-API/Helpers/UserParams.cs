namespace DotNetPractice.Helpers
{
    public class UserParams
    {
        private const int MaxPageSize = 50;
        public int PageNumber { get; set; } = 1;
        private int pageSize = 10;
        public int PageSize
        {
            get { return pageSize;}
            set { pageSize = (value > MaxPageSize) ? MaxPageSize : value;}
        }

        public int UserId { get; set; }
        public string Genre { get; set; } = "";
        public int MinExp { get; set; } = 0;
        public int MaxExp { get; set; } = 25;
        public string OrderBy { get; set; }
        public bool Followees { get; set; } = false;
        public bool Followers { get; set; } = false;   
    }
}