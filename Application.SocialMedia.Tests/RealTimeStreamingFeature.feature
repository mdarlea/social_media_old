Feature: RealTimeStreamingFeature

@ignore
Scenario: Subscribe one user to search in Twitter public streams
	Given the following information:
		| UserId                               | ClientCredentials.Type | ClientCredentials.ConsumerKey | ClientCredentials.ConsumerSecret                   |
		| 3b157eac-1faf-4de7-881a-2993996207ae | Twitter                | 7N6hnNVhCZQxOTex4ylQVZyfG     | t579rPenNwRvP9OT4OsDE8lpg1CxaTO4BUzJBwunYRwnkDCAQl |	
	When this user subscribes to receive real time notifications
	And later 'christianity017' tweets a message that contains the '@technology1976' word
	Then the associated tweet streamed event should be fired
