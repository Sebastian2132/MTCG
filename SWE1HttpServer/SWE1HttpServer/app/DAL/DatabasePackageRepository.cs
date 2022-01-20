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
        private const string CreateTableCardCommand = "CREATE TABLE IF NOT EXISTS cards("+
        " id VARCHAR PRIMARY KEY NOT NULL,"  +
        "  type VARCHAR NOT NULL,"+
        " element VARCHAR NOT NULL,"+
        "monstertype VARCHAR,"+
         "damage INT NOT NULL,"+
         "isMainDeck VARCHAR REFERENCES users(username)"+
          ");";
        private const string CreateTablePackagesCommand = "CREATE TABLE IF NOT EXISTS packages(" +
                                                  "id INT," +
                                                  "card_id VARCHAR REFERENCES cards," +
                                                  "PRIMARY KEY(id, card_id)" +
                                                  ");";


        private const string InsertCardCommand = "INSERT INTO cards(id,  type, element,  monstertype, damage) VALUES(@id, @type, @element,  @m_type, @damage);";

        private const string InsertPackageCommand = "INSERT INTO packages(id, card_id) VALUES(@id, @card_id);";

        private const string GetNextPackageCommand = "SELECT cards.id \"card_id\", type, element, monstertype , damage" +
                                                     "  FROM packages " +
                                                     "  JOIN cards ON packages.card_id = cards.id" +
                                                     " WHERE packages.id = @id" +
                                                     ";";
        private const string CREATETABLEOWNERSHIPCOMMAND = "CREATE TABLE IF NOT EXISTS owns(" +
"id VARCHAR REFERENCES cards," +
"username VARCHAR REFERENCES users,PRIMARY KEY(id,username)" +
");";
        private const string GetNewPackageIdCommand = "SELECT max(id) + 1 \"id\" FROM packages;";
        private const string GetNextPackageIdCommand = "SELECT min(id) \"id\" FROM packages;";



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
            using var cmd3 = new NpgsqlCommand(CREATETABLEOWNERSHIPCOMMAND, _connection);


            cmd2.ExecuteNonQuery();
            cmd.ExecuteNonQuery();
            cmd3.ExecuteNonQuery();
        }

        public void AddPackage(List<Card> package)
        {
            int packageId = 0;






            using var cmd3 = new NpgsqlCommand(GetNewPackageIdCommand, _connection);
            using var reader = cmd3.ExecuteReader();
            if (reader.Read())
            {
                if (reader["id"] != DBNull.Value)
                    packageId = Convert.ToInt32(reader["id"]);
            }

            reader.Close();
            //Change to get acutall Id




            foreach (var card in package)
            {
                //saves every card into card table(DB)
                using var cmd = new NpgsqlCommand(InsertCardCommand, _connection);
                cmd.Parameters.AddWithValue("id", card.Id);
                cmd.Parameters.AddWithValue("type", card.type.ToString());
                cmd.Parameters.AddWithValue("element", card.element.ToString());
                cmd.Parameters.AddWithValue("m_type", ((card.type == CardType.Monster) ? ((Monster)card).monsterType.ToString() : DBNull.Value));
                cmd.Parameters.AddWithValue("damage", card.Damage);
                cmd.ExecuteNonQuery();
                //Saves the connection package---card
                using var cmd2 = new NpgsqlCommand(InsertPackageCommand, _connection);
                cmd2.Parameters.AddWithValue("id", packageId);
                cmd2.Parameters.AddWithValue("card_id", card.Id);

                cmd2.ExecuteNonQuery();
            }
            packageId = packageId + 1;
        }

        public List<Card> GetPackage()
        {
            List<Card> package = new();
            //TODO Richtige id einstellen dann kommen auch die richtigen packages!!!!
            var id = 0;
            using var cmd3 = new NpgsqlCommand(GetNextPackageIdCommand, _connection);
            using var reader = cmd3.ExecuteReader();
            if (reader.Read())
            {
                if (reader["id"] != DBNull.Value)
                    id = Convert.ToInt32(reader["id"]);
            }
            reader.Close();
            Card card;
            using var cmd = new NpgsqlCommand(GetNextPackageCommand, _connection);
            cmd.Parameters.AddWithValue("id", id);
            using var reader2 = cmd.ExecuteReader();
            while (reader2.Read())
            {
                card = ResolveCard(reader2);
                package.Add(card);
            }
            reader2.Close();
            return package;
        }

        public Card ResolveCard(IDataRecord reader)
        {
            Card card;

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
        private void removePackageandCard()
        {

        }
    }
}






