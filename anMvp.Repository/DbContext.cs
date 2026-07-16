using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SqlSugar;
namespace anMvp.Repository
{
    public class DbContext
    {
        private readonly ISqlSugarClient _db;

        public DbContext(ISqlSugarClient db)
        {
            _db = db;
        }
        // 创建一个公开属性，让外部可以操作数据库
        public ISqlSugarClient Db => _db;


    }
}
