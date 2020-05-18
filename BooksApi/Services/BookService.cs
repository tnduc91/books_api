using BooksApi.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace BooksApi.Services
{
    public class BookService
    {
        private readonly IMongoCollection<Book> _books;
        
        public BookService(IBooksDataSettings settings)
        {
        
            var client = new MongoClient(settings.ConnectionString);
            var db = client.GetDatabase(settings.DatabaseName);

            _books = db.GetCollection<Book>(settings.BooksCollectionName);
        }

        public List<Book> Get() => 
            _books.Find<Book>(b => true).ToList();

        public Book Get(string id) =>
            _books.Find<Book>(b => b.Id == id).FirstOrDefault();

        public Book Create(Book b)
        {
            _books.InsertOne(b);
            return b;
        }

        public void Update(string id, Book book)
        {
            _books.ReplaceOne(b => b.Id == id, book);
        }

        public void Remove(Book book)
        {
            _books.DeleteOne(b => b.Id == book.Id);
        }

        public void Remove(string id)
        {
            _books.DeleteOne(b => b.Id == id);
        }
    }
}