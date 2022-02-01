using Business.Abstract;
using Business.CrossCuttingConcerns.Translate;
using Business.Helpers;
using Core.Aspects.Autofac.Authorization;
using Core.Aspects.Autofac.Transaction;
using Core.Business;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;

namespace Business.Concrete
{
    public class WordManager : BusinessService, IWordService
    {
        private readonly IWordDal _wordDal;
        private readonly IWordTranslateService _wordTranslateService;
        private readonly IStudentService _studentService;
        private readonly FileReaderHelper _fileReaderHelper;
        private readonly Random _r = new Random();

        public WordManager(IWordDal wordDal, IWordTranslateService wordTranslateService, IStudentService studentService, FileReaderHelper fileReaderHelper)
        {
            _wordDal = wordDal;
            _wordTranslateService = wordTranslateService;
            _studentService = studentService;
            _fileReaderHelper = fileReaderHelper;
        }

        [TransactionScopeAspect]
        [LoginRequired]
        public IResult Add(Word entity)
        {
            var student = _studentService.GetStudent();
            entity.StudentId = student.UserId;

            IResult result = BusinessRules.Run(
                CheckIfStudentIsNull(student),
                CheckLangugeCode(entity.ResultWordCode),
                CheckIfWordAddedForStudentAdd(entity)
            );

            if (!result.Success)
                return result;

            entity.CreatedDate = DateTime.Now;
            entity.IsShowed = false;

            _wordDal.Add(entity);
            _studentService.UpdatePoint(2);
            return new SuccessResult();
        }

        public IResult Delete(DeleteModel entity)
        {
            IResult result = BusinessRules.Run();

            if (!result.Success)
                return result;

            Word entityToDelete = GetById(entity.UUID).Data;
            _wordDal.Delete(entityToDelete);
            return new SuccessResult();
        }

        public IResult Update(Word entity)
        {
            IResult result = BusinessRules.Run();

            if (!result.Success)
                return result;

            _wordDal.Update(entity);
            return new SuccessResult();
        }

        public IDataResult<List<Word>> GetAll()
        {
            return new SuccessDataResult<List<Word>>(_wordDal.GetAll());
        }

        public IDataResult<Word> GetById(Guid id)
        {
            return new SuccessDataResult<Word>(_wordDal.Get(e => e.WordId == id));
        }

        public IDataResult<Word> GetByStudentIdAndSearchedWord(int studentId, string searchedWord)
        {
            return new SuccessDataResult<Word>(_wordDal.Get(w => w.StudentId == studentId && w.SearchedWord == searchedWord));
        }

        public IDataResult<List<Word>> GetByStudentId(int studentId)
        {
            return new SuccessDataResult<List<Word>>(_wordDal.GetAll(w => w.StudentId == studentId));
        }

        public IDataResult<List<Word>> GetByStudentIdAndSearchedLanguageCode(int studentId, string code)
        {
            return new SuccessDataResult<List<Word>>(_wordDal.GetAll(w => w.StudentId == studentId && w.SearchedWordCode == code));
        }

        public IDataResult<List<Word>> GetByStudentIdAndSearchedLanguageCodeAndIsShowed(int studentId, string code, bool isShowed)
        {
            return new SuccessDataResult<List<Word>>(_wordDal.GetAll(w => w.StudentId == studentId && w.SearchedWordCode == code && w.IsShowed == isShowed));
        }

        public IDataResult<List<Word>> GetByStudentIdAndResultLanguageCode(int studentId, string code)
        {
            return new SuccessDataResult<List<Word>>(_wordDal.GetAll(w => w.StudentId == studentId && w.ResultWordCode == code));
        }


        [LoginRequired]
        public IDataResult<Word> QueryTheWord(WordRequestDto wordRequestDto)
        {
            var student = _studentService.GetStudent();
            var word = new Word()
            {
                StudentId = student.UserId,
                SearchedWord = wordRequestDto.Word,
            };

            var newWord = CheckIfWordAddedForStudent(word).Data;
            if (newWord != null)
                return new SuccessDataResult<Word>(newWord);

            var detectedLanguage = _wordTranslateService.Detect(wordRequestDto.Word);
            var translatedText = _wordTranslateService.Translate(wordRequestDto.Word, wordRequestDto.TargetCode, detectedLanguage);

            word.SearchedWordCode = detectedLanguage;
            word.ResultWord = translatedText; word.ResultWordCode = wordRequestDto.TargetCode;

            var result = Add(word);
            if (!result.Success)
                return new ErrorDataResult<Word>(result.Message);

            return new SuccessDataResult<Word>(word, "Success");
        }

        [LoginRequired]
        public IDataResult<NewWordDto> GetNewWord()
        {
            var student = _studentService.GetStudent();
            var data = _fileReaderHelper.Read();
            int index = _r.Next(0, data.Count);
            var gettedWord = new NewWordDto() { Word = data[index] };

            var words = GetByStudentId(student.UserId).Data;

            foreach (var word in words)
            {
                if (word.SearchedWord != gettedWord.Word)
                {
                    gettedWord.Word = data[index];
                    gettedWord.Result = _wordTranslateService.Translate(gettedWord.Word, "tr", "en");
                    break;
                }

                index = _r.Next(0, data.Count);
                gettedWord = new NewWordDto() { Word = data[index] };
            }
            return new SuccessDataResult<NewWordDto>(gettedWord);
        }

        [TransactionScopeAspect]
        [LoginRequired]
        public IDataResult<WordForExam> GetWordFromPool()
        {
            var student = _studentService.GetStudent();
            var words = GetByStudentIdAndResultLanguageCode(student.UserId, "tr").Data;
            var wordsForKeyword = GetByStudentIdAndSearchedLanguageCodeAndIsShowed(student.UserId, "en", false).Data;
            if (wordsForKeyword.Count == 0)
            {
                return new SuccessDataResult<WordForExam>("Tebrikler bütün soruları yanıtladınız");
            }

            var word = wordsForKeyword[_r.Next(0, wordsForKeyword.Count)];

            var rand1 = _r.Next(0, words.Count);
            var rand2 = _r.Next(0, words.Count);
            var rand3 = _r.Next(0, words.Count);

            while (
                words[rand1].WordId == word.WordId ||
                words[rand2].WordId == word.WordId ||
                words[rand3].WordId == word.WordId ||
                words[rand1].WordId == words[rand2].WordId ||
                words[rand1].WordId == words[rand3].WordId ||
                words[rand2].WordId == words[rand1].WordId ||
                words[rand2].WordId == words[rand3].WordId ||
                words[rand3].WordId == words[rand1].WordId ||
                words[rand3].WordId == words[rand2].WordId
            )
            {
                rand1 = _r.Next(0, words.Count);
                rand2 = _r.Next(0, words.Count);
                rand3 = _r.Next(0, words.Count);
            }

            var option1 = words[rand1];
            var option2 = words[rand2];
            var option3 = words[rand3];


            var randwords = new List<Word>();
            randwords.Add(option1);
            randwords.Add(option2);
            randwords.Add(option3);

            randwords[_r.Next(0, randwords.Count)] = word;

            var wordForExam = new WordForExam()
            {
                Word = word.SearchedWord,
                Option1 = randwords[0].ResultWord,
                Option2 = randwords[1].ResultWord,
                Option3 = randwords[2].ResultWord,
            };

            return new SuccessDataResult<WordForExam>(wordForExam);
        }

        [LoginRequired]
        public IDataResult<CheckWordTranslateDto> CheckWordTranslate(CheckWordTranslateRequestDto checkWord)
        {
            var student = _studentService.GetStudent();
            var word = GetByStudentIdAndSearchedWord(student.UserId, checkWord.Word).Data;

            word.IsShowed = true;
            var operation = Update(word);
            if (!operation.Success)
                return new ErrorDataResult<CheckWordTranslateDto>(operation.Message);


            if (word == null)
                return new ErrorDataResult<CheckWordTranslateDto>("Word was null");

            if (word.ResultWord?.ToLower() == checkWord.Option?.ToLower())
            {
                _studentService.UpdatePoint(3);
                return new SuccessDataResult<CheckWordTranslateDto>(
                    new CheckWordTranslateDto()
                    {
                        CorrectWord = "",
                        IsSuccess = true
                    });
            }

            return new ErrorDataResult<CheckWordTranslateDto>(
                new CheckWordTranslateDto() { CorrectWord = word.ResultWord, IsSuccess = false }
                );
        }

        [LoginRequired]
        public IDataResult<Word> GetWordWithTypo()
        {
            var student = _studentService.GetStudent();
            var words = GetByStudentIdAndSearchedLanguageCode(student.UserId, "en").Data;
            if (words.Count == 0)
                return new SuccessDataResult<Word>("Önce Kelime Öğrenmeniz Gerekmektedir.");

            var word = words[_r.Next(0, words.Count)];
            var newWord = "";
            var randomNumber = new Random().Next(0, word.SearchedWord.Length);
            var counter = 0;
            var controller = 0;

            foreach (var item in word.SearchedWord)
            {
                if (counter != randomNumber)
                    newWord += item;

                if (controller < 1 && item != ' ')
                {
                    var x = word.SearchedWord[randomNumber];
                    newWord += x;
                    controller++;
                }
                counter++;
            }

            word.SearchedWord = newWord;

            return new SuccessDataResult<Word>(word);
        }

        [LoginRequired]
        public IDataResult<CheckWordTranslateDto> VerifyWord(VerifyTheWordRequestDto word)
        {
            var oldWord = GetById(word.Word.WordId).Data;

            if (oldWord.SearchedWord == word.NewWord)
            {
                _studentService.UpdatePoint(3);
                return new SuccessDataResult<CheckWordTranslateDto>(new CheckWordTranslateDto() { CorrectWord = "", IsSuccess = true });
            }

            return new ErrorDataResult<CheckWordTranslateDto>(new CheckWordTranslateDto() { CorrectWord = oldWord.SearchedWord, IsSuccess = false });
        }

        public IDataResult<List<string>> GetAllTranslateLanguages()
        {
            return new SuccessDataResult<List<string>>(_wordTranslateService.GetLanguages(), "");
        }

        private IResult CheckIfStudentIsNull(Student student)
        {
            if (student == null || student.UserId == 0)
                return new ErrorResult("Student was null");

            return new SuccessResult();
        }

        private IResult CheckLangugeCode(string code)
        {
            if (code != "en" && code != "tr")
                return new ErrorResult("Code was false");

            return new SuccessResult();
        }

        private IDataResult<Word> CheckIfWordAddedForStudent(Word word)
        {
            var result = GetByStudentIdAndSearchedWord(word.StudentId, word.SearchedWord).Data;

            if (result != null)
                return new SuccessDataResult<Word>(result);

            return new SuccessDataResult<Word>();
        }
        private IDataResult<Word> CheckIfWordAddedForStudentAdd(Word word)
        {
            var result = GetByStudentIdAndSearchedWord(word.StudentId, word.SearchedWord).Data;

            if (result != null)
                return new ErrorDataResult<Word>("Bu kelimeyi zaten öğrendiniz");

            return new SuccessDataResult<Word>();
        }
    }
}
