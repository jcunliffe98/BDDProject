@Coupon
Feature: Coupon Codes

Check that coupons properly apply

@TestCase1 @Tests
Scenario Outline: Buy an item of clothing using a coupon code as a registered user
	Given I login to my account using 'jack.cunliffe@nfocus.co.uk' and password
	And I add a '<item>' to my basket
	When I try to apply the coupon code 'edgewords'
	Then the total value should be correct
	Examples:
	| item	|
	| hat	|
	| belt	|