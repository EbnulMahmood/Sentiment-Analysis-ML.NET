using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.ML;
using static SentimentAnalysis_Web.SentimentAnalysis;

namespace SentimentAnalysis.Web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly PredictionEnginePool<ModelInput, ModelOutput> _predictionEnginePool;

        public IndexModel(ILogger<IndexModel> logger, PredictionEnginePool<ModelInput, ModelOutput> predictionEnginePool)
        {
            _logger = logger;
            _predictionEnginePool = predictionEnginePool;
        }

        public void OnGet()
        {

        }

        public IActionResult OnGetAnalyzeSentiment([FromQuery] string text)
        {
            try
            {
                if (string.IsNullOrEmpty(text)) return Content("Neutral");

                var input = new ModelInput { Col0 = text };

                var prediction = _predictionEnginePool.Predict(input);

                var sentiment = Convert.ToBoolean(prediction.PredictedLabel) ? "Not Toxic" : "Toxic";

                return Content(sentiment);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}