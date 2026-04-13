using Domain;
using Microsoft.Data.SqlClient;
using S3_Lab5.Abstractions;
using System;
using System.Collections.Generic;

namespace S3_Lab5.Data
{
    public class SqlPlywacyRepository : IPlywacyRepository
    {
        public event Action<string> Sukces;
        private readonly string _connectionString = @"Server=(localdb)\MSSQLLocalDB;Database=S3_Lab5_DB;Trusted_Connection=True;TrustServerCertificate=True;";
        private void Powiadom(string komunikat) => Sukces?.Invoke(komunikat);

        public IEnumerable<Plywak> PobierzWszystkich()
        {
            var lista = new List<Plywak>();
            using var connection = new SqlConnection(_connectionString);
            var command = new SqlCommand("SELECT Id, ImieNazwisko, RokUrodzenia, NajlepszyCzas, CzyAktywnyZawodnik, IloscZlotychMedali FROM Plywacy", connection);

            connection.Open();
            using var reader = command.ExecuteReader();
           
            while (reader.Read())
            {
                lista.Add(new Plywak
                {
                    Id = reader.GetInt32(0),
                    ImieNazwisko = reader.GetString(1),
                    RokUrodzenia = reader.GetInt32(2),
                    NajlepszyCzas = reader.GetDouble(3),
                    CzyAktywnyZawodnik = reader.GetBoolean(4),
                    IloscZlotychMedali = reader.IsDBNull(5) ? null : reader.GetInt32(5)
                });
            }
            
            Powiadom("Połączono z bazą SQL i pobrano aktualną listę pływaków.");
            return lista;
        }

        public void Dodaj(Plywak p)
        {
            using var connection = new SqlConnection(_connectionString);
            
            var query = "INSERT INTO Plywacy (ImieNazwisko, RokUrodzenia, NajlepszyCzas, CzyAktywnyZawodnik, IloscZlotychMedali) " +
                        "VALUES (@imie, @rok, @czas, @aktywny, @medale)";
            
            var command = new SqlCommand(query, connection);
            
            command.Parameters.AddWithValue("@imie", p.ImieNazwisko);
            command.Parameters.AddWithValue("@rok", p.RokUrodzenia);
            command.Parameters.AddWithValue("@czas", p.NajlepszyCzas);
            command.Parameters.AddWithValue("@aktywny", p.CzyAktywnyZawodnik);
            command.Parameters.AddWithValue("@medale", (object)p.IloscZlotychMedali ?? DBNull.Value);

            connection.Open();
            command.ExecuteNonQuery();

            Powiadom($"Bd: Zapisano nowego zawodnika: {p.ImieNazwisko}");
        }

        public void Usun(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            var command = new SqlCommand("DELETE FROM Plywacy WHERE Id = @id", connection);
            
            command.Parameters.AddWithValue("@id", id);
            connection.Open();
            command.ExecuteNonQuery();

            Powiadom($"Bd: Usunięto zawodnika o ID {id}");
        }

        public Plywak PobierzPoId(int id) => throw new NotImplementedException();
        public void Edytuj(Plywak p)
        {
            using var connection = new SqlConnection(_connectionString);
            var query = "UPDATE Plywacy SET ImieNazwisko = @imie, RokUrodzenia = @rok, NajlepszyCzas = @czas, CzyAktywnyZawodnik = @aktywny, IloscZlotychMedali = @medale WHERE Id = @id";
            var command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@id", p.Id);
            command.Parameters.AddWithValue("@imie", p.ImieNazwisko);
            command.Parameters.AddWithValue("@rok", p.RokUrodzenia);
            command.Parameters.AddWithValue("@czas", p.NajlepszyCzas);
            command.Parameters.AddWithValue("@aktywny", p.CzyAktywnyZawodnik);
            command.Parameters.AddWithValue("@medale", (object)p.IloscZlotychMedali ?? DBNull.Value);

            connection.Open();
            command.ExecuteNonQuery();

            Powiadom($"Bd: Zaktualizowano dane zawodnika {p.ImieNazwisko}");
        }
    }
}