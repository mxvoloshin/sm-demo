using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Web.Http;
using BlazorApp.Api.Auth;
using BlazorApp.Api.Entities;
using BlazorApp.Api.Repository;
using BlazorApp.Api.Services;
using BlazorApp.Shared.Audit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Shared.ImageWrapper;

namespace BlazorApp.Api.Functions.Audit
{
    public class NewAuditFunction
    {
        private readonly IAuditRepository _auditRepository;
        private readonly IDateTimeService _dateTimeService;
        private readonly IBlobService _blobService;
        private readonly IImageProcessor _imageProcessor;

        public NewAuditFunction(IAuditRepository auditRepository, IDateTimeService dateTimeService, IBlobService blobService, IImageProcessor imageProcessor)
        {
            _auditRepository = auditRepository;
            _dateTimeService = dateTimeService;
            _blobService = blobService;
            _imageProcessor = imageProcessor;
        }

        [FunctionName("NewAudit")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req, ILogger log)
        {
            try
            {
                var claims = req.GetClaimsPrincipal();

                var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                var audit = JsonConvert.DeserializeObject<NewAuditDto>(requestBody);

                var groups = await LoadGroupsFromDtoAsync(audit.Groups);

                var entity = new Entities.Audit
                {
                    PartitionKey = $"{DateTime.Now.Year}{DateTime.Now.Month}",
                    RowKey = Guid.NewGuid().ToString(),
                    Timestamp = _dateTimeService.TableEntityTimeStamp,
                    FacilityId = audit.FacilityId,
                    StartTimeUtc = audit.StartTimeUtc,
                    FinishTimeUtc = audit.FinishTimeUtc,
                    GroupsJson = JsonConvert.SerializeObject(groups),
                    CreatedBy = claims.ClientId(),
                    CreatedAt = _dateTimeService.CurrentUtcDateTime
                };

                
                var result = await _auditRepository.CreateAsync(entity);

                return new OkObjectResult(result);
            }
            catch (Exception e)
            {
                return new ExceptionResult(e, true);
            }
        }

        private async Task<IList<AuditItemGroup>> LoadGroupsFromDtoAsync(IEnumerable<AuditItemGroupDto> auditItemGroupDtos)
        {
            var result = new List<AuditItemGroup>();

            foreach (var auditItemGroupDto in auditItemGroupDtos)
            {
                var auditItemGroup = new AuditItemGroup
                {
                    Order = auditItemGroupDto.Order, Title = auditItemGroupDto.Title
                };

                foreach (var auditItemDto in auditItemGroupDto.Items)
                {
                    var auditItem = new AuditItem
                    {
                        Order = auditItemDto.Order,
                        Title = auditItemDto.Title,
                        IsCheckedAvailable = auditItemDto.IsCheckedAvailable,
                        IsChecked = auditItemDto.IsChecked,
                        IsCommentsAvailable = auditItemDto.IsCommentsAvailable,
                        Comments = auditItemDto.Comments,
                        IsPhotoAvailable = auditItemDto.IsPhotoAvailable
                    };

                    foreach (var imageDto in auditItemDto.Photos)
                    {
                        var resizedImage = await _imageProcessor.CompressAsync(imageDto.Content, 50);
                        imageDto.Content = resizedImage;
                        var imageUri = await _blobService.UploadImageAsync(imageDto);
                        
                        auditItem.PhotosPreview.Add(imageUri.AbsoluteUri);
                    }

                    auditItemGroup.Items.Add(auditItem);
                }

                result.Add(auditItemGroup);
            }

            return result;
        }
    }
}