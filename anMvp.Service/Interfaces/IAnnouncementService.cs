using anMvp.Model.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace anMvp.Service.Interfaces
{
    public interface IAnnouncementService
    {
        // 前端获取公告数据
        Task<AnnouncementDto> GetAync();
        // 前端请求创建公告
        Task<AnnouncementDto> CreateAsync(CreateAnnouncementRequest request);
        // 前端请求修改公告
        Task<AnnouncementDto> UpdateAsync(UpdateAnnouncementRequest request);
        // 前端请求查询公告 返回数据列表 以及长度
        Task<(List<AnnouncementDto>, long total)> QueryAnnouncementRequestAsync(QueryAnnouncementRequest request);
        // 根据id删除公告
        Task<bool> DeleteAsync(long id);
    }
}
