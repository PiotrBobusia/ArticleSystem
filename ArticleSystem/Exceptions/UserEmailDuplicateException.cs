namespace ArticleSystem.Exceptions
{
    public class UserEmailDuplicateException : Exception
    {
        public UserEmailDuplicateException(string message) : base(message)
        {
            
        }
    }
}
