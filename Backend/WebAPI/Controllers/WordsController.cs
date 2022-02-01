using Business.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.AspNetCore.Mvc;
using System;

namespace WebAPI.Controllers
{
    public class WordsController : ControllerRepository<Word, Guid>
    {
        private readonly IWordService _wordService;

        public WordsController(IWordService wordService) : base(wordService)
        {
            _wordService = wordService;
        }

        [HttpPost("[action]")]
        public IActionResult QueryTheWord(WordRequestDto wordRequestDto)
        {
            var result = _wordService.QueryTheWord(wordRequestDto);
            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpGet("[action]")]
        public IActionResult GetNewWord()
        {
            var result = _wordService.GetNewWord();
            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpGet("[action]")]
        public IActionResult GetWordFromPool()
        {
            var result = _wordService.GetWordFromPool();
            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpPost("[action]")]
        public IActionResult CheckWordTranslate(CheckWordTranslateRequestDto checkWord)
        {
            var result = _wordService.CheckWordTranslate(checkWord);
            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }


        [HttpGet("[action]")]
        public IActionResult GetWordWithTypo()
        {
            var result = _wordService.GetWordWithTypo();
            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpPost("[action]")]
        public IActionResult VerifyWord(VerifyTheWordRequestDto verifyTheWord)
        {
            var result = _wordService.VerifyWord(verifyTheWord);
            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpGet("[action]")]
        public IActionResult GetAllTranslateLanguages()
        {
            var result = _wordService.GetAllTranslateLanguages();
            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }
    }
}
