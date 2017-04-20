@feature
Feature: NavigateHome
	In order to navigate to Application HomePage
	As a Specflow user
	I want to verify the title is correct

@specflow
Scenario: Navigate to HomePage
	Given the HomePage
	Then I see 'Home Page - angular2prototype' as the title
