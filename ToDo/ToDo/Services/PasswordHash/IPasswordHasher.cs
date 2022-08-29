﻿namespace ToDo.Services.PasswordHash
{
    public interface IPasswordHasher
    {
        string HashPassword(string password);
        bool VerifyPassword(string password,string passwordHash);
    }
}
