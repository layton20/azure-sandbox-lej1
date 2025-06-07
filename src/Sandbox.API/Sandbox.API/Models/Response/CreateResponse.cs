namespace Sandbox.API.Models.Response;

public class CreateResponse
{
    public CreateResponse(Guid uid, bool isSuccess)
    {
        Uid = uid;
        IsSuccess = isSuccess;
    }

    public bool IsSuccess { get; set; }
    public Guid Uid { get; set; }

    public static CreateResponse Success(Guid uid)
    {
        return new CreateResponse(uid, true);
    }

    public static CreateResponse Failure()
    {
        return new CreateResponse(Guid.Empty, false);
    }
}