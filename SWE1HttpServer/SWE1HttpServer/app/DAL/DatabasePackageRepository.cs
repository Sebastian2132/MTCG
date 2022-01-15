using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SWE1HttpServer.app.Models;
using Npgsql;

namespace SWE1HttpServer.app.DAL
{
    class DatabasePackageRepository : IPackageRepository
    {
        private const string CreateTableCardCommand ="CREATE TABLE IF NOT EXISTS cards( id VARCHAR  PRIMARY KEY NOT NULL, type VARCHAR NOT NULL, element VARCHAR NOT NULL, monsterType VARCHAR, damage INT NOT NULL);";
        private const string CreateTablePackagesCommand = "CREATE TABLE IF NOT EXISTS packages(" +
                                                  "id INT," +
                                                  "card_id VARCHAR REFERENCES cards," +
                                                  "PRIMARY KEY(id, card_id)" +
                                                  ");";

        
        private const string InsertCardCommand = "INSERT INTO cards(card_id, element, type, damage, monster_type) VALUES(@id, @element, @type, @damage, @m_type);";

        private const string InsertPackageCommand = "INSERT INTO packages(id, card_id) VALUES(@id, @card_id);";

        private const string GetNextPackageIdCommand = "SELECT max(id) + 1 \"max_id\" FROM packages;";

        private const string GetLowestPackageIdCommand = "SELECT min(id) \"min_id\" FROM packages;";

        private const string GetNextPackageCommand = "SELECT cards.card_id \"card_id\", element, type, damage, monster_type " +
                                                     "  FROM packages " +
                                                     "  JOIN cards ON packages.card_id = cards.card_id" +
                                                     " WHERE packages.id = @id" +
                                                     ";";

        private const string DeletePackageCommand = "DELETE FROM packages WHERE id = @id;";

        private readonly NpgsqlConnection _connection;

        public DatabasePackageRepository(NpgsqlConnection connection)
        {
            _connection = connection;
            EnsureTables();
        }

        private void EnsureTables()
        {
            using var cmd = new NpgsqlCommand(CreateTablePackagesCommand, _connection);
            using var cmd2 = new NpgsqlCommand(CreateTableCardCommand, _connection);
            
            cmd2.ExecuteNonQuery();
            cmd.ExecuteNonQuery();
        }

        public void AddPackage(List<Card> package)
        {
            foreach (var card in package)
            {
                using var cmd = new NpgsqlCommand(InsertCardCommand, _connection);
                cmd.Parameters.AddWithValue("id", card.Id);
                cmd.Parameters.AddWithValue("element", card.element.ToString());
                cmd.Parameters.AddWithValue("type", card.type.ToString());
                cmd.Parameters.AddWithValue("damage", card.Damage);
                cmd.Parameters.AddWithValue("m_type", ((card.type == CardType.Monster) ? ((Monster)card).monsterType.ToString() : DBNull.Value));
                cmd.ExecuteNonQuery();
                using var cmd2 = new NpgsqlCommand(InsertPackageCommand, _connection);
                cmd2.Parameters.AddWithValue("card_id", card.Id);
                cmd2.ExecuteNonQuery();
            }
        }

        public List<Card> GetPackage()
        {
            List<Card> package = new();
            var id = GetLowestPackageId();
            using var cmd = new NpgsqlCommand(GetNextPackageCommand, _connection);
            cmd.Parameters.AddWithValue("id", id);

            // take the first row, if any
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                package.Add(ReadCard(reader));
            }

            reader.Close();

            if (!DeletePackage(id))
            {
                Console.WriteLine("Deleted package did not have 5 cards!");
            }

            return package;
        }

        public static Card ReadCard(IDataRecord record)
        {
            Card card;
            Enum.TryParse<ElementType>(Convert.ToString(record["element"]), out var elementType);
            if (Convert.ToString(record["type"]) == "Monster")
            {
                Enum.TryParse<MonsterType>(Convert.ToString(record["monster_type"]), out var monsterType);

                card = new Monster( elementType, monsterType,
                    Convert.ToInt32(record["damage"]),Convert.ToString(record["card_id"]));
            }
            else
            {
                card = new Spell(elementType,
                    Convert.ToInt32(record["damage"]),Convert.ToString(record["card_id"]) );
            }
            return card;
        }

        private int GetNextPackageId()
        {
            int id = 0;
            using var cmd = new NpgsqlCommand(GetNextPackageIdCommand, _connection);

            // take the first row, if any
            using var reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                if (reader["max_id"] != DBNull.Value)
                    id = Convert.ToInt32(reader["max_id"]);
            }

            reader.Close();

            return id;
        }

        private int GetLowestPackageId()
        {
            int id = 0;
            using var cmd = new NpgsqlCommand(GetLowestPackageIdCommand, _connection);

            // take the first row, if any
            using var reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                if (reader["min_id"] != DBNull.Value)
                    id = Convert.ToInt32(reader["min_id"]);
            }

            reader.Close();

            return id;
        }

        private bool DeletePackage(int id)
        {
            var affectedRows = 0;
            using var cmd = new NpgsqlCommand(DeletePackageCommand, _connection);
            cmd.Parameters.AddWithValue("id", id);

            // take the first row, if any
            affectedRows = cmd.ExecuteNonQuery();
            return affectedRows == 5;
        }
    }
}
