using System;

namespace QuanLySach.Services;

using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;
using Models;

public class AuthorService(string connectionString)
{
    private readonly string _connectionString = connectionString;

    private IDbConnection Connection => new SqlConnection(_connectionString);

    public IEnumerable<Author> GetAll()//lấy tất cả tác giả
    {
        using var conn = Connection;
        return conn.Query<Author>("SELECT * FROM Authors");
    }

    public Author? GetById(int id)//tìm kiếm tác giả theo id
    {
        using var conn = Connection;
        return conn.QuerySingleOrDefault<Author>("SELECT * FROM Authors WHERE Id = @Id", new { Id = id });
    }

    public void Add(Author author)//thêm tác giả mới
    {
        using var conn = Connection;
        conn.Execute("INSERT INTO Authors (Name) VALUES (@Name)", author);
    }

    public void Update(Author author)//cập nhật thông tin tác giả
    {
        using var conn = Connection;
        conn.Execute("UPDATE Authors SET Name = @Name WHERE Id = @Id", new { author.Name, Id = author.Id });
    }

    public void Delete(int id)//xóa tác giả theo id
    {
        using var conn = Connection;
        conn.Execute("DELETE FROM Authors WHERE Id = @Id", new { Id = id });
    }
    public IEnumerable<Book> GetBooksByAuthorId(int authorId)//lấy sách theo id tác giả
    {
        using var conn = Connection;
        return conn.Query<Book>("SELECT * FROM Books WHERE AuthorId = @AuthorId", new { AuthorId = authorId });
    }
}

