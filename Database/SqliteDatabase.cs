
using System.Data.SQLite;
using System;
using System.Collections.Generic;
using Models;

namespace Database
{
    public class SqliteDatabase
    {
        private readonly string connectionString;

        public SqliteDatabase(string dbFilePath)
        {
            connectionString = $"Data Source={dbFilePath};Version=3;";
        }

        public void InitializeDatabase()
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                // Criação das tabelas
                string createTableQuery = @"
                    CREATE TABLE IF NOT EXISTS composicao (
                        id INTEGER PRIMARY KEY AUTOINCREMENT,
                        codigo TEXT,
                        nome TEXT,
                        nome_cientifico TEXT,
                        grupo TEXT
                    );

                    CREATE TABLE IF NOT EXISTS detalhes (
                        id INTEGER PRIMARY KEY AUTOINCREMENT,
                        food_id INTEGER,
                        nutriente TEXT,
                        valor TEXT,
                        unidade TEXT,
                        fonte TEXT,
                        FOREIGN KEY (food_id) REFERENCES composicao(id)
                    );";

                ExecuteNonQuery(createTableQuery, connection);
            }
        }

        public void SaveFoodData(List<FoodItem> foodItems)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                foreach (var foodItem in foodItems)
                {
                    InsertFoodItem(foodItem, connection);
                }
            }
        }

public void DisplayFoodData()
{
    using (var connection = new SQLiteConnection(connectionString))
    {
        connection.Open();

        string selectQuery = "SELECT * FROM composicao";
        using (var command = new SQLiteCommand(selectQuery, connection))
        using (var reader = command.ExecuteReader())
        {
            while (reader.Read())
            {
                Console.WriteLine($"ID: {reader["id"]}, Código: {reader["codigo"]}, Nome: {reader["nome"]}, Nome Científico: {reader["nome_cientifico"]}, Grupo: {reader["grupo"]}");
            }
        }
    }
}
        private void InsertFoodItem(FoodItem foodItem, SQLiteConnection connection)
        {
            string insertFoodQuery = "INSERT INTO composicao (codigo, nome, nome_cientifico, grupo) VALUES (@codigo, @nome, @nomeCientifico, @grupo)";
            ExecuteNonQuery(insertFoodQuery, connection, new Dictionary<string, object>
            {
                { "@codigo", foodItem.Codigo },
                { "@nome", foodItem.Nome },
                { "@nomeCientifico", foodItem.NomeCientifico },
                { "@grupo", foodItem.Grupo }
            });

            var foodId = connection.LastInsertRowId;

            foreach (var detail in foodItem.DetailData)
            {
                InsertFoodDetail(foodId, detail, connection);
            }
        }

        private void InsertFoodDetail(long foodId, string[] detail, SQLiteConnection connection)
        {
            string insertDetailQuery = "INSERT INTO detalhes (food_id, nutriente, valor, unidade, fonte) VALUES (@foodId, @nutriente, @valor, @unidade, @fonte)";
            ExecuteNonQuery(insertDetailQuery, connection, new Dictionary<string, object>
            {
                { "@foodId", foodId },
                { "@nutriente", detail[0] }, 
                { "@valor", detail[1] },      
                { "@unidade", detail[2] },   
                { "@fonte", detail[3] }      
            });
        }


        private void ExecuteNonQuery(string query, SQLiteConnection connection, Dictionary<string, object>? parameters = null)
        {
            using (var command = new SQLiteCommand(query, connection))
            {
                if (parameters != null)
                {
                    foreach (var param in parameters)
                    {
                        command.Parameters.AddWithValue(param.Key, param.Value ?? DBNull.Value);
                    }
                }

                command.ExecuteNonQuery();
            }
        }
    }
}