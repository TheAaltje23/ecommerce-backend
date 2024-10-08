﻿using ecommerce_backend.Models;

namespace ecommerce_backend.Dto
{
    public class ReadUserDto
    {
        public long Id { get; init; }
        public string? Username { get; init; }
        public string? FirstName { get; init; }
        public string? LastName { get; init; }
        public string? Email { get; init; }
        public string? PhoneNumber { get; init; }
        public User.Role UserRole { get; init; }
    }
}