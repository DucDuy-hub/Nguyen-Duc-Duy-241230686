using Microsoft.AspNetCore.Mvc;
using Ndd_lesson6.Models;

namespace Ndd_lesson6.Controllers
{
    public class NddMemberController : Controller
    {
        private static List<Models.NddMember> _nddMembers = new List< NddMember>()
        {
         
            new NddMember
            {
                NddMemberId = Guid.NewGuid().ToString(),
                NddMemberUserName = "Duy Nguyen",
                NddMemberPassword = "123456",
                NddMemberEmail = "duynguyen@gmail.com",
                NddMemberFullName = "Nguyễn Đức Duy"
            },
            new NddMember
            {
                NddMemberId = Guid.NewGuid().ToString(),
                NddMemberUserName = "nguyenvana",
                NddMemberPassword = "123456",
                NddMemberEmail = "nguyenvana@gmail.com",
                NddMemberFullName = "Nguyễn Văn A"
            },
            new NddMember
            {
                NddMemberId = Guid.NewGuid().ToString(),
                NddMemberUserName = "tranthib",
                NddMemberPassword = "123456",
                NddMemberEmail = "tranthib@gmail.com",
                NddMemberFullName = "Trần Thị B"
            },
            new NddMember
            {
                NddMemberId = Guid.NewGuid().ToString(),
                NddMemberUserName = "leminhc",
                NddMemberPassword = "123456",
                NddMemberEmail = "leminhc@gmail.com",
                NddMemberFullName = "Lê Minh C"
            },
            new NddMember
            {
                NddMemberId = Guid.NewGuid().ToString(),
                NddMemberUserName = "phamvand",
                NddMemberPassword = "123456",
                NddMemberEmail = "phamvand@gmail.com",
                NddMemberFullName = "Phạm Văn D"
            }
        
    };
        public IActionResult NddIndex()
        {
            return View(_nddMembers);
        }
        
        public IActionResult NddCreate()
        {
            return View();
        }
        [HttpPost]

        public IActionResult NddCreate(NddMember nddMember)
        {
            nddMember.NddMemberId = Guid.NewGuid().ToString();
            _nddMembers.Add(nddMember);
            return RedirectToAction("NddIndex");
        }
        public IActionResult NddEdit(string id)
        {
            var nddMember = _nddMembers.FirstOrDefault(x => x.NddMemberId.Equals(id));
            return View(nddMember);
        }

        [HttpPost]

        public IActionResult NddEdit(string id, NddMember nddMember)
        {
            for (int i = 0; i < _nddMembers.Count; i++)
            {
                if (_nddMembers[i].NddMemberId == id)
                {
                    _nddMembers[i].NddMemberId = nddMember.NddMemberId;
                    _nddMembers[i].NddMemberUserName = nddMember.NddMemberUserName;
                    _nddMembers[i].NddMemberPassword = nddMember.NddMemberPassword;
                    _nddMembers[i].NddMemberEmail = nddMember.NddMemberEmail;
                    _nddMembers[i].NddMemberFullName = nddMember.NddMemberFullName;
                    break;
                }
            }
            return RedirectToAction("NddIndex");
        }
        public IActionResult NddGetDetails()
        {
            var nddMember = new Models.NddMember
            {
                NddMemberId = Guid.NewGuid().ToString(),
                NddMemberUserName = "Đức Duy",
                NddMemberPassword = "Duy123@",
                NddMemberEmail = "nguyenducduy04042006@gmail.com",
                NddMemberFullName = "Nguyễn Đức Duy"
            };
            return View(nddMember);
        }
    }
}
