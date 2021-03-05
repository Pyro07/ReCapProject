using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Results
{
    public class Result : IResult
    {
        /*
         * İki tane constructor var. Biri hem mesaj alıyor, hem de başarılı durumunu alıyor.
         * Diğer constructor sadece durumu alıyor.
        */

        //this demek class'ın kendini ifade eder. Yani Result class'ını
        public Result(bool success, string message) : this(success)//Tek parametreli olan constructor'a success'i yolla
        {
            Message = message;
            //Success = success; ->> Buradaki Success'i set etme görevini alttaki metoda veriyoruz.
        }

        //Sadece bu metodu kullanmak istersek üstteki metot çalışmaz. Ama üstteki metot çalışırsa doğal olarak 
        //bu metod da çalışır
        public Result(bool success)
        {
            Success = success;
        }

        public bool Success { get; }

        public string Message { get; }
    }
}
