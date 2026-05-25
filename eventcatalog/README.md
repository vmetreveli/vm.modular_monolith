# EventCatalog — VM Modular Monolith

This directory contains the [EventCatalog](https://www.eventcatalog.dev/) documentation for the modular monolith's event-driven architecture.

## Prerequisites

- **Node.js >= 22.12.0** (required by Astro, EventCatalog's underlying framework)
  - Use [nvm-windows](https://github.com/coreybutler/nvm-windows) or [fnm](https://github.com/Schniz/fnm) to manage Node versions
  - Run `nvm install 22` and `nvm use 22`, or `fnm use` (reads `.nvmrc`)

## Getting Started

```bash
# Install dependencies
cd eventcatalog
npm install

# Start the dev server (http://localhost:4321)
npm run dev

# Build for production
npm run build

# Preview the production build
npm run preview
```

## Structure

```
eventcatalog/
├── eventcatalog.config.js    # Main configuration
├── domains/
│   └── ECommerce/            # E-Commerce domain (groups all modules)
├── services/
│   ├── CatalogModule/        # Product management module
│   ├── BasketModule/         # Shopping cart module
│   └── OrderingModule/       # Order processing module
├── events/
│   ├── ProductCreatedEvent/              # Domain: product created
│   ├── ProductPriceChangedEvent/         # Domain: product price changed
│   ├── ProductPriceChangedIntegrationEvent/  # Integration: Catalog → Basket
│   ├── BasketCheckoutIntegrationEvent/       # Integration: Basket → Ordering
│   └── OrderCreatedEvent/                # Domain: order created
└── teams/
    └── vm-team/              # Team ownership
```

## Event Flows

### 1. Price Synchronization (Catalog → Basket)
```
Product.Update() → ProductPriceChangedEvent (domain)
  → ProductPriceChangedEventHandler
    → ProductPriceChangedIntegrationEvent (via RabbitMQ/MassTransit)
      → ProductPriceChangedIntegrationEventHandler (Basket)
        → UpdateItemPriceInBasketCommand
```

### 2. Checkout → Order Creation (Basket → Ordering)
```
CheckoutBasketEndpoint → CheckoutBasketCommand
  → BasketCheckoutIntegrationEvent (via Outbox + RabbitMQ)
    → BasketCheckoutIntegrationEventHandler (Ordering)
      → CreateOrderCommand
```

## Adding New Events

1. Create a new folder under `events/` with the event name
2. Add `index.md` with frontmatter (`id`, `name`, `version`, `producers`, `consumers`)
3. Add `schema.json` with the JSON Schema for the event payload
4. Update the relevant service's `sends`/`receives` in its frontmatter
5. Run `npm run dev` to verify

## Upgrading

When upgrading Node.js to ≥ 22, you can also upgrade to EventCatalog v3:
```bash
npm install @eventcatalog/core@latest
```

