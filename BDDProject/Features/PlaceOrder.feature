Feature: Place Order

Check that orders can be placed

@PlaceOrder @TestCase2 @Tests
Scenario Outline: Place an order as a registered user
	Given I have logged in to my account using 'jack.cunliffe@nfocus.co.uk' and password
	And I add a '<item>' to my cart
	When I input my address
	And I place the order
	Then the order is confirmed
	Examples:
	| item	|
	| hat	|
	| belt	|
