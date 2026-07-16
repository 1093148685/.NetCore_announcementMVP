using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace anMvp.Model.DTOs
{
    // 返回数据的接口
    public class AnnouncementDto
    {
        public long ID { get; set; }
        public string Content { get; set; } = string.Empty;
        public bool IsEnable { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }
    }
    // 创建公告的接口数据 前端发送给后端的创建公告条件 内容和是否启用这两个即可；
    // 因为ID是自增的 创建时间会默认获取当前时间
    public class CreateAnnouncementRequest
    {
        public string Content { get; set; } = string.Empty;
        public bool IsEnable { get; set; } = true;
        
    }
    // 修改公告 前端给后端发送的修改条件 对某个公告（具体ID）
    public class UpdateAnnouncementRequest
    {
        public long ID { get; set; }
        public string Content { get; set; } = string.Empty;
        public bool IsEnable { get; set; }
    }
    // 查询公告 前端给后端发送的查询条件
    // 只有两个值是Dto里面有的 其他都是新增加的
    public class QueryAnnouncementRequest
    {
        public string? Content { get; set; } = string.Empty;
        public bool? IsEnable { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        // 分页 页起点 页大小
        public int PageNum { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
