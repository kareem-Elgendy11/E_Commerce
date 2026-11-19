using Application.Dtos.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Common
{
    public class AuthResponseDto
    {
        public string? Accesstoken { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime ExpireRefreshAt { get; set; }
        public DateTime ExpireAt {  get; set; }
        public UserResDto user {  get; set; }=new UserResDto();
    }
}
