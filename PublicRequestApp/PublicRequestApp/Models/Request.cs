namespace PublicRequestApp.Models
{
    public class Request
    {
        public int Id { get; set; } 
        public string? full_name { get; set; }
        public string? email { get; set; }
        public string? subject { get; set; }
        public string? message { get; set; }
        public string? status { get; set; }
        public DateTime? created_at { get; set; }
        public string? department { get; set; }
    }
}
