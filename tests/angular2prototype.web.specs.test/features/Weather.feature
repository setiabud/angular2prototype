@specflow
Feature: Weather - City Search
	As a application user
	I want to search the weather for a particular city

Scenario: Search weather for valid city
	Given I am on the page "/"
	When I click the link "Weather data"
	And I enter the value "Jakarta" in "city"
	And I click the button "Get Weather"
	Then the text "Weather for Jakarta" is displayed

Scenario: Search weather for invalid city
	Given I am on the page "/"
	When I click the link "Weather data"
	And I enter the value "aaaaaa" in "city"
	And I click the button "Get Weather"
	Then the text "Weather for aaaaaa" is NOT displayed