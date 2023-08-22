Feature: Coupon Codes

A short summary of the feature

@Coupon @TestCase1 @Tests

Scenario: Buy an item of clothing using a coupon code as a registered user
	Given I login to my account using 'jack.cunliffe@nfocus.co.uk' and password
	And I add a hat to my basket
	When I try to apply the coupon code 'edgewords'
	Then the total value should be correct
