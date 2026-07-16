using anMvp.Model.DTOs;
using anMvp.Model.Entities;
using anMvp.Repository;
using anMvp.Service.Interfaces;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace anMvp.Service.Implements
{
    public class AnnouncementService : IAnnouncementService
    {
        private readonly DbContext _db;

        public AnnouncementService(DbContext db)
        {
            _db = db;
        }

        public async Task<AnnouncementDto> CreateAsync(CreateAnnouncementRequest create)
        {
            // 创建公告实体对象
            var announcement = new Announcement()
            {
                Content = create.Content,
                IsEnable = create.IsEnable,
                CreateTime = DateTime.Now,
                UpdateTime = DateTime.Now

            };

            // 判断是否启用公告
            if (create.IsEnable)
            {
                // 如果启用的话 那么把数据库中其他记录设置为false
                await _db.Db.Updateable<Announcement>()
                    .SetColumns(it => it.IsEnable == false)
                    .Where(it => !it.IsEnable)
                    .ExecuteCommandAsync();
            }
            // 开始插入数据 
            // 执行后返回该记录的id
            var id = await _db.Db.Insertable(announcement).ExecuteReturnBigIdentityAsync();
            announcement.ID = id;
            // 返回数据
            return new AnnouncementDto()
            {
                ID = announcement.ID,
                Content = announcement.Content,
                IsEnable = announcement.IsEnable,
                CreateTime = announcement.CreateTime,
                UpdateTime = announcement.UpdateTime
            };
        }

        public async Task<bool> DeleteAsync(long id)
        {
            var result = await _db.Db.Updateable<Announcement>()
                .SetColumns(id=>id.IsDelete == false)
                .Where(d => d.ID == id)
                .ExecuteCommandAsync();

            return result > 0;

        }

        public async Task<AnnouncementDto> GetAync()
        {
            // 筛选出没有被删除的最新的那条数据
            var announcement = await _db.Db.Queryable<Announcement>()
                .Where(it => !it.IsDelete)
                .OrderByDescending(it => it.CreateTime)
                .FirstAsync();
            // 判断是否为null
            if (announcement == null)
            {
                return new AnnouncementDto();
            }
            return new AnnouncementDto()
            {
                ID = announcement.ID,
                Content = announcement.Content,
                IsEnable = announcement.IsEnable,
                CreateTime = announcement.CreateTime,
                UpdateTime = announcement.UpdateTime
            };


        }
        // 查询==判断
        // where---类型是ISugarQueryable<T>
        // 创建一个私有分发用于判断用 返回类型是ISugarQueryable<T>
        // 然后 在where判断这个基础上继续进行链式操作 如 获取总数CountAsync() 以及进行分页操作

        // 创建BuildQueryConditions 参数是query 返回类型ISugarQueryable<Announcement>
        private ISugarQueryable<Announcement> BuildQueryConditions(QueryAnnouncementRequest request)
        {
            var query = _db.Db.Queryable<Announcement>()
                .Where(it => !it.IsDelete);
            // 判断内容是否为空
            if (string.IsNullOrEmpty(request.Content))
            {
                query = query.Where(it => it.Content == request.Content);
            }
            // 判断IsEnable是否为空
            if (request.IsEnable.HasValue)
            {
                query = query.Where(it => it.IsEnable == request.IsEnable);
            }
            // 判断开始时间
            if (request.StartDate.HasValue)
            {
                query = query.Where(it => it.CreateTime >= request.StartDate);
            }
            // 判断结束时间
            if (request.EndDate.HasValue)
            {
                query = query.Where(it => it.CreateTime <= request.EndDate);
            }
            return query;
        }

        // 创建ExecutedPaginatedQueryAsync
        private async Task<(List<AnnouncementDto>,long total)> ExecutedPaginatedQueryAsync(ISugarQueryable<Announcement> query,int pageNum,int pageSize)
        {
            // 总数
            long total = await query.CountAsync();

            // 分页数
            var list = await query
                .OrderBy(it => it.CreateTime, OrderByType.Desc)
                .Skip((pageNum - 1) * 10)
                .Take(pageSize)
                .Select(it => new AnnouncementDto
                {
                    ID = it.ID,
                    Content = it.Content,
                    IsEnable = it.IsEnable,
                    CreateTime = it.CreateTime,
                    UpdateTime = it.UpdateTime
                }).ToListAsync();

            return (list, total);
                
        }
        public async Task<(List<AnnouncementDto>, long total)> QueryAnnouncementRequestAsync(QueryAnnouncementRequest request)
        {
            // 调用私有创建查询条件的方法
            var query = BuildQueryConditions(request);
            // 返回查询列表和总数
            return await ExecutedPaginatedQueryAsync(query, request.PageNum, request.PageSize);
        }
        
        public async Task<AnnouncementDto> UpdateAsync(UpdateAnnouncementRequest update)
        {
            // 先查询该数据
            var announcemnt = await _db.Db.Queryable<Announcement>()
                .Where(it => it.ID == update.ID && it.IsDelete == false)
                .FirstAsync();
            // 再判断是否为空
            if(announcemnt == null)
            {
                return new AnnouncementDto();
            }


            // 修改
            announcemnt.Content = update.Content;
            announcemnt.IsEnable = update.IsEnable;
            announcemnt.UpdateTime = DateTime.Now;

            // 再判断是否启用 
            if(update.IsEnable == true)
            {
                await _db.Db.Updateable<Announcement>()
                    .SetColumns(it => it.IsEnable == false)
                    .Where(it => it.ID!=update.ID&&!it.IsDelete)
                    .ExecuteCommandAsync();
            }
            // 启用就把其他公告IsEnable改为false

            // 返回Dto实体数据
            return new AnnouncementDto
            {
                ID = announcemnt.ID,
                Content = announcemnt.Content,
                IsEnable = announcemnt.IsEnable,
                CreateTime = announcemnt.CreateTime,
                UpdateTime = announcemnt.UpdateTime
            };
        }
    }
}
