using PublicRequestApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PublicRequestApp.Models;
using Microsoft.Data.SqlClient;
using System.Data;


namespace PublicRequestApp.Repositories
{
    public class RequestRepository
    {
        private readonly AppDbContext _context;
        private readonly string _connectionString;

        public RequestRepository(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public List<Request> GetAllRequests()
        {
            return _context.Request.ToList();
        }

        public Request GetRequestById(int id)
        {
            return _context.Request.FirstOrDefault(r => r.Id == id);
        }

        public Request CreateRequest(Request request)
        {
            _context.Request.Add(request); 
            _context.SaveChanges();
            return request; 
        }
        public void UpdateRequest(Request publicRequest)
        {
            _context.Request.Update(publicRequest);
            _context.SaveChanges();
        }

        public void DeleteRequest(int id)
        {
            var publicRequest = _context.Request.Find(id);
            if (publicRequest != null)
            {
                _context.Request.Remove(publicRequest);
                _context.SaveChanges();
            }
        }
        public DataTable GetRequestsComparison()
        {
            DataTable result = new DataTable();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("GetRequestsByDepartment", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(result);
                }
            }

            return result;
        }
        public List<Dictionary<string, object>> ConvertDataTableToList(DataTable table)
        {
            var list = new List<Dictionary<string, object>>();

            foreach (DataRow row in table.Rows)
            {
                var dict = new Dictionary<string, object>();
                foreach (DataColumn col in table.Columns)
                {
                    dict[col.ColumnName] = row[col];
                }
                list.Add(dict);
            }

            return list;
        }
    }
}