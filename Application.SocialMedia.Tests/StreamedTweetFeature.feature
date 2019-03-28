Feature: StreamedTweetFeature
	StreamedTweet Application Service Tests

@mytag @ignore
Scenario: Find streamed tweets
	Given I want to see streamed tweets on the 1st page of the screen
	And the page size is 10
	And the tweets must match the following search criteria
	| Field  | Value                                |
	| UserId | 3b157eac-1faf-4de7-881a-2993996207ae |
	When I search for the streamed tweets
	Then the found tweets must be displayed on the screen
