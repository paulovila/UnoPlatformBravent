using System;

namespace UnoWebApiSwagger.WebApi
{
    public class SessionDto
    {
        public Guid StaffId { get; set; }
        public string StaffNickName { get; set; }
        public string RoleCode { get; set; }
        public string StaffFullName { get; set; }
        public string StaffRoleName { get; set; }
        public bool IsAdmin { get; set; }
    }
}