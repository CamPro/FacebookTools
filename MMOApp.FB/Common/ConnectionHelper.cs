using KSS.Patterns.Web;
using System;

namespace MarketDragon.Automation.Common
{
    public class ConnectionHelper
    {
        /// <summary>
        /// Try request many times if error.
        /// </summary>
        /// <param name="func"></param>
        public void TryRequest(Action func, int maxRequestTry = 3)
        {
            int tries = 0;
            do
            {
                try
                {
                    tries++;
                    func();
                    break;
                }
                catch (Exception e)
                {
                    if (!CatchRequestException(e) || tries >= maxRequestTry)
                        throw;
                }
            } while (tries < maxRequestTry);
        }

        public T TryRequest<T>(Func<T> func, int maxRequestTry = 3)
        {
            int tries = 0;

            do
            {
                try
                {
                    tries++;
                    return func();
                }
                catch (Exception e)
                {
                    if (!CatchRequestException(e) || tries >= maxRequestTry)
                        throw;
                }
            } while (tries < maxRequestTry);

            return default(T);
        }

        public bool CatchRequestException(Exception e)
        {
            if (e is WebResponseException)
                return true;

            if (e is AggregateException)
            {
                var ae = e as AggregateException;
                ae.Flatten();

                foreach (var ine in ae.InnerExceptions)
                {
                    if (CatchRequestException(ine))
                        return true;
                }
            }

            if (e.InnerException != null)
                return CatchRequestException(e.InnerException);

            return false;
        }        
    }
}