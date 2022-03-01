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
        private const string InsertNewOwnerCommand = "INSERT INTO owns(id, username)VALUES(@id,@user)";
        private const string RemoveFromPackageCommand = "DELETE FROM packages WHERE card_id=@id";
        private const string GetCardIdCommand = "SELECT id FROM owns WHERE username = @username";
        private const string GetCardsCommmand = "SELECT cards.id \"card_id\", type,element, monstertype, damage" +
                                                        "  FROM owns" +
                                                        "  JOIN cards ON owns.id = cards.id" +
                                                        " WHERE username = @username" +
                                                        ";";
        private const string GetActiveDeckCommand = "SELECT cards.id \"card_id\", type,element, monstertype, damage" +
                                                        "  FROM cards WHERE ismaindeck = @username";
        private const string UpdateUserCoinsCommand = "UPDATE users SET coins = coins-5 WHERE username = @username;";
        private const string SetMainDeckCommmand = "Update cards SET isMainDeck = @username WHERE id = @id;";
        private const string InsertUserDataCommand = "UPDATE users SET name = @name, bio = @bio, picture=@pic WHERE username = @username;";
        private const string GetUserStatCommand = "SELECT score FROM users WHERE username=@username";
        private const string GetUserDataCommand = "SELECT name,bio,picture FROM users WHERE username = @username";
        private const string GetRandomIdsCommand = "select id from owns where username=@username order by random() limit 4";

        private const string UpdateEloWinnerCommand = "UPDATE users SET score = score+3 WHERE username = @username;";
        private const string UpdateEloLooserCommand = "UPDATE users SET score = score-5 WHERE username = @username;";
        private const string GetScoreboardCommand = "SELECT score FROM users";
        private const string DeconfigureDeckCommand = "UPDATE cards SET isMainDeck = null WHERE isMainDeck = @username;";

        private readonly NpgsqlConnection _connection;

        public DatabaseUserRepository(NpgsqlConnection connection)
        {
            _connection = connection;
            EnsureTables();
        }

        public List<Card> allCards(User user)
        {
            List<Card> deck = new();

            using var cmd = new NpgsqlCommand(GetCardsCommmand, _connection);
            cmd.Parameters.AddWithValue("username", user.Username);
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                deck.Add(ResolveCard(reader));
            }
            return deck;
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
            string response = "";
            using var cmd = new NpgsqlCommand(GetUserDataCommand, _connection);
            cmd.Parameters.AddWithValue("username", user.Username);
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {


                response += "Name: " + reader["name"] + " Bio: " + reader["bio"] + " Picture: " + reader["picture"];
            }
            return response;
        }

        public void UpdateDeck(User user, List<Card> package)
        {

            for (int i = 0; i < package.Count; i++)
            {
                var cmd = new NpgsqlCommand(InsertNewOwnerCommand, _connection);
                cmd.Parameters.AddWithValue("id", package[i].Id);
                cmd.Parameters.AddWithValue("user", user.Username);
                cmd.ExecuteNonQuery();
                var cmd2 = new NpgsqlCommand(RemoveFromPackageCommand, _connection);
                cmd2.Parameters.AddWithValue("id", package[i].Id);
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
            using var cmd = new NpgsqlCommand(InsertUserDataCommand, _connection);
            cmd.Parameters.AddWithValue("name", userName);
            cmd.Parameters.AddWithValue("bio", Bio);
            cmd.Parameters.AddWithValue("pic", picture);
            cmd.Parameters.AddWithValue("username", user.Username);
            cmd.ExecuteNonQuery();

        }

        public List<Card> ShowActiveDeck(User user)
        {
            List<Card> deck = new();

            using var cmd = new NpgsqlCommand(GetActiveDeckCommand, _connection);
            cmd.Parameters.AddWithValue("username", user.Username);
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                deck.Add(ResolveCard(reader));
            }

            return deck;
        }

        public List<Card> ShowWholeDeck(User user)
        {
            List<Card> deck = new();

            using var cmd = new NpgsqlCommand(GetCardsCommmand, _connection);
            cmd.Parameters.AddWithValue("username", user.Username);
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                deck.Add(ResolveCard(reader));
            }

            return deck;
        }
        public string GetStat(User user)
        {
            string response = "";
            using var cmd = new NpgsqlCommand(GetUserStatCommand, _connection);
            cmd.Parameters.AddWithValue("username", user.Username);
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {

                if (reader["score"] != DBNull.Value)
                    response = Convert.ToString(reader["score"]);
            }
            return response;
        }

        private Card ResolveCard(NpgsqlDataReader reader)
        {
            Card card;
            string element = Convert.ToString(reader["element"]);
            string mtype = Convert.ToString(reader["monstertype"]);
            string damage = Convert.ToString(reader["damage"]);
            string id = Convert.ToString(reader["card_id"]);
            if (Convert.ToString(reader["type"]) == "Monster")
            {
                card = new Monster(getElement(Convert.ToString(reader["element"])), getMonsterType(Convert.ToString(reader["monstertype"])), Convert.ToInt32(reader["damage"]), Convert.ToString(reader["card_id"]));

            }
            else
            {
                card = new Spell(getElement(Convert.ToString(reader["element"])), Convert.ToInt32(reader["damage"]), Convert.ToString(reader["card_id"]));
            }


            return card;
        }

        public void UpdateActiveDeck(User user, List<Card> cards)
        {
            var cmd2 = new NpgsqlCommand(DeconfigureDeckCommand, _connection);
            cmd2.Parameters.AddWithValue("username", user.Username);
            cmd2.ExecuteNonQuery();
            //check later if you own the card!!
            for (int i = 0; i < cards.Count; i++)
            {

                var cmd = new NpgsqlCommand(SetMainDeckCommmand, _connection);
                cmd.Parameters.AddWithValue("id", cards[i].Id);
                cmd.Parameters.AddWithValue("username", user.Username);

                cmd.ExecuteNonQuery();



            }
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
                Password = Convert.ToString(record["password"]),
                Coins = Convert.ToInt32(record["coins"]),
                Score = Convert.ToInt32(record["score"]),
                Name = Convert.ToString(record["name"]),
                Bio = Convert.ToString(record["bio"]),
                Picture = Convert.ToString(record["picture"])

            };
            return user;
        }
        public MonsterType getMonsterType(string type)
        {
            switch (type)
            {
                case "Goblin": return MonsterType.Goblin;
                case "Dragon": return MonsterType.Dragon;
                case "Wizzard": return MonsterType.Wizzard;
                case "Ork": return MonsterType.Ork;
                case "Knight": return MonsterType.Knight;
                case "Kraken": return MonsterType.Kraken;
                case "Elve": return MonsterType.Elve;
                case "Troll": return MonsterType.Troll;
                default: return MonsterType.Goblin;
            }
        }
        public ElementType getElement(string element)
        {
            switch (element)
            {
                case "Water": return ElementType.Water;
                case "Fire": return ElementType.Fire;
                default: return ElementType.Normal;
            }
        }



        public void UpdateUserCoins(string username)
        {
            using var cmd = new NpgsqlCommand(UpdateUserCoinsCommand, _connection);
            cmd.Parameters.AddWithValue("username", username);
            cmd.ExecuteNonQuery();
        }

        public void updateElo(User playerOne, User playerTwo, string winner)
        {
            using var cmd = new NpgsqlCommand(UpdateEloWinnerCommand, _connection);
            using var cmd2 = new NpgsqlCommand(UpdateEloLooserCommand, _connection);
            if (winner == "A")
            {
                cmd.Parameters.AddWithValue("username", playerOne.Username);
                cmd2.Parameters.AddWithValue("username", playerTwo.Username);
                cmd.ExecuteNonQuery();
                cmd2.ExecuteNonQuery();
            }
            else if (winner == "B")
            {
                cmd.Parameters.AddWithValue("username", playerTwo.Username);
                cmd2.Parameters.AddWithValue("username", playerOne.Username);
                cmd.ExecuteNonQuery();
                cmd2.ExecuteNonQuery();

            }

        }

        public string[] GetRandomIds(User user)
        {
            List<string> randomIds = new List<string>();
            using var cmd = new NpgsqlCommand(GetRandomIdsCommand, _connection);
            cmd.Parameters.AddWithValue("username", user.Username);
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                if (reader["id"] != DBNull.Value)
                {
                    randomIds.Add(Convert.ToString(reader["id"]));
                }

            }
            return randomIds.ToArray();
        }

        public void UpdateActiveDeckRandom(User user, List<Card> cards)
        {
            var cmd2 = new NpgsqlCommand(DeconfigureDeckCommand, _connection);
            cmd2.Parameters.AddWithValue("username", user.Username);
            cmd2.ExecuteNonQuery();
            //check later if you own the card!!
            for (int i = 0; i < cards.Count; i++)
            {
                var cmd = new NpgsqlCommand(SetMainDeckCommmand, _connection);
                cmd.Parameters.AddWithValue("id", cards[i].Id);
                cmd.Parameters.AddWithValue("username", user.Username);
                cmd.ExecuteNonQuery();




            }
        }

        public string GetScoreboard()
        {
            string response = "";
            int i = 1;
            using var cmd = new NpgsqlCommand(GetScoreboardCommand, _connection);
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {

                if (reader["score"] != DBNull.Value)
                    response += Convert.ToString(i) + ") " + Convert.ToString(reader["score"]) + "\n";

                i++;
            }
            return response;
        }
    }
}
