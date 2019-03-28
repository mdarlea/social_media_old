Feature: UserFeature
	User Application Service Tests

@ignore
Scenario: Get user profiles
	Given the following information about the current user:
		| Field          | Value                                              |	
		| UserId         | 3b157eac-1faf-4de7-881a-2993996207ae               |
	When I search for the given user's external profiles
	Then the result should contain one Twitter profile

@AddStreamFilter @ignore
Scenario: Add steam filter
	Given the following stream filter request:
		| Field  | Value                                |
		| UserId | 3b157eac-1faf-4de7-881a-2993996207ae |
		| Query  | spiritual warfare                    |
	When I request to add the stream filter for this user
	Then the result should be successful

@AddStreamFilterMessageOperation @ignore
Scenario: Add stream filter message operation
	Given the following message operation request:
		| Field          | Value                                |
		| UserId         | 3b157eac-1faf-4de7-881a-2993996207ae |
		| StreamFilterId | 1                                    |
		| MessageId      | 1                                    |
	When I request to associate the message to the stream filter
	Then the message operation result should be successful

@AddStreamFilterMessageOperation @ignore
Scenario: Add stream filter message operation 2
	Given the following message operation request:
		| Field          | Value                                                                                                            |
		| UserId         | 99512f1d-375b-4f53-882c-6b153d911bf5                                                                             |
		| StreamFilterId | 2                                                                                                                |
		| Message        | "Yea, I have loved thee with an everlasting love: therefore with lovingkindness have I drawn thee" Jeremiah 31:3 |
	When I request to associate the message to the stream filter
	Then the message operation result should be successful

@GetStreamFilters @ignore
Scenario: Get stream filters
	Given the following stream filter request:
		| Field  | Value                                |
		| UserId | 3b157eac-1faf-4de7-881a-2993996207ae |
	When I request the stream filters for the given user
	Then the following stream filters must be returned:
		| Id | Query           | MessageOperation.MessageOperationId | MessageOperation.MessageId |
		| 1  | @technology1976 | 1                                   | 1                          |
		|    |                 | 2                                   | 2                          |