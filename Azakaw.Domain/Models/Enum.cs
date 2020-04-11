namespace Azakaw.Domain.Models
{
    public enum Role
    {
        Admin = 100,
        User = 10000
    }

    public enum ComplaintStatus
    {
        Queued = 100,
        InProgress = 200,
        Closed = 300
    }
}