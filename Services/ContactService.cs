using Dapper;
using RecipeManagementApp.Data;
using RecipeManagementApp.Interfaces;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;

namespace RecipeManagementApp.Services
{
    public class ContactService : IContactService
    {
        private readonly string _connectionString;

        public ContactService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public bool SaveMessage(Contacts contact)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var sql = @"INSERT INTO Contacts (Name, Email, Subject, Message, SubmittedAt) 
                            VALUES (@Name, @Email, @Subject, @Message, @SubmittedAt)";
                var rowsAffected = db.Execute(sql, contact);
                return rowsAffected > 0;
            }
        }

        public IEnumerable<Contacts> GetAllMessages()
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var sql = "SELECT * FROM Contacts ORDER BY SubmittedAt DESC";
                return db.Query<Contacts>(sql);
            }
        }

        public Contacts GetMessageById(int id)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var sql = "SELECT * FROM Contacts WHERE Id = @Id";
                return db.QueryFirstOrDefault<Contacts>(sql, new { Id = id });
            }
        }
    }
}
