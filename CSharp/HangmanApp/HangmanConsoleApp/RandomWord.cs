namespace Hangman1
{
    public class RandomWord
    {
        public List<string> WordList { get; set; } = new List<string>();

        public RandomWord() { }

        public void FillList(string wordToAdd)
        {
            WordList.Add(wordToAdd);
        }

        public string GetWord()
        {
            Random random = new Random();
            int randomNum = random.Next(WordList.Count);
            return WordList[randomNum];
        }
    }
}
