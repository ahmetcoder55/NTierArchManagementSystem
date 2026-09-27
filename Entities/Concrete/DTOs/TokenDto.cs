using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete.DTOs
{
    public record TokenDto
    {
        public string AccessToken { get; init; }
        public string RefreshToken { get; init; }
    }
}
