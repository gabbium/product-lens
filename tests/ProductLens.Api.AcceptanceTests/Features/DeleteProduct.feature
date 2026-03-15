Feature: Delete Product

Scenario: Successfully delete a draft product
  Given a product exists
  And the product status is "Draft"
  When I delete the product
  Then the product should be deleted

Scenario: Deleting a non existing product should succeed
  Given a non existing product id
  When I delete the product
  Then the product should be deleted

Scenario: Fail to delete product due to validation error
  Given an empty product id
  When I delete the product
  Then a validation error should occur for "ProductId"

Scenario: Fail to delete an active product
  Given a product exists
  And the product status is "Active"
  When I delete the product
  Then a business rule violation should occur with message "Only draft products can be deleted."

Scenario: Fail to delete a discontinued product
  Given a product exists
  And the product status is "Discontinued"
  When I delete the product
  Then a business rule violation should occur with message "Only draft products can be deleted."
