﻿using MediMax.Data.Enums;

namespace MediMax.Data.RequestModels
{
    public class UserUpdateRequestModel
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public int TypeUserId { get; set; }
    }
}
