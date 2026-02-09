# Accepting payments with Blazor, ASP.NET Core Minimal APIs, and Stripe Checkout

The sample was ported from [Charles Watkins'](https://github.com/charlesw-stripe) demo from Vue Nation 2022 on [Getting started with payments in Vue 3](https://github.com/charlesw-stripe/vuejs-nation). Why do JavaScript folks have to have all the fun!?!

There's a lot of work that goes into accepting online payments. It can be hard to create secure and compliant payment forms that are accessible to your global customer base. With [Stripe checkout](https://stripe.com/payments/checkout), you get a secure, Stripe-hosted payment page that lets you collect payments quickly across devices with support for a growing number of [countries](https://stripe.com/global).

## What's in the box
This sample is split into two applications
* [SimpleCheckoutServer](src/SimpleCheckoutServer) - ASP.NET Core Minimal API backend that hosts 2 endpoints for the products and checkout work redirect.
* [SimpleCheckoutBlazor](src/SimpleCheckoutBlazor) - Frontend UI built with Blazor and Tailwind CSS.

## Requirements
* .NET SDK 6.0+ 
* Node JS 14+
* A FREE [Stripe Account](https://dashboard.stripe.com/register)

## Running the demo
### Obtain your Stripe Secret Key 🕵🏽‍♂️
Before running the code, you'll need to retrieve your Stripe Secret Key from your account dashboard.
* Log in to your [Stripe Dashboard](https://dashboard.stripe.com/)
* Make sure you're in test mode. The toggle is located at the top right corner of the page.
* Click on the `Developers` button, then select `API Keys` in the left menu
* Under `Standard Keys`, reveal and copy your `Secret key`.

> You can learn more about API Keys and Modes at this link => https://stripe.com/docs/keys
* Add your key to the `appsettings.json` configuration file in the [SimpleCheckoutServer](src/SimpleCheckoutServer) project.
```json
  "Stripe": {
    "STRIPE_SECRET_KEY": "PASTE YOUR KEY HERE"
  }
```

### Run the code 👨🏽‍💻
Navigate into the src/ directory
```shell
cd  src/
```

Run the build. This will restore both the node and .NET packages.
```shell
dotnet build
```
Run the project
```shell
dotnet run --project SimpleCheckoutServer
```

By default, the application should start running on http://localhost:4242

