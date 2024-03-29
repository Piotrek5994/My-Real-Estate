﻿using Core.Commend.Create;
using Core.Commend.Update;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.IRepositories;

public interface IAuthRepository
{
    Task<string> Login(CreateLogin login);
    Task<string> Refresh(string token);
    Task<bool> UpdateRole(string userId, string? role);
    Task<bool> ChangePassword(UpdatePassword password);
}
