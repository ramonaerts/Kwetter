using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthenticationService.Entities;
using MySql.Data.MySqlClient;

namespace AuthenticationService.DAL
{
    public class AuthenticationContext : IAuthenticationContext
    {
        private readonly MySqlConnection _connection = new MySqlConnection("Server=authenticationdatabase;Port=3306;Database=kwetter;Uid=root;password=example");
        public User LoginUser(string email, string password)
        {
            try
            {
                _connection.Open();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            var mscCommand = new MySqlCommand("SELECT * FROM authentication WHERE email = @mail && password = @pw;", _connection);
            mscCommand.Parameters.AddWithValue("@mail", email);
            mscCommand.Parameters.AddWithValue("@pw", password);

            using (var reader = mscCommand.ExecuteReader())
            {
                while (reader.Read())
                {
                    var user = new User
                    {
                        Id = (int)reader["id"],
                        Email = (string)reader["email"],
                        ProfileName = (string)reader["profilename"],
                        Username = (string)reader["username"],
                        Password = (string)reader["password"]
                    };

                    return user;
                }
            }
            return null;
        }
    }
}
