# Data Generator
Using Azure Functions, create a simple program which can generate test stock market data.
The data generator should produce JSON messages containing the stock symbol, price and timestamp at regular intervals (e.g., every 5 seconds).

## Note
The generated prices need to be random with a big enough range to trigger the Logic App Buy and Sell commands. i.e. `375 ± 50`.

```JSON
// Example JSON message
{
  "datetime": "2023-11-20T00:00:00.000Z"
  "symbol": "MSFT",
  "price": 377.44
}
```
