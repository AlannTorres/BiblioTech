using LibrarySystem_2.Domain;

namespace LibrarySystem_2.Interfaces.Repositories
{
    public interface IBookRepository
    {
        Task<IEnumerable<Book>> ListAllBooksAsync();
        Task<Book> GetBookByIdAsync(int book_id);
        Task<int> InsertAsync(Book book);
        Task<int> UpdateAsync(Book book, int book_id);
        Task<int> DeleteAsync(int book_id);
        Task<int> UpdateBookQuantityAsync(int book_Id, int newQuantity);
        Task<int> GetAvailableBooksAsync(int book_Id);
        Task<int> GetBookQuantityAsync(int book_id);
    }
}
