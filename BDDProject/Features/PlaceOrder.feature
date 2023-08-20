Feature: Place Order

A short summary of the feature

@Tests
Scenario: Place an order as a registered user
	Given I have logged into my account
	And I add a hat to my cart
	When I input my address
	And I place the order
	Then the order is placed
