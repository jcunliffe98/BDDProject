@Cart
Feature: Cart2

Background: 
	Given I have logged in to my account using 'jack.cunliffe@nfocus.co.uk' and password
	And I add a 'Belt' to my cart

@TestCase1 @Tests
Scenario: Apply a valid coupon code to a cart as a registered user
	When I try to apply the coupon code 'edgewords'
	Then the coupon should apply a '15%' discount

@TestCase2 @Tests
Scenario: Place an order as a registered user
	When I input my billing details
	| First Name | Surname  | Street           | City   | Postcode | Telephone   |
	| Jack       | Cunliffe | 24 London Street | London | SW1A 0AA | 07700900000 |
	And I place the order
	Then the order is confirmed
