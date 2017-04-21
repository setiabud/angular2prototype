@specflow
Feature: Navigate to Home
	As a application user
	I want to verify the title is correct

Scenario: Navigate to HomePage
	Given I am on the page "/"
	Then I see 'Home Page - angular2prototype' as the title
