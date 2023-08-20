Feature: Coupon Codes

A short summary of the feature

@Tests
Scenario: Buy an item of clothing using a coupon code as a registered user
	Given I have logged into my account
	And I add a hat to my basket
	When I try to apply the coupon code edgewords
	Then the total value should be correct
