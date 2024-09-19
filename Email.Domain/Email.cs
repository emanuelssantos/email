namespace Email.Domain;

public class Email
{
    public string? Subject { get; set; }
    public string? Body { get; set; }
    public List<string> To { get; set; } = new List<string>();
    public List<EmailFile>? Files { get; set; }
}

public class EmailFile
{
    public required string ContentType { get; set; }
    public required string FileName { get; set; }
    public required byte[] Bytes { get; set; }
}
