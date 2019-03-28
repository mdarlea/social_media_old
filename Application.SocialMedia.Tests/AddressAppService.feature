@AddressAppServiceFeature
Feature: AddressAppService
	Test the Address application service

@UseAnExistingAddressForUser
Scenario: Associate an existing address with a user
Given The user with the 'ef4b2bdb-eda9-4778-bc1c-ab347a4924f5' id
And the following address:
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
When I associate this address with the given user
Then The adress having the id equal with 1 should be associated with the given user
And the given user should be associated with 1 addresses
And the address application service should return a dto with the following information
	| Field                   | Value                  |
	| Status                  | Success                |
	| Id                      | 1                      |
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

@AddNewAddressForUser
Scenario: Add a new address to an existing user
	Given The user with the 'ef4b2bdb-eda9-4778-bc1c-ab347a4924f5' id
	And the following address:
	| Field                   | Value              |
	| StreetAddress           | Brasov nr.9        |
	| SuiteNumber             | 3                  |
	| City                    | Timisoara          |
	| State                   | Timis              |
	| Zip                     | 1900               |
	| CountryIsoCode          | ro                 |
	| Latitude                | 45.747699          |
	| Longitude               | 21.222093900000004 |
	| GeolocationStreetNumber | 9                  |
	| GeolocationStreet       | Brasov             |
	| IsMainAddress           | true               |
	When I associate this address with the given user
	Then A new address with the information below shoud be added to the database
	| Field                   | Value              |
	| StreetAddress           | Brasov nr.9        |
	| SuiteNumber             | 3                  |
	| City                    | Timisoara          |
	| State                   | Timis              |
	| Zip                     | 1900               |
	| CountryIsoCode          | ro                 |
	| Latitude                | 45.747699          |
	| Longitude               | 21.222093900000004 |
	| GeolocationStreetNumber | 9                  |
	| GeolocationStreet       | Brasov             |
	| IsMainAddress           | true               |
	And the id of the new address should not be equal with 1
	And the newly added address should be associated with the given user
	And the given user should be associated with 2 addresses
	And the address application service should return a dto with the following information
	| Field                   | Value              |
	| StreetAddress           | Brasov nr.9        |
	| SuiteNumber             | 3                  |
	| City                    | Timisoara          |
	| State                   | Timis              |
	| Zip                     | 1900               |
	| CountryIsoCode          | ro                 |
	| Latitude                | 45.747699          |
	| Longitude               | 21.222093900000004 |
	| GeolocationStreetNumber | 9                  |
	| GeolocationStreet       | Brasov             |
	| IsMainAddress           | true               |
	And the id of the returned address should be equal with the id of the newly created address

	@UpdateAnExistingAddress
	Scenario: Update an existing address
	Given the address with the id equal to 3
	And the following address:
	| Field                   | Value            |
	| Id                      | 3                |
	| StreetAddress           | 480 N Orange Ave |
	| City                    | Orlando          |
	| State                   | Florida          |
	| Zip                     | 32801            |
	| CountryIsoCode          | us               |
	| Latitude                | 50.210796        |
	| Longitude               | -42.5489216      |
	| GeolocationStreetNumber | 480              |
	| GeolocationStreet       | N Orange Avenue  |
	| IsMainAddress           | true             |
	| AddressTypeId           | 1                |
	When I update the address with the given new address
	Then the given address entity should be updated with the information from the given  address dto