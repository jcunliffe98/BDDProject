Feature: Place Order

A short summary of the feature

@PlaceOrder @TestCase2 @Tests
Scenario: Place an order as a registered user
	Given I have logged in to my account using 'jack.cunliffe@nfocus.co.uk' and 'Mu3Wbu!AstG!!6Z'
	And I add a hat to my cart
	When I input my address
	And I place the order
	Then the order is placed
