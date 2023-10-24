﻿using ProjectChallengeData.Database.Repositories.IRepositories;
using ProjectChallengeDomain.IService;
using ProjectChallengeDomain.Models;
using ProjectChallengeDomain.Models.Requests;
using System.Linq;

namespace ProjectChallengeService.Services
{
    public class BookService : IBookService
    {
        private static IUnitOfWork _unitOfWork;


        public BookService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Delete(Guid id)
        {
            var book = await _unitOfWork.BookRepository.GetById(id);
            if (book is null)
                return false;

            _unitOfWork.BookRepository.Remove(book);
            _unitOfWork.Commit();

            return true;
        }

        public async Task<List<Book>> Get()
        {
            var books = await _unitOfWork.BookRepository.GetAll();
            return books.ToList();
        }

        public async Task<Book> GetById(Guid id)
        {
            return await _unitOfWork.BookRepository.GetById(id);    
        }

        public Book Post(BookRequestPost request)
        {
            Book book = new Book();
            book.BookName = request.BookName;
            book.Year = request.Year;
            book.Shelf = request.Shelf;
            book.Author = request.Author;
            _unitOfWork.BookRepository.Add(book);
            _unitOfWork.Commit();
            return book;
        }

        public async Task<Book> Put(BookRequestPut request)
        {
            var book = await _unitOfWork.BookRepository.GetById(request.Id);
            if (book is null)
                return null;

            book.UpdateDate = DateTime.UtcNow.AddHours(-3);
            book.Shelf = request.Shelf;
            book.BookName = request.BookName;
            book.Year = request.Year;
            _unitOfWork.BookRepository.Update(book);
            _unitOfWork.Commit();
            return book;
        }
    }
}