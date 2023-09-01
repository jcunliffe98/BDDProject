@Cart
Feature: Cart

Background: 
	Given I have logged in to my account using 'jack.cunliffe@nfocus.co.uk' and password
	And I add a 'hat' to my cart

@TestCase1 @Tests
Scenario: Apply a valid coupon code to a cart as a registered user
	When I try to apply the coupon code 'edgewords'
	Then the coupon should apply a '15%' discount

@TestCase2 @Tests
Scenario: Place an order as a registered user
	When I input 'Jack, Cunliffe,24 London Street,London,SW1A 0AA,020 7219 4272' as my address
	And I place the order
	Then the order is confirmed
