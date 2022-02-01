using Core.Business;
using Core.Utilities.Results.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;

namespace Business.Abstract
{
    public interface IWordService : IServiceRepository<Word, Guid>
    {
        IDataResult<Word> GetByStudentIdAndSearchedWord(int studentId, string searchedWord);
        IDataResult<List<Word>> GetByStudentId(int studentId);
        IDataResult<List<Word>> GetByStudentIdAndSearchedLanguageCode(int studentId, string code);
        IDataResult<List<Word>> GetByStudentIdAndSearchedLanguageCodeAndIsShowed(int studentId, string code, bool isShowed);
        IDataResult<List<Word>> GetByStudentIdAndResultLanguageCode(int studentId, string code);
        IDataResult<Word> QueryTheWord(WordRequestDto wordRequestDto);
        IDataResult<NewWordDto> GetNewWord();
        IDataResult<WordForExam> GetWordFromPool();
        IDataResult<CheckWordTranslateDto> CheckWordTranslate(CheckWordTranslateRequestDto checkWord);
        IDataResult<Word> GetWordWithTypo();
        IDataResult<CheckWordTranslateDto> VerifyWord(VerifyTheWordRequestDto verifyTheWord);
        IDataResult<List<string>> GetAllTranslateLanguages();
    }
}