using System;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using System.Text.Json;


namespace StockMarketDataGenerator
{
    public class DataGenerator
    {
        private readonly ILogger _logger;
        private readonly HttpClient _client;

        public DataGenerator(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<DataGenerator>();
            _client = new HttpClient();
        }

        [Function("DataGenerator")]
        public async Task Run([TimerTrigger("*/5 * * * * *")] TimerInfo myTimer)
        {
            var random = new Random();

            var symbolList = new List<string> { "MSFT", "AMZN", "AAPL", "NVDA" };
            var index = random.Next(symbolList.Count); // Generate a random index
            var randomSymbol = symbolList[index]; // Pick the symbol at the random index

            var stockPrice = 375 + random.Next(-50, 51);  // Price between 325 and 425

            var stockData = new
            {
                datetime = DateTime.UtcNow.ToString("o"),
                symbol = randomSymbol,
                price = stockPrice
            };

            var stockJson = JsonSerializer.Serialize(stockData);
            _logger.LogInformation($"Generated Stock Data: {stockJson}");

            var stockTradingBotUrl = Environment.GetEnvironmentVariable("StockTradingBot.Url");
            var response = await _client.PostAsync(stockTradingBotUrl, new StringContent(stockJson, System.Text.Encoding.UTF8, "application/json"));
            var responseBody = await response.Content.ReadAsStringAsync();
            _logger.LogInformation($"Stock Trading Bot response: {response.StatusCode} - {responseBody}");
        }
    }
}
