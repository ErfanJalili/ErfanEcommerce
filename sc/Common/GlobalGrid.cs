namespace Common
{
    public class GlobalGrid
    {
        int? pageNumber;
        public int? PageNumber
        {
            get
            {
                if (pageNumber == null || pageNumber <= 0)
                    return 1;
                return pageNumber.Value;
            }
            set
            {
                pageNumber = value;
            }
        }

        int? count;
        public int? Count
        {
            get
            {
                if (count == null || count <= 0)
                    return 10;
                if (count > 100)
                    return 100;

                return count.Value;
            }
            set
            {
                count = value;
            }
        }
    }
}
