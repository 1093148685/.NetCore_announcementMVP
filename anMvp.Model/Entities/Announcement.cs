using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SqlSugar;


namespace anMvp.Model.Entities
{
    [SugarTable("t_announcement")]
    public class Announcement
    {
        // 公告 
        // id 内容 是否启用 创建时间和更新时间 是否删除
        // ID 主键 自增
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public long ID { get; set; }
        // 公告内容
        [SugarColumn(Length = 1000,DefaultValue =" ")]
        public string Content { get; set; }
        // 是否启用
        public bool IsEnable { get; set; }
        // 创建时间
        public DateTime CreateTime { get; set; } = DateTime.Now;
        // 更新时间
        public DateTime UpdateTime { get; set; } = DateTime.Now;
        // 是否删除 0-false 1-true
        public bool IsDelete { get; set; } = false;
    }
}
