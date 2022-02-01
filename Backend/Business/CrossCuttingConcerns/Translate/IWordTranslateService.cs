using System.Collections.Generic;

namespace Business.CrossCuttingConcerns.Translate
{
    public interface IWordTranslateService 
    {
        string Detect(string word);
        string Translate(string word, string target, string source);
        List<string> GetLanguages();
    }
}
