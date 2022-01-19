using Npgsql;
using SWE1HttpServer.app.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWE1HttpServer.app.DAL
{
    class DatabaseUserRepository : IUserRepository
    {
        private const string CreateTableCommand = "CREATE TABLE IF NOT EXISTS users" +
        "(username VARCHAR PRIMARY KEY, password VARCHAR, auth_token VARCHAR, coins INT DEFAULT 20," +
        "score INT Default 100, name VARCHAR Default ' ',bio VARCHAR Default ' ',picture VARCHAR Default ' ' );";

       

        private const string InsertUserCommand = "INSERT INTO users(username, password, auth_token) VALUES (@username, @password, @token)";
        private const string SelectUserByTokenCommand = "SELECT * FROM users WHERE auth_token=@token";
        private const string SelectUserByCredentialsCommand = "SELECT * FROM users WHERE username=@username AND password=@password";
    	private const string InsertNewOwnerCommand ="INSERT INTO owns(id, username)VALUES(@id,@user)";
        private const string RemoveFromPackageCommand ="DELETE FROM packages WHERE card_id=@id";

        private readonly NpgsqlConnection _connection;

        public DatabaseUserRepository(NpgsqlConnection connection)
        {
            _connection = connection;
            EnsureTables();
        }

        public List<Card> allCards(User user)
        {
            throw new NotImplementedException();
        }

        public User GetUserByAuthToken(string authToken)
        {
            User user = null;
            using (var cmd = new NpgsqlCommand(SelectUserByTokenCommand, _connection))
            {
                cmd.Parameters.AddWithValue("token", authToken);

                // take the first row, if any
                using var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    user = ReadUser(reader);
                }
            }
            return user;
        }

        public User GetUserByCredentials(string username, string password)
        {
            User user = null;
            using (var cmd = new NpgsqlCommand(SelectUserByCredentialsCommand, _connection))
            {
                cmd.Parameters.AddWithValue("username", username);
                cmd.Parameters.AddWithValue("password", password);

                // take the first row, if any
                using var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    user = ReadUser(reader);
                }
            }
            return user;
        }

        public string GetUserInfo(User user)
        {
            throw new NotImplementedException();
        }

        public void UpdateDeck(User user, List<Card> package)
        {
            for(int i = 0; i < package.Count; i++){
                var cmd = new NpgsqlCommand(InsertNewOwnerCommand, _connection);
                cmd.Parameters.AddWithValue("id",package[i].Id);
                cmd.Parameters.AddWithValue("user",user.Username);
                cmd.ExecuteNonQuery();
                var cmd2 = new NpgsqlCommand(RemoveFromPackageCommand,_connection);
                cmd2.Parameters.AddWithValue("id",package[i].Id);
                cmd2.ExecuteNonQuery();


            }
            
        }
        public bool InsertUser(User user)
        {
            var affectedRows = 0;
            try
            {
                using var cmd = new NpgsqlCommand(InsertUserCommand, _connection);
                cmd.Parameters.AddWithValue("username", user.Username);
                cmd.Parameters.AddWithValue("password", user.Password);
                cmd.Parameters.AddWithValue("token", user.Token);



                affectedRows = cmd.ExecuteNonQuery();
            }
            catch (PostgresException)
            {
                // this might happen, if the user already exists (constraint violation)
                // we just catch it an keep affectedRows at zero
            }
            return affectedRows > 0;
        }

        public void SetUserInfo(User user, string userName, string Bio, string picture)
        {
            throw new NotImplementedException();
        }

        public List<Card> ShowActiveDeck(User user)
        {
            throw new NotImplementedException();
        }

        public List<Card> ShowWholeDeck(User user)
        {
            throw new NotImplementedException();
        }

        public void UpdateActiveDeck(User user, List<Card> cards)
        {
            throw new NotImplementedException();
        }



        private void EnsureTables()
        {
            using var cmd = new NpgsqlCommand(CreateTableCommand, _connection);
             cmd.ExecuteNonQuery();

        }

        private User ReadUser(IDataRecord record)
        {
            var user = new User
            {
                Username = Convert.ToString(record["username"]),
                Password = Convert.ToString(record["password"])
            };
            return user;
        }
    }
}
