# Shopping Basket Pricing Calculator

A C# command-line application that totals a shopping basket and applies configured multi-buy offers. This project is based on a shopping basket pricing programming challenge and is maintained here as a learning project; it is not presented as wholly original work.

## Requirements

- .NET 10 SDK

## Run

From the repository root:

```sh
dotnet run --project src/PricingCalculator -- Melon Melon Melon
```

The example prints a £1.50 subtotal, a 50p melon discount, and a £1.00 total. The program exits after printing and returns a nonzero exit code when input is missing or a product name is not supported.

## Current catalog and offers

The catalog supports apples (35p), bananas (20p), melons (50p), and limes (15p). Product names can be singular or plural. Configured offers are buy one melon, get one free, and three limes for the price of two.

The challenge description in the inherited README listed different products and offers. This implementation and its tests use the catalog and offers above.

## Design

- `ProductFactory` resolves names to product records.
- `Utilities` groups repeated items and builds the configured discount list.
- `ShoppingBasket` calculates the subtotal and delegates offer calculations.
- `IDiscount` provides a shared contract for percentage, half-price, and multi-buy rules.

Prices use `decimal`. Product and offer configuration is currently hard-coded and could later move to a configuration file or database.

## Tests

Run the unit tests with:

```sh
dotnet test
```

The MSTest suite covers basket totals, discount calculations, supported product name variants, and qualifying and non-qualifying quantities. Moq is used to isolate discount behavior in basket tests.

## Project layout

```text
src/
  PricingCalculator/           Console application and pricing logic
  PricingCalculator.UnitTests/ MSTest unit tests
PricingCalculator.sln
```


