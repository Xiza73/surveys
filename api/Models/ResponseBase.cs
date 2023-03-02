namespace manuel_fajardo_prueba_tecnica.Models;

public partial class ResponseBase
{
  public string Message { get; set; } = null!;

  public int Status { get; set; } = 0;

  public ResponseBase(string message, int status)
  {
    Message = message;
    Status = status;
  }
}
