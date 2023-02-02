using System.Collections.Generic;

namespace InventoryControlMobile.Models
{
    public class HttpResponse<T>
    {
        public bool IsSuccess { get; set; }

        public T Data { get; set; }

        public IList<string> Errors { get; set; }
    }
}