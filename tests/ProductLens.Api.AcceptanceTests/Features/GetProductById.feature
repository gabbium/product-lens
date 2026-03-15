Feature: Get Product By Id

Scenario: Successfully get a product by id
  Given a product exists
  When I get the product by id
  Then the product details should be returned
  
Scenario: Fail to get product due to validation error
  Given an empty product id
  When I get the product by id
  Then a validation error should occur for "ProductId"

Scenario: Fail to get product when product does not exist
  Given a non existing product id
  When I get the product by id
  Then a not found error should occur with message "Product was not found."
