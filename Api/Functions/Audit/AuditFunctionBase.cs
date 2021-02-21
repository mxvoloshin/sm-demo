using System.Collections.Generic;
using System.Threading.Tasks;
using BlazorApp.Api.Services;
using BlazorApp.Shared;
using BlazorApp.Shared.Audit;

namespace BlazorApp.Api.Functions.Audit
{
    public abstract class AuditFunctionBase
    {
        private readonly IBlobService _blobService;

        protected AuditFunctionBase(IBlobService blobService)
        {
            _blobService = blobService;
        }

        protected async Task ProcessPhotosAsync(IEnumerable<AuditItemGroupDto> groupsDto)
        {
            foreach (var auditItemGroupDto in groupsDto)
            {
                foreach (var auditItemDto in auditItemGroupDto.Items)
                {
                    foreach (var auditPhotoDto in auditItemDto.Photos)
                    {
                        if (auditPhotoDto.Content == null)
                        {
                            if (auditPhotoDto.Removed)
                            {
                                await _blobService.RemoveBlobAsync(auditPhotoDto.Name);
                            }
                        }
                        else
                        {
                            var imageDto = new ImageDto
                            {
                                Name = auditPhotoDto.Name,
                                ContentType = auditPhotoDto.ContentType,
                                Content = auditPhotoDto.Content
                            };

                            var imageUri = await _blobService.UploadImageAsync(imageDto);
                            auditPhotoDto.PreviewUrl = imageUri.AbsoluteUri;
                        }
                    }
                }
            }
        }
    }
}