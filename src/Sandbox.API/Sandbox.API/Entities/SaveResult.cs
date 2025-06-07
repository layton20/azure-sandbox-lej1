namespace Sandbox.API.Entities;

public class SaveResult
{
    public SaveResult(Guid uid, bool isSuccess, bool isDuplicate)
    {
        IsSuccess = isSuccess;
        Uid = uid;
        IsDuplicate = isDuplicate;
    }

    public bool IsSuccess { get; set; }
    public Guid Uid { get; set; }
    public bool IsDuplicate { get; set; }

    public static SaveResult Success(Guid uid)
    {
        return new SaveResult(uid, true, false);
    }

    public static SaveResult Failure()
    {
        return new SaveResult(Guid.Empty, false, false);
    }

    public static SaveResult Duplicate(Guid uid)
    {
        return new SaveResult(uid, false, true);
    }
}