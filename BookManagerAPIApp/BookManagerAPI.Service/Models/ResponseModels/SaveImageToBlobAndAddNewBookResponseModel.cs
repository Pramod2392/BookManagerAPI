using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookManagerAPI.Service.Models.ResponseModels
{
    public class SaveImageToBlobAndAddNewBookResponseModel
    {
        public bool IsSuccess { get; set; }

        public string ErrorMessage { get; set; }


        public SaveImageToBlobAndAddNewBookResponseModel(bool isSuccess, string errorMessage)
        {
            IsSuccess = isSuccess;
            ErrorMessage = errorMessage;
        }
    }
}
