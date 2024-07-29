using System;
namespace rs.model
{
    public class Response
    {
        public string Status { get; set; } = "Success";

        public string Message { get; set; } = "";

        public string? RedirectURL { get; set; }

        public object? Results { get; set; }

        public int? StatusCode { get; set; }
    }
}