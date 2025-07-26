using System;


using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;
using QuanLySach.Models;
namespace QuanLySach.Services
{
    public class BookService(string connectionString)
    {
        private readonly string _connectionString = connectionString;

        private IDbConnection Connection => new SqlConnection(_connectionString);

        public IEnumerable<Book> GetAll()// lấy tất cả sách
        {
            using var conn = Connection;
            return conn.Query<Book>("SELECT * FROM Books");
        }

        public Book? GetById(int id)// tìm kiếm sách theo id
        {
            using var conn = Connection;
            return conn.QuerySingleOrDefault<Book>("SELECT * FROM Books WHERE Id = @Id", new { Id = id });
        }

        public void Add(Book book)// thêm sách mới
        {
            using var conn = Connection;
            conn.Execute("INSERT INTO Books (Title, AuthorId) VALUES (@Title, @AuthorId)", book);
        }

        public void Update(Book book)// cập nhật thông tin sách
        {
            using var conn = Connection;
            conn.Execute("UPDATE Books SET Title = @Title, AuthorId = @AuthorId WHERE Id = @Id", new
            {
                book.Title,
                book.AuthorId,
                Id = book.Id
            });
        }

        public void Delete(int id)// xoa sách theo id
        {
            using var conn = Connection;
            conn.Execute("DELETE FROM Books WHERE Id = @Id", new { Id = id });
        }
    }

}
