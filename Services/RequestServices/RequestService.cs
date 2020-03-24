using AppTool.Dtos;
using AppTool.Model;
using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace AppTool.Services.RequestServices
{
    public class RequestService: IRequestService
    {
        private readonly IConfiguration _config;
        public RequestService(IConfiguration config)
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
        public async Task<bool> Add(RequestDto dto)
        {
            using (IDbConnection conn = Connection)
            {
                try
                {
                    string sql = @"insert into [Request]([Tool_id],[Requester_id],[Team_id],[Project_id],[Reason]) values(@toolId,@reqId,@teamId,@proId,@reason)";
                    conn.Open();
                    await conn.QueryAsync<Request>(sql, new { toolId = dto.Tool_id, reqId = dto.Requester_id, teamId = dto.Team_id, proId = dto.Project_id, reason = dto.Reason });
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        public async Task<List<Request>> MyRequest(int id)
        {
            using (IDbConnection conn = Connection)
            {
                string sql = @"select * from [Request] where Requester_id = @idRequest";
                conn.Open();
                var listMyRequest = await conn.QueryAsync<Request>(sql, new { idRequest = id });
                return listMyRequest.ToList();
            }
        }
    }
}
