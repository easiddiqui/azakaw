using System.Collections.Generic;
using Azakaw.Domain.Models;

namespace Azakaw.Domain.Services
{
    public interface IComplaintService : IBaseService<Complaint>
    {
        ResponseModel<IEnumerable<Complaint>> GetAllByStatus(ComplaintStatus status);
        ResponseModel<IEnumerable<Complaint>> GetAllByUserId(int userId);
    }
}