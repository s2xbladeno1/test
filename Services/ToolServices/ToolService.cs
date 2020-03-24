using AppTool.Dtos;
using AppTool.Model;
using AppTool.Services.SessionServices;
using AppTool.Services.ToolServices;
using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace AppTool.Services
{
    public class ToolService: IToolService
    {
        private readonly IConfiguration _config;
        public ToolService(IConfiguration config)
        {
            _config = config;
        }
        public IDbConnection Connection
        {
            get
            {
                return new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            }
        }
        public async Task<List<Tool>> GetAll()
        {
            using (IDbConnection conn = Connection)
            {
                string sql = @"select [Tool].Title, [Tool].Description, [User].Fullname from [Tool],[User] where [Tool].Create_id = [User].Id";
                conn.Open();
                var rs = await conn.QueryAsync<Tool>(sql);
                return rs.ToList();
            }
        }

        public async Task<List<Tool>> Search(FilterDto dto)
        {
            using (IDbConnection conn = Connection)
            {
                string sql =  @"select * from [Tool] where (Tags like '%" + dto.Tags + "%') and (Creator like '%" + dto.Creator + "%') and (Title like '%" + dto.Title + "%') and (Description like '%" + dto.Description + "%') ";
                conn.Open();
                var rs = await conn.QueryAsync<Tool>(sql);
                return rs.ToList();
            }
        }

        public async Task<bool> Register(ToolDto dto)
        {
            using (IDbConnection conn = Connection)
            {
                try
                {
                    string sql = @"insert into [Tool]([Create_id],[Title],[Description],[File_url]) values(@creator,@title,@desc,@url)";
                    conn.Open();
                    conn.QueryFirstOrDefault<Tool>(sql, new { creator = dto.Create_id, title = dto.Title, desc = dto.Description, url = dto.File_url });
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }
        public async Task<bool> Update(ToolDto dto, int id)
        {
            using (IDbConnection conn = Connection)
            {
                try
                {
                    string sqlGetTool = "select * from [Tool] where Id = @id1";
                    conn.Open();
                    var tool = await conn.QueryFirstOrDefaultAsync<Tool>(sqlGetTool, new { id1 = id });
                    tool.Title = (dto.Title == null)? tool.Title : dto.Title;
                    tool.Description = (dto.Description == null) ? tool.Description : dto.Description;
                    tool.Create_id = (dto.Create_id == 0) ? tool.Create_id : dto.Create_id;
                    tool.File_url = (dto.File_url == null) ? tool.File_url : dto.File_url;
                    string sqlUpdate = @"update [Tool] set Create_id = @creator, Title = @title, Description = @desc, File_url = @url where Id = @idTool";
                    await conn.QueryAsync<Tool>(sqlUpdate, new { idTool = id, creator = tool.Create_id, title = tool.Title, desc = tool.Description, url = tool.File_url });
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }
        public async Task<List<Tool>> MyTool(int id)
        {
            using (IDbConnection conn = Connection)
            {
                string sql = @"select * from [Tool] where Create_id = @idSession";
                conn.Open();
                var listMyTool = await conn.QueryAsync<Tool>(sql, new { idSession = id });
                return listMyTool.ToList();
            }
        }
    }
}
