Feature: Activate Product

Scenario: Successfully activate a product
  Given a product exists
  And the product status is "Draft"
  When I activate the product
  Then the product should be activated

Scenario: Fail to activate product due to validation error
  Given an empty product id
  When I activate the product
  Then a validation error should occur for "ProductId"

Scenario: Fail to activate product when product does not exist
  Given a non existing product id
  When I activate the product
  Then a not found error should occur with message "Product was not found."

Scenario: Fail to activate a discontinued product
  Given a product exists
  And the product status is "Discontinued"
  When I activate the product
  Then a business rule violation should occur with message "Activation is not allowed for a discontinued product."
