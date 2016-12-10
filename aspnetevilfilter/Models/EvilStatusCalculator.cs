using System;

namespace aspnetevilfilter.Models
{
    public class EvilStatusCalculator
    {
        private const int MIN_HTTP_STATUS_CODE = 100;
        private const int MAX_HTTP_STATUS_CODE = 599;
        private readonly int LIMIT_HTTP_STATUS_CODE;
        
        public EvilStatusCalculator(int rangeLimitHttpStatusCode)
        {
           if(rangeLimitHttpStatusCode < MIN_HTTP_STATUS_CODE 
           || rangeLimitHttpStatusCode > MAX_HTTP_STATUS_CODE)
                throw new ArgumentException("Value is incorrect");

           LIMIT_HTTP_STATUS_CODE = rangeLimitHttpStatusCode;
        }

        public int CalculateEvilStatusCode(int currentStatusCode)
        {
            if(ValueIsDownLimit(currentStatusCode))
            {
                return (int) (IsEven(currentStatusCode) ? 
                EvilHttpStatusCode.InternalServerError : EvilHttpStatusCode.Fox);
            }
            else if (ValueIsUpLimit(currentStatusCode))
            {
                return (int) EvilHttpStatusCode.Teapot;
            }
            return currentStatusCode;
        }

        private bool ValueIsDownLimit(int currentStatusCode)
        {
            return currentStatusCode >= MIN_HTTP_STATUS_CODE
             && currentStatusCode <= LIMIT_HTTP_STATUS_CODE;
        }

        private bool ValueIsUpLimit(int currentStatusCode)
        {
            return currentStatusCode >= (LIMIT_HTTP_STATUS_CODE+1) 
            && currentStatusCode <= MAX_HTTP_STATUS_CODE;
        }

        private bool IsEven(int number){
            return number % 2 == 0;
        }

    }
}