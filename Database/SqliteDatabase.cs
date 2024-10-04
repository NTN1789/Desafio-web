using System.Data.SQLite;
using System;
using System.Collections.Generic;
using Models;


namespace Database
{
    public class SqliteDatabase
    {
        private string connectionString;

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
                    );
                ";

                var command = new SQLiteCommand(createTableQuery, connection);
                command.ExecuteNonQuery();

                connection.Close();
            }
        }

        public void SaveFoodData(List<FoodItem> foodItems)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                foreach (var foodItem in foodItems)
                {
                    var query = "INSERT INTO composicao (codigo, nome, nome_cientifico, grupo) VALUES (@codigo, @nome, @nomeCientifico, @grupo)";
                    var command = new SQLiteCommand(query, connection);
                    command.Parameters.AddWithValue("@codigo", foodItem.Codigo);
                    command.Parameters.AddWithValue("@nome", foodItem.Nome);
                    command.Parameters.AddWithValue("@nomeCientifico", foodItem.NomeCientifico);
                    command.Parameters.AddWithValue("@grupo", foodItem.Grupo);
                    command.ExecuteNonQuery();

                    var foodId = connection.LastInsertRowId;

                   
                    foreach (var detail in foodItem.DetailData)
                    {
                        var detailQuery = "INSERT INTO detalhes (food_id, nutriente, valor, unidade, fonte) VALUES (@foodId, @nutriente, @valor, @unidade, @fonte)";
                        var detailCommand = new SQLiteCommand(detailQuery, connection);
                        detailCommand.Parameters.AddWithValue("@foodId", foodId);
                        detailCommand.Parameters.AddWithValue("@nutriente", detail[0]);
                        detailCommand.Parameters.AddWithValue("@valor", detail[1]);
                        detailCommand.Parameters.AddWithValue("@unidade", detail[2]);
                        detailCommand.Parameters.AddWithValue("@fonte", detail[7]); 
                        detailCommand.ExecuteNonQuery();
                    }
                }

                connection.Close();
            }
        }
    }
}