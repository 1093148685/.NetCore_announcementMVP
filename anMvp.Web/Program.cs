using anMvp.Model.Entities;
using anMvp.Repository;
using anMvp.Service.Implements;
using anMvp.Service.Interfaces;
using SqlSugar;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// 连接数据库信息
var ConnectString = builder.Configuration.GetConnectionString("MYSQL");
// 配置ISqlSugarClient
builder.Services.AddScoped<ISqlSugarClient>(s =>
{
    // 创建一个上下文客户端对象
    var db = new SqlSugarClient(new ConnectionConfig()
    {
        // 连接字符串
        ConnectionString = ConnectString,
        // 数据库类型
        DbType = DbType.MySql,
        // 自动关闭数据库连接
        IsAutoCloseConnection = true,
        // 更多配置
        MoreSettings = new ConnMoreSettings()
        {
            IsAutoRemoveDataCache = true
        }
    });
    db.CodeFirst.InitTables(typeof(Announcement));
    return db;
});

// 注入DbContext依赖
builder.Services.AddScoped<DbContext>();
// 注入依赖Announcement
builder.Services.AddScoped<IAnnouncementService, AnnouncementService>();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
