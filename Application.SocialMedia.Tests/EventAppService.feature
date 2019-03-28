Feature: EventAppService
	Scenarios for the Event Application Service

@CreateNewEventAtExistingAddress
Scenario: Create a new event at an existing address
Given the following event:
| Field       | Value                                |
| Name        | Youth Meeting                        |
| Description | Young adults meeting                 |
| AddressId   | 2                                    |
| UserId      | ef4b2bdb-eda9-4778-bc1c-ab347a4924f5 |
| StartTime   | 10 PM                                |
| EndTime     | 11 PM                                |
When I create this event
Then a new event with the information below should be created in the database:
	| Field       | Value                                |
	| Name        | Youth Meeting                        |
	| Description | Young adults meeting                 |
	| AddressId   | 2                                    |
	| UserId      | ef4b2bdb-eda9-4778-bc1c-ab347a4924f5 |
	| StartTime   | 10 PM                                |
	| EndTime     | 11 PM                                |
And the event application service should return a dto with the following event information:
	| Field       | Value                                |
	| Status      | Success                              |
	| Name        | Youth Meeting                        |
	| Description | Young adults meeting                 |
	| AddressId   | 2                                    |
	| UserId      | ef4b2bdb-eda9-4778-bc1c-ab347a4924f5 |
	| StartTime   | 10 PM                                |
	| EndTime     | 11 PM                                |
	| Instructor  | Michelle Darlea                      |
And the followng address:
	| Field                   | Value             |
	| Id                      | 2                 |
	| StreetAddress           | 3668 Livernois Rd |
	| SuiteNumber             |                   |
	| City                    | Troy              |
	| State                   | MI                |
	| Zip                     | 48083             |
	| CountryIsoCode          | us                |
	| Latitude                | 42.572365         |
	| Longitude               | -83.146155        |
	| GeolocationStreetNumber | 3668              |
	| GeolocationStreet       | 3668 Livernois Rd |
	| IsMainAddress           | falses            |                     

@FindWeeklyEventsForUser
Scenario: Find weekly events for user
	Given The user with the 'ef4b2bdb-eda9-4778-bc1c-ab347a4924f5' id
	When I search for the user's events in the weekly calendar
	Then the service should return the following events:
	| Id | Name                | Description         | AddressId | UserId                               | Instructor      |
	| 1  | Prayer Meeting      | Meet up for prayer  | 1         | ef4b2bdb-eda9-4778-bc1c-ab347a4924f5 | Michelle Darlea |
	| 3  | Small Group Meeting | Small Group Meeting | 1         | ef4b2bdb-eda9-4778-bc1c-ab347a4924f5 | Michelle Darlea |
	And the 'Prayer Meeting' event should start on Saturday from 7 PM until 9 PM

@FindEvent
Scenario: Find event
Given the event id equal with 1
When I search for this event
Then the event application service should return a dto with the following event information:
| Field       | Value                                |
| Status      | Success                              |
| Id          | 1                                    |
| Name        | Prayer Meeting                       |
| Description | Meet up for prayer                   |
| AddressId   | 1                                    |
| UserId      | ef4b2bdb-eda9-4778-bc1c-ab347a4924f5 |
| Instructor  | Michelle Darlea                      |
And the followng address:
	| Field                   | Value                  |
	| StreetAddress           | 10023 Belle Rive Blvd. |
	| SuiteNumber             | Apt. 1204              |
	| City                    | Jacksonville           |
	| State                   | Florida                |
	| Zip                     | 32256                  |
	| CountryIsoCode          | us                     |
	| Latitude                | 30.210796              |
	| Longitude               | -81.5489216            |
	| GeolocationStreetNumber | 10023                  |
	| GeolocationStreet       | Belle Rive Boulevard   |
	| IsMainAddress           | true                   |

@FindRepeatedEvents
Scenario: Find repeated events
Given The user with the 'ef4b2bdb-eda9-4778-bc1c-ab347a4924f5' id
And a start time equal with 12/11/2016
And an end time equal with 12/17/2016
When I search for the user's events in the given time range
Then the application service should return an action result with the Success status 
And the following event should be found:
	| Field       | Value                                |
	| Status      | Success                              |
	| Id          | 3                                    |
	| Name        | Small Group Meeting                  |
	| Instructor  | Michelle Darlea                      |
	| Description | Small Group Meeting                  |
	| AddressId   | 1                                    |
	| UserId      | ef4b2bdb-eda9-4778-bc1c-ab347a4924f5 |
	| Repeat      | True                                 |
	| StartTime   | 12/14/2016 1:00 PM                   |
	| EndTime     | 12/14/2016 3:00 PM                   |

@FindRepeatedEvents
Scenario: Find weekly repeated events (1)
Given The user with the 'ef4b2bdb-eda9-4778-bc1c-ab347a4924f5' id
And a start time equal with 12/18/2016
And an end time equal with 12/24/2016
When I search for the user's events in the given time range
Then the application service should return an action result with the Success status 
And the following event should be found:
	| Field       | Value                                |
	| Status      | Success                              |
	| Id          | 3                                    |
	| Name        | Small Group Meeting                  |
	| Instructor  | Michelle Darlea                      |
	| Description | Small Group Meeting                  |
	| AddressId   | 1                                    |
	| UserId      | ef4b2bdb-eda9-4778-bc1c-ab347a4924f5 |
	| Repeat      | True                                 |
	| StartTime   | 12/14/2016 1:00 PM                   |
	| EndTime     | 12/14/2016 3:00 PM                   |

@FindRepeatedEvents
Scenario: Find weekly repeated events (2)
Given The user with the 'ef4b2bdb-eda9-4778-bc1c-ab347a4924f5' id
And a start time equal with 12/25/2016
And an end time equal with 12/31/2016
When I search for the user's events in the given time range
Then the application service should return an action result with the Success status 
And the following event should be found:
	| Field       | Value                                |
	| Id          | 3                                    |
	| Name        | Small Group Meeting                  |
	| Instructor  | Michelle Darlea                      |
	| Description | Small Group Meeting                  |
	| AddressId   | 1                                    |
	| UserId      | ef4b2bdb-eda9-4778-bc1c-ab347a4924f5 |
	| Repeat      | True                                 |
	| StartTime   | 12/14/2016 1:00 PM                   |
	| EndTime     | 12/14/2016 3:00 PM                   |
