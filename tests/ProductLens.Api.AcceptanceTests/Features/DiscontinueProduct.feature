Feature: Discontinue Product

Scenario: Successfully discontinue a product
  Given a product exists
  And the product status is "Active"
  When I discontinue the product
  Then the product should be discontinued

Scenario: Fail to discontinue product due to validation error
  Given an empty product id
  When I discontinue the product
  Then a validation error should occur for "ProductId"

Scenario: Fail to discontinue product when product does not exist
  Given a non existing product id
  When I discontinue the product
  Then a not found error should occur with message "Product was not found."

Scenario: Fail to discontinue a draft product
  Given a product exists
  And the product status is "Draft"
  When I discontinue the product
  Then a business rule violation should occur with message "A draft product cannot be discontinued."
