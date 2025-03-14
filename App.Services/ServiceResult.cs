using System.Net;

namespace App.Services
{
    public class ServiceResult<T>
    {
        public T? Data { get; set; }
        public List<string>? ErrorMessage { get; set; }
        public HttpStatusCode HttpStatusCode { get; set; }
        public bool IsSuccess => ErrorMessage == null || ErrorMessage.Count == 0;
        public bool IsFail() => !IsSuccess;

        //static factory method
        public static ServiceResult<T> Success(T data,HttpStatusCode httpStatusCode = HttpStatusCode.OK)
        {
            return new ServiceResult<T>() 
            { 
                Data = data,
                HttpStatusCode = httpStatusCode
            };
        }
        public static ServiceResult<T> Fail(List<string> errorMessage,
            HttpStatusCode httpStatusCode = HttpStatusCode.BadRequest)
        {
            return new ServiceResult<T>()
            {
                ErrorMessage = errorMessage,
                HttpStatusCode=httpStatusCode
            };
        }
        public static ServiceResult<T> Fail(string errorMessage, HttpStatusCode httpStatusCode = HttpStatusCode.BadRequest)
        {
            return new ServiceResult<T>()
            {
                ErrorMessage = new List<string>() { errorMessage },
                HttpStatusCode=httpStatusCode
            };
        }

    }

    public class ServiceResult
    {
        public List<string>? ErrorMessage { get; set; }
        public HttpStatusCode HttpStatusCode { get; set; }
        public bool IsSuccess() => ErrorMessage == null || ErrorMessage.Count == 0;
        public bool IsFail() => !IsSuccess();

        //static factory method
        public static ServiceResult Success(HttpStatusCode httpStatusCode = HttpStatusCode.OK)
        {
            return new ServiceResult()
            {
                HttpStatusCode = httpStatusCode
            };
        }
        public static ServiceResult Fail(List<string> errorMessage,
            HttpStatusCode httpStatusCode = HttpStatusCode.BadRequest)
        {
            return new ServiceResult()
            {
                ErrorMessage = errorMessage,
                HttpStatusCode = httpStatusCode
            };
        }
        public static ServiceResult Fail(string errorMessage, HttpStatusCode httpStatusCode = HttpStatusCode.BadRequest)
        {
            return new ServiceResult()
            {
                ErrorMessage = new List<string>() { errorMessage },
                HttpStatusCode = httpStatusCode
            };
        }

    }
}
