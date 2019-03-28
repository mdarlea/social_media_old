Feature: OAuth
	In order to avoid silly mistakes
	As a math idiot
	I want to be told the sum of two numbers

@mytag
Scenario: Get Twitter user profile
	Given the following information:
	| UserId                               | ClientCredentials.Type | ClientCredentials.ConsumerKey | ClientCredentials.ConsumerSecret                   | CallbackUrl                            | OAuthToken                       | OAuthVerifier                    |
	| 3b157eac-1faf-4de7-881a-2993996207ae | Twitter                | 7N6hnNVhCZQxOTex4ylQVZyfG     | t579rPenNwRvP9OT4OsDE8lpg1CxaTO4BUzJBwunYRwnkDCAQl | http://www.swaksoft.com/oauth/callback | DGcPKH925FT9kXvq2B872ucnmTu6UOe0 | We7fkv6ZLOmPgHX6E3Y1X47BB1XK2BO0 |
	When I request authorization
	Then the Twitter user profile should be returned
