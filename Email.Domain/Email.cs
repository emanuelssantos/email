namespace Email.Domain;

public class Email
{
    public string? Subject { get; set; }
    public string? Body { get; set; }
    public List<string> To { get; set; } = new List<string>();
    //public IFormFile[]? Files { get; set; }
}
