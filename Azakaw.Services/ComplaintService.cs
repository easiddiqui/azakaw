using System.Collections.Generic;
using System.Linq;
using Azakaw.Domain;
using Azakaw.Domain.Models;
using Azakaw.Domain.Repositories;
using Azakaw.Domain.Services;

namespace Azakaw.Services
{
    public class ComplaintService : BaseService<Complaint>, IComplaintService
    {
        public ComplaintService(IBaseRepository<Complaint> baseRepository) : base(baseRepository)
        {
        }

        public override ResponseModel<Complaint> Create(Complaint entity)
        {
            entity.ComplaintStatus = ComplaintStatus.Queued;
            return base.Create(entity);
        }

        public ResponseModel<IEnumerable<Complaint>> GetAllByStatus(ComplaintStatus status)
        {
            var query = BaseRepository.GetAll(x => x.ComplaintStatus == status);

            return query.Any()
                ? ResponseModel<IEnumerable<Complaint>>.GetSuccessResponse(query)
                : ResponseModel<IEnumerable<Complaint>>.GetNotFoundResponse();
        }

        public ResponseModel<IEnumerable<Complaint>> GetAllByUserId(int userId)
        {
            var query = BaseRepository.GetAll(x => x.UserId == userId);

            return query.Any()
                ? ResponseModel<IEnumerable<Complaint>>.GetSuccessResponse(query)
                : ResponseModel<IEnumerable<Complaint>>.GetNotFoundResponse();
        }
    }
}