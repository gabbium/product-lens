Feature: List Products

Scenario: Successfully list products
  Given products exist
  When I list products with page 1 and size 10
  Then the products should be returned

Scenario Outline: Fail to list products due to validation error
  When I list products with page <PageNumber> and size <PageSize>
  Then a validation error should occur for "<Field>"

Examples:
| PageNumber | PageSize | Field      |
| 0          | 10       | PageNumber |
| -1         | 10       | PageNumber |
| 1          | 0        | PageSize   |
| 1          | 101      | PageSize   |
