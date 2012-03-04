using System;

namespace BDDForNUnit.Test
{
    public class TestExceptionHelper
    {
        public static Exception GenerateException()
        {
            try
            {
                throw new Exception("my exception", new Exception("my inner exception"));
            }
            catch (Exception ex)
            {
                return ex;
            }
        }
    }
}