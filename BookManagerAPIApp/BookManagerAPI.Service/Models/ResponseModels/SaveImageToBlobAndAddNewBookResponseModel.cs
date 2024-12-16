using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BookManagerAPI.Service.Models.ResponseModels
{
    public class SaveImageToBlobAndAddNewBookResponseModel
    {
        public bool IsSuccess { get; set; }

        public string ErrorMessage { get; set; }

        public HttpStatusCode StatusCode { get; set; }


        public SaveImageToBlobAndAddNewBookResponseModel(bool isSuccess, string errorMessage, HttpStatusCode httpStatusCode)
        {
            IsSuccess = isSuccess;
            ErrorMessage = errorMessage;
            StatusCode = httpStatusCode;
        }        
    }
}
