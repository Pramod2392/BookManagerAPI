using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookManagerAPI.Repository.Models.ResponseModels
{
    public class UploadImageToBlobAsyncResponseModel
    {
        public bool IsSuccess { get; set; }        

        public string ReasonPhrase { get; set; }

        public string BlobName { get; set; }

        public UploadImageToBlobAsyncResponseModel(bool isSuccess, string reasonPhrase, string blobName)
        {
            IsSuccess = isSuccess;
            ReasonPhrase = reasonPhrase;
            BlobName = blobName;
        }
    }
}
