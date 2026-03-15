Feature: Update Product

Scenario: Successfully update a product
  Given a product exists
  And the product status is "Draft"
  When I update the product details to "Updated Product" "Updated description"
  Then the product should be updated

Scenario: Fail to update product due to validation error
  Given a product exists
  When I update the product details to "<Name>" "<Description>"
  Then a validation error should occur for "<Field>"

Examples:
| Name | Description | Field |
|      | Valid Desc  | Name  |

Scenario: Fail to update product due to validation error on id
  Given an empty product id
  When I update the product details to "Product A" "Some description"
  Then a validation error should occur for "ProductId"

Scenario: Fail to update product when product does not exist
  Given a non existing product id
  When I update the product details to "Product A" "Some description"
  Then a not found error should occur with message "Product was not found."

Scenario: Fail to update discontinued product
  Given a product exists
  And the product status is "Discontinued"
  When I update the product details to "Updated Product" "Updated description"
  Then a business rule violation should occur with message "Modification is not allowed for a discontinued product."
