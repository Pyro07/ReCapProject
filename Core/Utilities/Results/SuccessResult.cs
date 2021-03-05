using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Results
{
    public class SuccessResult : Result
    {
        //base() demek, Result demek
        public SuccessResult(string message) : base(true, message)
        {

        }

        //Mesaj vermek istemiyorsak bu constructor. base'in tek parametreli constructor'ı çalışır
        public SuccessResult() : base(true)
        {

        }
    }
}
