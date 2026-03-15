Feature: Create Product

Scenario: Successfully create a product
  Given the following product data
    | Name                | Description        | Amount | Currency |
    | Smartphone Pro Max  | Latest model 2026  | 1200.0 | USD      |
  When I create the product
  Then the product should be created
  And the created product details should be returned

Scenario Outline: Fail to create product due to validation errors
  Given the following product data
    | Name        | Description | Amount  | Currency |
    | <Name>      | <Description> | <Amount> | <Currency> |
  When I create the product
  Then a validation error should occur for "<Field>"

Examples:
| Name           | Description | Amount | Currency | Field    |
|                | Valid Desc  | 50.0   | EUR      | Name     |
| Product A      | Valid Desc  | -10.0  | GBP      | Amount   |
| Product A      | Valid Desc  | 0.0    | GBP      | Amount   |
| Product B      | Valid Desc  | 100.0  |          | Currency |
| Product B      | Valid Desc  | 100.0  | US       | Currency |
| Product B      | Valid Desc  | 100.0  | USAAA    | Currency |

Scenario: Fail to create product when currency violates business rule
  Given the following product data
    | Name      | Description     | Amount | Currency |
    | Product A | Valid product   | 100.0  | BRL      |
  When I create the product
  Then a business rule violation should occur with message "A draft product cannot be discontinued."
