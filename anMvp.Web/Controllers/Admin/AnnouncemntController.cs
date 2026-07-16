using anMvp.Comman.Result;
using anMvp.Model.DTOs;
using anMvp.Service.Implements;
using anMvp.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace anMvp.Web.Controllers.Admin
{
    [Route("api/admin/announcement")]
    [ApiController]
    public class AnnouncemntController : ControllerBase
    {
        // 引入依赖
        private readonly IAnnouncementService _announcementService;

        public AnnouncemntController(IAnnouncementService announcementService)
        {
            _announcementService = announcementService;
        }

        // get 获取最新且启用的公告
        [HttpGet]
        public async Task<Result<AnnouncementDto>> Get()
        {
            var result = await _announcementService.GetAync();
            return Result<AnnouncementDto>.Ok(result);
        }

        // 创建
        [HttpPost]
        public async Task<Result<AnnouncementDto>> Create([FromBody] CreateAnnouncementRequest request)
        {
            var result = await _announcementService.CreateAsync(request);
            return Result<AnnouncementDto>.Ok(result);
        }

        // 修改
        [HttpPut]
        public async Task<Result<AnnouncementDto>> Update([FromBody] UpdateAnnouncementRequest request)
        {
            var result = await _announcementService.UpdateAsync(request);
            return Result<AnnouncementDto>.Ok(result);
        }

        // 删除
        [HttpDelete("{id}")]
        public async Task<Result> Delete(long id)
        {
           var result = await _announcementService.DeleteAsync(id);
            return result ? Result.Ok() : Result.Fail("删除失败");
        }

        // 查询 如果查询复杂 那么就使用post 如果相对简单就使用get
        [HttpPost("list")]
        public async Task<Result<PageDto<AnnouncementDto>>> Query(QueryAnnouncementRequest request)
        {
            var (list,total) = await _announcementService.QueryAnnouncementRequestAsync(request);
            return Result<PageDto<AnnouncementDto>>.Ok(new PageDto<AnnouncementDto>
            {
                List = list,
                Total = (int)total,
                PageNum = request.PageNum,
                PageSize = request.PageSize
            });
        }
    }
}
