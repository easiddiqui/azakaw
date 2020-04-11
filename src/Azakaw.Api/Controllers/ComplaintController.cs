using System.Collections.Generic;
using Azakaw.Api.Models;
using Azakaw.Domain;
using Azakaw.Domain.Models;
using Azakaw.Domain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Azakaw.Api.Controllers
{
    public class ComplaintController : BaseController
    {
        private readonly IComplaintService _complaintService;

        public ComplaintController(IComplaintService complaintService)
        {
            _complaintService = complaintService;
        }

        [HttpGet("{id}")]
        public ActionResult<ResponseModel<Complaint>> Get(int id)
        {
            var response = _complaintService.GetById(id);

            return response;
        }

        [HttpGet("")]
        public ActionResult<ResponseModel<IEnumerable<Complaint>>> GetAll()
        {
            var response = _complaintService.GetAll();

            return response;
        }

        [HttpGet("{offset}/{fetch}")]
        public ActionResult<ResponseModel<IEnumerable<Complaint>>> GetAll(int offset, int fetch)
        {
            var response = _complaintService.GetAll(offset, fetch);

            return response;
        }

        [HttpGet("~/Status/{status}/Complaints")]
        public ActionResult<ResponseModel<IEnumerable<Complaint>>> GetAllByStatus(ComplaintStatus status)
        {
            var response = _complaintService.GetAllByStatus(status);

            return response;
        }

        [HttpGet("~/User/{userId}/Complaints"), Authorize(Roles = "Admin")]
        public ActionResult<ResponseModel<IEnumerable<Complaint>>> GetAllByUserId(int userId)
        {
            var response = _complaintService.GetAllByUserId(userId);

            return response;
        }

        [HttpGet("History")]
        public ActionResult<ResponseModel<IEnumerable<Complaint>>> GetAllMyComplaints()
        {
            var response = _complaintService.GetAllByUserId(WorkContext.CurrentUser.Id);

            return response;
        }

        [HttpPost]
        public ActionResult<ResponseModel<Complaint>> Create(ComplaintModel model)
        {
            var complaint = new Complaint
            {
                UserId = WorkContext.CurrentUser.Id,
                Message = model.Message
            };

            var response = _complaintService.Create(complaint);

            return response;
        }

        [HttpDelete]
        public ActionResult<ResponseModel<bool>> Delete(int id)
        {
            var response = _complaintService.SoftDelete(id);

            return response;
        }
    }
}