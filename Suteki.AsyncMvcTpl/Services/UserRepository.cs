using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Suteki.AsyncMvcTpl.Models;

namespace Suteki.AsyncMvcTpl.Services
{
    public class UserRepository
    {
        public Task<User> GetUserById(int currentUserId)
        {
            var connection =
                new SqlConnection("Data Source=localhost;Initial Catalog=AsncMvcTpl;Integrated Security=SSPI;Asynchronous Processing=true");
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = "select * from [user] where Id = 10";
            command.CommandType = CommandType.Text;

            var readerTask = Task<SqlDataReader>.Factory.FromAsync(command.BeginExecuteReader, command.EndExecuteReader, null);
            return readerTask.ContinueWith(t =>
            {
                var reader = t.Result;
                try
                {
                    if (reader.HasRows)
                    {
                        reader.Read();
                        return new User((int)reader["Id"], (string)reader["Name"], (string)reader["Email"]);
                    }
                    throw new ApplicationException("No row with Id = 10 in user table");
                }
                finally 
                {
                    reader.Dispose();
                    command.Dispose();
                    connection.Dispose();
                }
            });
        }
    }
}