namespace ArticleSystem.Exceptions
{
    public class UserLoginDuplicateException : Exception
    {
        public UserLoginDuplicateException(string message) : base(message)
        {
            
        }
    }
}
