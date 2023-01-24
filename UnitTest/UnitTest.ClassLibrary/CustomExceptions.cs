namespace UnitTest.ClassLibrary
{
    public class DataNotExistException : Exception
    {
        public DataNotExistException(string msg) : base(msg)
        {

        }
    }

    public class NameIsEmptyOrWhiteSpaceException : Exception
    {
        public NameIsEmptyOrWhiteSpaceException(string msg) : base(msg)
        {

        }
    }

    public class AgeIsInvalidException : Exception
    {
        public AgeIsInvalidException(string msg) : base(msg)
        {

        }
    }
}
