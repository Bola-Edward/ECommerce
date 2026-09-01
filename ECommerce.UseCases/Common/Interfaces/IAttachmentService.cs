using ECommerce.Domain.Common;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.UseCases.Common.Interfaces
{
    public interface IAttachmentService
    {
        Task<Result<string>> UploadAttachmentAsync(IFormFile file);
    }
}
