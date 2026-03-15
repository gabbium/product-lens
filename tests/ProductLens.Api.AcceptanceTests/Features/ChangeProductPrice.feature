Feature: Change Product Price

Scenario: Successfully change product price
  Given a product exists
  And the product status is "Draft"
  When I change the product price to 200.0 "USD"
  Then the product price should be changed

Scenario: Fail to change price due to validation error
  Given a product exists
  When I change the product price to <Amount> "<Currency>"
  Then a validation error should occur for "<Field>"

Examples:
| Amount | Currency | Field    |
| -10.0  | USD      | Amount   |
| 0.0    | USD      | Amount   |
| 10.0   |          | Currency |
| 10.0   | US       | Currency |
| 10.0   | USAAAA   | Currency |

Scenario: Fail to change price when product does not exist
  Given a non existing product id
  When I change the product price to 100.0 "USD"
  Then a not found error should occur with message "Product was not found."

Scenario: Fail to change price of discontinued product
  Given a product exists
  And the product status is "Discontinued"
  When I change the product price to 100.0 "USD"
  Then a business rule violation should occur with message "Modification is not allowed for a discontinued product."
