using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PresentationModels = CalorieCounter.Models;
using DataAccessModels = DataLibrary.Models;
using DataLibrary.BusinessLogic;
using CalorieCounter.Models.ViewModels;

namespace CalorieCounter.Controllers
{
    public class MotivationController : Controller
    {
        private readonly IDatabaseData _db;

        public MotivationController(IDatabaseData db)
        {
            _db = db; 
        }
        // GET: MotivationController
        public IActionResult Index()
        {
            // get all quotes List<Quote>
            List<DataAccessModels.QuoteModel> quotesData = _db.GetAllQuotes();

            List<PresentationModels.QuoteModel> quotes = new List<PresentationModels.QuoteModel>(); 
            foreach(DataAccessModels.QuoteModel quote in quotesData)
            {
                PresentationModels.QuoteModel quoteModel = new PresentationModels.QuoteModel()
                {
                    Quote = quote.Quote,
                    Author = quote.Author
                }; 
                quotes.Add(quoteModel); 
            }
            // get a random quote from the list 
            Random random = new Random(); 

            int randomIndex = random.Next(0, quotes.Count); 

            PresentationModels.QuoteModel randomQuote = quotes[randomIndex]; 

            // get all challenges List<Challenge>
            List<DataAccessModels.ChallengeModel> challengesData = _db.GetAllChallenges();

            List<PresentationModels.ChallengeModel> challenges = new List<PresentationModels.ChallengeModel>(); 
            foreach(DataAccessModels.ChallengeModel challenge in challengesData)
            {
                PresentationModels.ChallengeModel challengeModel = new PresentationModels.ChallengeModel()
                {
                    Challenge = challenge.Challenge,
                    Difficulty = challenge.Difficulty
                }; 
                challenges.Add(challengeModel); 
            }

            // get a random challenge 
            randomIndex = random.Next(0, challenges.Count); 

            PresentationModels.ChallengeModel randomChallenge = challenges[randomIndex];

            // add them to a viewmodel
            MotivationViewModel motivationViewModel = new MotivationViewModel()
            {
                Quote = randomQuote,
                Challenge = randomChallenge
            }; 

            return View(motivationViewModel); 
        }

    }
}
